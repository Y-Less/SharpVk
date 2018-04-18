using System;
using System.Collections.Generic;

namespace SharpVk.Shanq
{
    public interface IVectorTypeLibrary
    {
        Type GetMatrixRowType(Type value);

        Type GetVectorElementType(Type value);

        int GetVectorLength(Type value);

        int[] GetMatrixDimensions(Type value);

        bool IsVectorType(Type type);

        bool IsMatrixType(Type type);

        IEnumerable<int> GetSwizzle(Type type, string name);

        Format GetVectorFormat(Type type);
    }
}
