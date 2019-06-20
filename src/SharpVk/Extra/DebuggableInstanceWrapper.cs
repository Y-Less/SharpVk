using System;

namespace SharpVk.Extra
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DebuggableInstanceWrapper : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Set to `true` to allow returning `true` from debug callbacks.  Default (`false`)
        /// disables this feature, mainly used for internal testing to abort actions.
        /// </summary>
        public bool ValidationLayerTesting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Instance Instance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabledLayerNames"></param>
        /// <param name="enabledExtensionNames"></param>
        /// <param name="flags"></param>
        /// <param name="applicationInfo"></param>
        /// <param name="validationFlagsExt"></param>
        /// <param name="allocator"></param>
        public unsafe abstract void CreateInstance(ArrayProxy<string>? enabledLayerNames, ArrayProxy<string>? enabledExtensionNames, SharpVk.InstanceCreateFlags? flags = null, SharpVk.ApplicationInfo? applicationInfo = null, SharpVk.Multivendor.ValidationFlags? validationFlagsExt = null, AllocationCallbacks? allocator = null);

        /// <summary>
        /// 
        /// </summary>
        public abstract void Dispose();
    }
}
