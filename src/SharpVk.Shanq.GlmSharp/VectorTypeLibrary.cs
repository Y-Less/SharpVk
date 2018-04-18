using GlmSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpVk.Shanq.GlmSharp
{
    public class VectorTypeLibrary
        : IVectorTypeLibrary
    {
        private readonly static Dictionary<Type, Format> vectorFormats = new Dictionary<Type, Format>()
        {
            [typeof(float)] = Format.R32SFloat,
            [typeof(vec2)] = Format.R32G32SFloat,
            [typeof(vec3)] = Format.R32G32B32SFloat,
            [typeof(vec4)] = Format.R32G32B32A32SFloat,
            [typeof(int)] = Format.R32SInt,
            [typeof(ivec2)] = Format.R32G32SInt,
            [typeof(ivec3)] = Format.R32G32B32SInt,
            [typeof(ivec4)] = Format.R32G32B32A32SInt,
            [typeof(uint)] = Format.R32UInt,
            [typeof(uvec2)] = Format.R32G32UInt,
            [typeof(uvec3)] = Format.R32G32B32UInt,
            [typeof(uvec4)] = Format.R32G32B32A32UInt
        };

        private readonly static Assembly glmSharpAssembly = typeof(mat4).Assembly;

        public readonly static VectorTypeLibrary Instance = new VectorTypeLibrary();

        public Type GetMatrixRowType(Type value)
        {
            return value.GetProperty("Row0").PropertyType;
        }

        public Type GetVectorElementType(Type value)
        {
            return value.GetField("x").FieldType;
        }

        public int GetVectorLength(Type value)
        {
            return ((IEnumerable)value.GetProperty("Zero")
                                .GetValue(null))
                                .OfType<object>()
                                .Count();
        }

        public int[] GetMatrixDimensions(Type value)
        {
            var identity = value.GetProperty("Identity")
                                .GetValue(null);

            var values = (float[,])value.GetProperty("Values")
                                        .GetValue(identity);

            return new[]
            {
                values.GetLength(0),
                values.GetLength(1)
            };
        }

        public bool IsVectorType(Type type)
        {
            return type.Assembly == glmSharpAssembly
                && type.Name.Contains("vec");
        }

        public bool IsMatrixType(Type type)
        {
            return type.Assembly == glmSharpAssembly
                && type.Name.Contains("mat");
        }

        public IEnumerable<int> GetSwizzle(Type type, string name)
        {
            if (name.Length > 4)
            {
                return null;
            }

            var result = new List<int>();

            for (int index = 0; index < name.Length; index++)
            {
                switch(name[index])
                {
                    case 'r':
                    case 'x':
                        result.Add(0);
                        break;
                    case 'g':
                    case 'y':
                        result.Add(1);
                        break;
                    case 'b':
                    case 'z':
                        result.Add(2);
                        break;
                    case 'a':
                    case 'w':
                        result.Add(3);
                        break;
                }
            }

            return result;
        }

        public Format GetVectorFormat(Type type)
        {
            if (vectorFormats.TryGetValue(type, out Format result))
            {
                return result;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
