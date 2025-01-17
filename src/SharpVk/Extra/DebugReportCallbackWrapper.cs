using SharpVk.Multivendor;
using System;

namespace SharpVk.Extra
{
    /// <summary>
    /// 
    /// </summary>
    public class DebugReportCallbackWrapper : DebuggableInstanceWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Multivendor.DebugReportFlags? Flags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DebugReportCallback Debugger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public delegate bool Delegate(DebugReportFlags flags, DebugReportObjectType objectType, ulong @object, HostSize location, int messageCode, string pLayerPrefix, string pMessage);

        private readonly Delegate callback_;
        private readonly System.Runtime.InteropServices.GCHandle gch_;

        private static unsafe Bool32 DebugReportCallbackWrapperCallback(DebugReportFlags flags, DebugReportObjectType objectType, ulong @object, HostSize location, int messageCode, string pLayerPrefix, string pMessage, IntPtr pUserData)
        {
            if (pUserData == IntPtr.Zero)
            {
                return false;
            }

            System.Runtime.InteropServices.GCHandle gch = System.Runtime.InteropServices.GCHandle.FromIntPtr(pUserData);
            DebugReportCallbackWrapper callback = (DebugReportCallbackWrapper)gch.Target;
            if (callback == null)
            {
                return false;
            }

            return callback.callback_(flags, objectType, @object, location, messageCode, pLayerPrefix, pMessage) && callback.ValidationLayerTesting;
        }

        private static SharpVk.Multivendor.DebugReportCallbackDelegate
            DebugReportCallbackDelegate = DebugReportCallbackWrapperCallback;

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            Debugger?.Dispose();
            Debugger = null;

            Instance?.Dispose();
            Instance = null;

            gch_.Free();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="flags"></param>
        public DebugReportCallbackWrapper(Delegate callback, SharpVk.Multivendor.DebugReportFlags? flags = null)
        {
            callback_ = callback;
            gch_ = System.Runtime.InteropServices.GCHandle.Alloc(this);
            Flags = flags;
            Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        ~DebugReportCallbackWrapper()
        {
            Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="that"></param>
        public static implicit operator SharpVk.Multivendor.DebugReportCallbackCreateInfo(DebugReportCallbackWrapper that)
        {
            return new SharpVk.Multivendor.DebugReportCallbackCreateInfo {
                Flags = that.Flags,
                Callback = DebugReportCallbackDelegate,
                UserData = System.Runtime.InteropServices.GCHandle.ToIntPtr(that.gch_)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="that"></param>
        public static implicit operator SharpVk.Multivendor.DebugReportCallback(DebugReportCallbackWrapper that)
        {
            return that.Debugger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="that"></param>
        public static implicit operator SharpVk.Instance(DebugReportCallbackWrapper that)
        {
            return that.Instance;
        }

        /// <summary>
        /// Create a new Vulkan instance.
        /// </summary>
        /// <param name="flags">
        /// Reserved for future use.
        /// </param>
        /// <param name="applicationInfo">
        /// Null or an instance of ApplicationInfo. If not Null, this
        /// information helps implementations recognize behavior inherent to
        /// classes of applications. ApplicationInfo is defined in detail
        /// below.
        /// </param>
        /// <param name="enabledLayerNames">
        /// An array of enabledLayerCount strings containing the names of
        /// layers to enable for the created instance. See the Layers section
        /// for further details.
        /// </param>
        /// <param name="enabledExtensionNames">
        /// An array of enabledExtensionCount strings containing the names of
        /// extensions to enable.
        /// </param>
        /// <param name="validationFlagsExt">
        /// </param>
        /// <param name="validationFeaturesExt"></param>
        /// <param name="allocator">
        /// An optional AllocationCallbacks instance that controls host memory
        /// allocation.
        /// </param>
        public unsafe override void CreateInstance(ArrayProxy<string>? enabledLayerNames, ArrayProxy<string>? enabledExtensionNames, SharpVk.InstanceCreateFlags? flags = null, SharpVk.ApplicationInfo? applicationInfo = null, SharpVk.Multivendor.ValidationFlags? validationFlagsExt = null, SharpVk.Multivendor.ValidationFeatures? validationFeaturesExt = null, AllocationCallbacks? allocator = null)
        {
            if (Instance != null)
            {
                return;
            }
            else if (Enabled)
            {
                Instance = SharpVk.Instance.Create(enabledLayerNames.GetValueOrDefault().AddUnique("VK_LAYER_KHRONOS_validation"), enabledExtensionNames.GetValueOrDefault().AddUnique(SharpVk.Multivendor.ExtExtensions.DebugReport), flags, applicationInfo, this, validationFlagsExt, validationFeaturesExt, null, allocator);
                Debugger = Instance.CreateDebugReportCallback(DebugReportCallbackDelegate, Flags, System.Runtime.InteropServices.GCHandle.ToIntPtr(gch_), allocator);
            }
            else
            {
                Instance = SharpVk.Instance.Create(enabledLayerNames, enabledExtensionNames, flags, applicationInfo, null, validationFlagsExt, validationFeaturesExt, null, allocator);
            }
        }
    }
}
