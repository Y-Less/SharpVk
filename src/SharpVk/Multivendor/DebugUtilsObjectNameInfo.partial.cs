using System;

namespace SharpVk.Multivendor
{
    public partial struct DebugUtilsObjectNameInfo
    {
        /// <summary>
        /// 
        /// </summary>
        internal static unsafe DebugUtilsObjectNameInfo MarshalFrom(SharpVk.Interop.Multivendor.DebugUtilsObjectNameInfo* pointer)
        {
            DebugUtilsObjectNameInfo result = default(DebugUtilsObjectNameInfo);
            result.ObjectType = pointer->ObjectType;
            result.ObjectHandle = pointer->ObjectHandle;
            result.ObjectName = Interop.HeapUtil.MarshalStringFrom(pointer->ObjectName);
            return result;
        }
    }
}

