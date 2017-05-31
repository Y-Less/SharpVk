﻿using SharpVk.Generator.Collation;
using SharpVk.Generator.Rules;
using System.Collections.Generic;

using static SharpVk.Emit.ExpressionBuilder;

namespace SharpVk.Generator.Generation.Marshalling
{
    public class ArrayMemberPattern
        : IMemberPatternRule
    {
        private readonly IEnumerable<IMarshalValueRule> marshallingRules;
        private readonly NameLookup nameLookup;

        public ArrayMemberPattern(IEnumerable<IMarshalValueRule> marshallingRules, NameLookup nameLookup)
        {
            this.marshallingRules = marshallingRules;
            this.nameLookup = nameLookup;
        }

        public bool Apply(IEnumerable<ITypedDeclaration> others, ITypedDeclaration source, MemberPatternInfo info)
        {
            if (source.Dimensions != null)
            {
                string typeName = this.nameLookup.Lookup(source.Type, true);

                info.Interop = new TypedDefinition
                {
                    Name = source.Name,
                    Type = typeName
                };

                if (source.Dimensions.Length == 2)
                {
                    info.Public = new TypedDefinition
                    {
                        Name = source.Name,
                        Type = "string[]"
                    };
                }
                else if (source.Dimensions.Length == 1)
                {
                    switch (source.Dimensions[0].Type)
                    {
                        case LenType.NullTerminated:
                            info.Public = new TypedDefinition
                            {
                                Name = source.Name,
                                Type = "string"
                            };

                            info.InteropFullType = typeName;

                            info.MarshalTo.Add((getTarget, getValue) => new AssignAction
                            {
                                ValueExpression = StaticCall("Interop.HeapUtil", "MarshalTo", getValue(source.Name)),
                                TargetExpression = getTarget(source.Name),
                                MemberType = this.nameLookup.Lookup(source.Type, false),
                                Type = AssignActionType.Assign
                            });

                            info.MarshalFrom.Add(new AssignAction
                            {
                                ValueExpression = StaticCall("Interop.HeapUtil", "MarshalStringFrom", DerefMember(Variable("pointer"), source.Name)),
                                TargetExpression = Member(Variable("result"), source.Name),
                                MemberType = this.nameLookup.Lookup(source.Type, false),
                                Type = AssignActionType.Assign
                            });

                            break;
                        case LenType.Expression:
                            var elementType = source.Type.Deref();

                            if (elementType.VkName == "void")
                            {
                                elementType.VkName = "uint8_t";
                            }

                            var marshalling = this.marshallingRules.ApplyFirst(elementType);

                            info.InteropFullType = marshalling.InteropType;

                            if(source.Type.PointerType.IsPointer())
                            {
                                info.InteropFullType += "*";
                            }

                            info.Public = new TypedDefinition
                            {
                                Name = source.Name,
                                Type = marshalling.MemberType + "[]"
                            };

                            info.MarshalTo.Add((getTarget, getValue) => new AssignAction
                            {
                                TargetExpression = getTarget(source.Name),
                                MemberType = marshalling.InteropType,
                                IsLoop = true,
                                IndexName = "index",
                                Type = marshalling.MarshalToActionType,
                                NullCheckExpression = IsNotEqual(getValue(source.Name), Null),
                                LengthExpression = Member(getValue(source.Name), "Length"),
                                ValueExpression = marshalling.BuildMarshalToValueExpression(Index(getValue(source.Name), Variable("index")))
                            });
                            break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}