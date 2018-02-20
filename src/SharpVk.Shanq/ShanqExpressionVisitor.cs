using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.Expressions;
using SharpVk.Spirv;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SharpVk.Shanq
{
    internal class ShanqExpressionVisitor
    {
        private static readonly Dictionary<Type, (Op TypeOp, int[] Operands)> primitiveTypeMapping = new Dictionary<Type, (Op, int[])>
        {
            [typeof(long)] = (Op.OpTypeInt, new[] { 64, 1 }),
            [typeof(int)] = (Op.OpTypeInt, new[] { 32, 1 }),
            [typeof(short)] = (Op.OpTypeInt, new[] { 16, 1 }),
            [typeof(sbyte)] = (Op.OpTypeInt, new[] { 8, 1 }),
            [typeof(ulong)] = (Op.OpTypeInt, new[] { 64, 0 }),
            [typeof(uint)] = (Op.OpTypeInt, new[] { 32, 0 }),
            [typeof(ushort)] = (Op.OpTypeInt, new[] { 16, 0 }),
            [typeof(byte)] = (Op.OpTypeInt, new[] { 8, 0 }),
            [typeof(double)] = (Op.OpTypeFloat, new[] { 64 }),
            [typeof(float)] = (Op.OpTypeFloat, new[] { 32 }),
            [typeof(void)] = (Op.OpTypeFloat, new int[0])
        };

        private readonly SpirvFile file;
        private readonly IVectorTypeLibrary vectorLibrary;

        private Dictionary<ExpressionType, Func<Expression, ResultId>> expressionVisitors = new Dictionary<ExpressionType, Func<Expression, ResultId>>();
        private Dictionary<SpirvStatement, ResultId> expressionResults = new Dictionary<SpirvStatement, ResultId>();
        private Dictionary<FieldInfo, ResultId> inputMappings = new Dictionary<FieldInfo, ResultId>();
        private Dictionary<FieldInfo, Tuple<ResultId, int>> bindingMappings = new Dictionary<FieldInfo, Tuple<ResultId, int>>();
        private Dictionary<IFromClause, ResultId> samplerMappings = new Dictionary<IFromClause, ResultId>();

        public ShanqExpressionVisitor(SpirvFile file, IVectorTypeLibrary vectorLibrary)
        {
            var visitMethods = this.GetType()
                                    .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                    .Where(method => method.GetCustomAttribute<NodeTypeAttribute>() != null);

            foreach (var method in visitMethods)
            {
                var attribute = method.GetCustomAttribute<NodeTypeAttribute>();

                this.expressionVisitors.Add(attribute.NodeType, x => (ResultId)method.Invoke(this, new object[] { x }));
            }

            this.file = file;
            this.vectorLibrary = vectorLibrary;
        }

        public void AddInputMapping(FieldInfo field, ResultId resultId)
        {
            this.inputMappings.Add(field, resultId);
        }

        public void AddBinding(FieldInfo field, Tuple<ResultId, int> binding)
        {
            this.bindingMappings.Add(field, binding);
        }

        public void AddSampler(IFromClause clause, ResultId resultId)
        {
            this.samplerMappings.Add(clause, resultId);
        }

        public ResultId Visit(Expression expression)
        {

            if (!this.expressionVisitors.TryGetValue(expression.NodeType, out var visit))
            {
                throw new NotImplementedException($"{expression} not implemented.");
            }
            else
            {
                return visit(expression);
            }
        }

        [NodeType(ExpressionType.Convert)]
        private ResultId VisitConvert(UnaryExpression expression)
        {
            var mapping = new Dictionary<(Type, Type), Op>
            {
                [(typeof(float), typeof(int))] = Op.OpConvertFToS,
                [(typeof(float), typeof(uint))] = Op.OpConvertFToU,
                [(typeof(int), typeof(float))] = Op.OpConvertSToF,
                [(typeof(uint), typeof(float))] = Op.OpConvertUToF,
                [(typeof(int), typeof(uint))] = Op.OpSConvert,
                [(typeof(uint), typeof(int))] = Op.OpUConvert,
            };

            if (mapping.TryGetValue((expression.Operand.Type, expression.Type), out Op convertOp))
            {
                ResultId result = this.file.GetNextResultId();

                var subExpressionId = this.Visit(expression.Operand);

                this.file.AddFunctionStatement(result, convertOp, this.Visit(Expression.Constant(expression.Type)), subExpressionId);

                return result;
            }
            else
            {
                throw new NotImplementedException($"Conversion from {expression.Operand.Type} to {expression.Type} not implemented.");
            }
        }

        [NodeType(ExpressionType.Call)]
        private ResultId VisitCall(MethodCallExpression expression)
        {
            var methodDeclaringType = expression.Method.DeclaringType;

            if (IsSampler(methodDeclaringType))
            {
                var sourceReference = (QuerySourceReferenceExpression)expression.Object;
                ResultId samplerTypeId = this.Visit(Expression.Constant(expression.Object.Type));
                ResultId samplerId = this.samplerMappings[(IFromClause)sourceReference.ReferencedQuerySource];
                ResultId loadedSamplerId = this.file.GetNextResultId();
                this.file.AddFunctionStatement(loadedSamplerId, Op.OpLoad, samplerTypeId, samplerId);

                if (expression.Method.Name == nameof(Sampler2d<float, float>.Sample))
                {
                    ResultId resultTypeId = this.Visit(Expression.Constant(expression.Method.ReturnType));

                    ResultId coordId = this.Visit(expression.Arguments[0]);

                    ResultId sampleId = this.file.GetNextResultId();

                    this.file.AddFunctionStatement(sampleId, Op.OpImageSampleImplicitLod, resultTypeId, loadedSamplerId, coordId);

                    return sampleId;
                }

                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        [NodeType(ExpressionType.Negate)]
        private ResultId VisitNegate(UnaryExpression expression)
        {
            if (GetElementType(expression.Type) != typeof(float))
            {
                throw new NotSupportedException($"Expressions of type {expression.Type} are not supported.");
            }

            return VisitUnary(expression, Op.OpFNegate);
        }

        [NodeType(ExpressionType.Add)]
        private ResultId VisitAdd(BinaryExpression expression)
        {
            Op additionOp = SelectByType(expression.Type, Op.OpFAdd, Op.OpIAdd);

            return VisitBinary(expression, additionOp);
        }

        [NodeType(ExpressionType.Subtract)]
        private ResultId VisitSubtract(BinaryExpression expression)
        {
            Op subtractionOp = SelectByType(expression.Type, Op.OpFSub, Op.OpISub);

            return VisitBinary(expression, subtractionOp);
        }

        [NodeType(ExpressionType.Multiply)]
        private ResultId VisitMultiply(BinaryExpression expression)
        {
            Op multiplicationOp;

            if (this.vectorLibrary.IsVectorType(expression.Left.Type) && !this.vectorLibrary.IsVectorType(expression.Right.Type))
            {
                if (!IsFloatingPoint(this.vectorLibrary.GetVectorElementType(expression.Left.Type))
                        || !IsFloatingPoint(GetElementType(expression.Right.Type)))
                {
                    throw new NotSupportedException("Vector by scalar multiplication is only supported for floating point types.");
                }

                if (this.vectorLibrary.IsMatrixType(expression.Right.Type))
                {
                    multiplicationOp = Op.OpVectorTimesMatrix;
                }
                else
                {
                    multiplicationOp = Op.OpVectorTimesScalar;
                }
            }
            else if (this.vectorLibrary.IsMatrixType(expression.Left.Type))
            {
                if (!IsFloatingPoint(GetElementType(expression.Right.Type)))
                {
                    throw new NotSupportedException("Matrix by scalar multiplication is only supported for floating point types.");
                }

                if (this.vectorLibrary.IsVectorType(expression.Right.Type))
                {
                    multiplicationOp = Op.OpMatrixTimesVector;
                }
                else if (this.vectorLibrary.IsMatrixType(expression.Right.Type))
                {
                    multiplicationOp = Op.OpMatrixTimesMatrix;
                }
                else
                {
                    multiplicationOp = Op.OpMatrixTimesScalar;
                }
            }
            else
            {
                multiplicationOp = SelectByType(expression.Type, Op.OpFMul, Op.OpIMul);
            }

            return VisitBinary(expression, multiplicationOp);
        }

        [NodeType(ExpressionType.Modulo)]
        private ResultId VisitModulo(BinaryExpression expression)
        {
            Op moduloOp = SelectByType(expression.Type, Op.OpFMod, Op.OpSMod, Op.OpUMod);

            return VisitBinary(expression, moduloOp);
        }

        [NodeType(ExpressionType.Divide)]
        private ResultId VisitDivide(BinaryExpression expression)
        {
            Op divisionOp = SelectByType(expression.Type, Op.OpFDiv, Op.OpSDiv, Op.OpUDiv);

            return VisitBinary(expression, divisionOp);
        }

        private ResultId VisitUnary(UnaryExpression expression, Op unaryOp)
        {
            ResultId resultTypeId = this.Visit(Expression.Constant(expression.Type));

            ResultId operand = this.Visit(expression.Operand);
            ResultId result = this.file.GetNextResultId();

            this.file.AddFunctionStatement(result, unaryOp, resultTypeId, operand);

            return result;
        }

        private ResultId VisitBinary(BinaryExpression expression, Op binaryOp)
        {
            ResultId resultTypeId = this.Visit(Expression.Constant(expression.Type));

            ResultId left = this.Visit(expression.Left);
            ResultId right = this.Visit(expression.Right);
            ResultId result = this.file.GetNextResultId();

            this.file.AddFunctionStatement(result, binaryOp, resultTypeId, left, right);

            return result;
        }

        [NodeType(ExpressionType.MemberAccess)]
        private ResultId VisitMemberAccess(MemberExpression expression)
        {
            if (expression.Expression is QuerySourceReferenceExpression)
            {
                var fieldInfo = (FieldInfo)expression.Member;

                return this.GetInputId(fieldInfo);
            }
            else
            {
                var targetType = expression.Expression.Type;

                if (this.vectorLibrary.IsVectorType(targetType))
                {
                    GetMemberData(expression, out string name, out var type);

                    int fieldIndex;

                    switch (name)
                    {
                        case "x":
                        case "r":
                            fieldIndex = 0;
                            break;
                        case "y":
                        case "g":
                            fieldIndex = 1;
                            break;
                        case "z":
                        case "b":
                            fieldIndex = 2;
                            break;
                        case "w":
                        case "a":
                            fieldIndex = 3;
                            break;
                        default:
                            throw new Exception($"Unsupported field: {name}");
                    }

                    ResultId targetId = this.Visit(expression.Expression);

                    ResultId typeId = this.Visit(Expression.Constant(type));

                    ResultId accessId = this.file.GetNextResultId();

                    this.file.AddFunctionStatement(accessId, Op.OpCompositeExtract, typeId, targetId, fieldIndex);

                    return accessId;
                }
                else
                {
                    throw new NotImplementedException("Member access is only implemented for vector types.");
                }
            }
        }

        private static void GetMemberData(MemberExpression expression, out string name, out Type type)
        {
            name = expression.Member.Name;

            switch (expression.Member.MemberType)
            {
                case MemberTypes.Field:
                    type = ((FieldInfo)expression.Member).FieldType;
                    break;
                case MemberTypes.Property:
                    type = ((PropertyInfo)expression.Member).PropertyType;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private ResultId GetInputId(FieldInfo fieldInfo)
        {
            ResultId typeId = this.Visit(Expression.Constant(fieldInfo.FieldType));

            if (this.inputMappings.ContainsKey(fieldInfo))
            {
                ResultId fieldId = this.inputMappings[fieldInfo];

                ResultId resultId = this.file.GetNextResultId();

                this.file.AddFunctionStatement(resultId, Op.OpLoad, typeId, fieldId);

                return resultId;
            }
            else
            {
                ResultId pointerTypeId = this.Visit(Expression.Constant(typeof(UniformPointer<>).MakeGenericType(fieldInfo.FieldType)));

                var binding = this.bindingMappings[fieldInfo];

                ResultId accessId = this.file.GetNextResultId();

                this.file.AddFunctionStatement(accessId, Op.OpAccessChain, pointerTypeId, binding.Item1, this.Visit(Expression.Constant(binding.Item2)));

                ResultId resultId = this.file.GetNextResultId();

                this.file.AddFunctionStatement(resultId, Op.OpLoad, typeId, accessId);

                return resultId;
            }
        }

        [NodeType(ExpressionType.New)]
        private ResultId VisitNew(NewExpression expression)
        {
            SpirvStatement statement;

            if (this.vectorLibrary.IsVectorType(expression.Type))
            {
                var operands = new[] { this.Visit(Expression.Constant(expression.Type)) }
                                    .Concat(this.ExpandNewArguments(expression.Arguments));

                statement = new SpirvStatement(Op.OpCompositeConstruct, operands.Cast<object>().ToArray());
            }
            else
            {
                throw new NotImplementedException("New expressions are only implemented for vector types.");
            }

            ResultId resultId = this.file.GetNextResultId();

            this.file.AddFunctionStatement(resultId, statement);

            return resultId;
        }

        private IEnumerable<ResultId> ExpandNewArguments(IEnumerable<Expression> arguments)
        {
            foreach (var argument in arguments)
            {
                ResultId argumentId = this.Visit(argument);

                if (this.vectorLibrary.IsVectorType(argument.Type))
                {
                    ResultId typeId = this.Visit(Expression.Constant(this.vectorLibrary.GetVectorElementType(argument.Type)));

                    for (int index = 0; index < this.vectorLibrary.GetVectorLength(argument.Type); index++)
                    {
                        ResultId fieldId = this.file.GetNextResultId();

                        this.file.AddFunctionStatement(fieldId, Op.OpCompositeExtract, typeId, argumentId, index);

                        yield return fieldId;
                    }
                }
                else
                {
                    yield return argumentId;
                }
            }
        }

        [NodeType(ExpressionType.Constant)]
        private ResultId VisitConstant(ConstantExpression expression)
        {
            SpirvStatement statement;

            if (this.vectorLibrary.IsVectorType(expression.Type))
            {
                var operands = new object[] { expression.Type }
                                    .Concat(((IEnumerable)expression.Value).OfType<object>())
                                    .Select(x => (object)this.Visit(Expression.Constant(x)));

                statement = new SpirvStatement(Op.OpConstantComposite, operands.ToArray());
            }
            else if (typeof(Type).IsAssignableFrom(expression.Type))
            {
                statement = VisitTypeConstant(expression);
            }
            else
            {
                ResultId typeOperand = this.Visit(Expression.Constant(expression.Type));
                statement = new SpirvStatement(Op.OpConstant, typeOperand, expression.Value);
            }


            if (!this.expressionResults.TryGetValue(statement, out ResultId resultId))
            {
                resultId = this.file.GetNextResultId();

                this.expressionResults.Add(statement, resultId);

                this.file.AddGlobalStatement(resultId, statement);
            }

            return resultId;
        }

        private SpirvStatement VisitTypeConstant(ConstantExpression expression)
        {
            Type value = (Type)expression.Value;

            if (this.vectorLibrary.IsMatrixType(value))
            {
                Type rowType = this.vectorLibrary.GetMatrixRowType(value);
                int[] dimensions = this.vectorLibrary.GetMatrixDimensions(value);

                ResultId rowTypeId = this.Visit(Expression.Constant(rowType));

                return new SpirvStatement(Op.OpTypeMatrix, rowTypeId, dimensions[0]);
            }
            else if (this.vectorLibrary.IsVectorType(value))
            {
                Type elementType = this.vectorLibrary.GetVectorElementType(value);
                int length = this.vectorLibrary.GetVectorLength(value);

                ResultId elementTypeId = this.Visit(Expression.Constant(elementType));

                return new SpirvStatement(Op.OpTypeVector, elementTypeId, length);
            }
            else if (typeof(Delegate).IsAssignableFrom(value))
            {
                var returnType = value.GetMethod("Invoke").ReturnType;

                ResultId returnTypeId = this.Visit(Expression.Constant(returnType));

                if (value.GetMethod("Invoke").GetParameters().Length > 0)
                {
                    throw new NotImplementedException();
                }

                return new SpirvStatement(Op.OpTypeFunction, returnTypeId);
            }
            else if (value.BaseType.IsGenericType && value.BaseType.GetGenericTypeDefinition() == typeof(Pointer<>))
            {
                StorageClass storage = (StorageClass)value.GetProperty("Storage").GetValue(null);
                ResultId typeId = this.Visit(Expression.Constant(value.GetGenericArguments()[0]));

                return new SpirvStatement(Op.OpTypePointer, storage, typeId);
            }
            else if (IsTupleType(value))
            {
                var fieldTypes = value.GetGenericArguments();

                var fieldTypeIds = fieldTypes.Select(x => (object)this.Visit(Expression.Constant(x))).ToArray();

                return new SpirvStatement(Op.OpTypeStruct, fieldTypeIds);
            }
            else if (primitiveTypeMapping.TryGetValue(value, out var mapping))
            {
                return new SpirvStatement(mapping.TypeOp, mapping.Operands);
            }
            else if (value.IsValueType)
            {
                var fieldTypeIds = value.GetFieldsByOffset().Select(x => (object)this.Visit(Expression.Constant(x.FieldType))).ToArray();

                return new SpirvStatement(Op.OpTypeStruct, fieldTypeIds);
            }
            else if (IsSampler(value))
            {
                ResultId imageTypeId = this.Visit(Expression.Constant(typeof(ImageType2d<>).MakeGenericType(value.GetGenericArguments()[0])));

                return new SpirvStatement(Op.OpTypeSampledImage, imageTypeId);
            }
            else if (IsImage(value))
            {
                ResultId pixelTypeId = this.Visit(Expression.Constant(this.vectorLibrary.GetVectorElementType(value.GetGenericArguments()[0])));

                return new SpirvStatement(Op.OpTypeImage, pixelTypeId, Dim.Dim2D, 0, 0, 0, 1, ImageFormat.Unknown);
            }
            else
            {
                throw new NotImplementedException($"Constants of type {value} are not implemented.");
            }
        }

        private static bool IsSampler(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Sampler2d<,>);
        }

        private static bool IsImage(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ImageType2d<>);
        }

        private Op SelectByType(Type type, Op floatingPointOp, Op integerOp)
        {
            return SelectByType(type, floatingPointOp, integerOp, integerOp);
        }

        private Op SelectByType(Type type, Op floatingPointOp, Op signedIntegerOp, Op unsignedIntegerOp)
        {
            type = GetElementType(type);

            if (IsFloatingPoint(type))
            {
                return floatingPointOp;
            }
            else if (IsSignedInteger(type))
            {
                return signedIntegerOp;
            }
            else if (IsUnsignedInteger(type))
            {
                return unsignedIntegerOp;
            }
            else
            {
                throw new NotSupportedException($"Operations of type {type} are not supported.");
            }
        }

        private static bool IsFloatingPoint(Type type)
        {
            return type == typeof(float) || type == typeof(double);
        }

        private static bool IsInteger(Type type)
        {
            return IsSignedInteger(type) || IsUnsignedInteger(type);
        }

        private static bool IsSignedInteger(Type type)
        {
            return type == typeof(sbyte)
                    || type == typeof(short)
                    || type == typeof(int)
                    || type == typeof(long);
        }

        private static bool IsUnsignedInteger(Type type)
        {
            return type == typeof(byte)
                    || type == typeof(ushort)
                    || type == typeof(uint)
                    || type == typeof(ulong);
        }

        private bool IsTupleType(Type value)
        {
            return value.GetInterfaces().Any(x => x.Name == "ITuple");
        }

        private Type GetElementType(Type type)
        {
            if (type.IsPrimitive)
            {
                return type;
            }
            else if (this.vectorLibrary.IsVectorType(type))
            {
                return this.vectorLibrary.GetVectorElementType(type);
            }
            else if (this.vectorLibrary.IsMatrixType(type))
            {
                return typeof(float);
            }
            else
            {
                throw new NotSupportedException($"Type {type} is not supported for this expression.");
            }
        }

        private class NodeTypeAttribute
            : Attribute
        {
            public NodeTypeAttribute(ExpressionType nodeType)
            {
                this.NodeType = nodeType;
            }

            public ExpressionType NodeType
            {
                get;
                private set;
            }
        }
    }
}
