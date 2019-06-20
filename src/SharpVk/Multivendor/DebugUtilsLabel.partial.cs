using System;

namespace SharpVk.Multivendor
{
    public partial struct DebugUtilsLabel
    {
        /// <summary>
        /// 
        /// </summary>
        internal unsafe static DebugUtilsLabel MarshalFrom(SharpVk.Interop.Multivendor.DebugUtilsLabel* pointer)
        {
            DebugUtilsLabel result = default(DebugUtilsLabel);
            result.LabelName = Interop.HeapUtil.MarshalStringFrom(pointer->LabelName);
            result.Color = (pointer->Color[0], pointer->Color[1], pointer->Color[2], pointer->Color[3]);
            return result;
        }
    }
}
