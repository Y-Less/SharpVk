using SharpVk.Multivendor;
using System;

namespace SharpVk.Extra
{
    /// <summary>
    /// 
    /// </summary>
    public class DebugUtilsMessengerWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Multivendor.DebugUtilsMessengerCreateFlags? Flags
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Multivendor.DebugUtilsMessageSeverityFlags MessageSeverity
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Multivendor.DebugUtilsMessageTypeFlags MessageType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate bool Delegate(SharpVk.Multivendor.DebugUtilsMessageSeverityFlags messageSeverity, SharpVk.Multivendor.DebugUtilsMessageTypeFlags messageTypes, SharpVk.Multivendor.DebugUtilsMessengerCallbackData pCallbackData);

        private readonly System.Runtime.InteropServices.GCHandle gch_;

        private static unsafe Bool32 DebugUtilsMessengerWrapperCallback(SharpVk.Multivendor.DebugUtilsMessageSeverityFlags messageSeverity, SharpVk.Multivendor.DebugUtilsMessageTypeFlags messageTypes, IntPtr pCallbackData, IntPtr pUserData)
        {
            if (pUserData == IntPtr.Zero)
            {
                return false;
            }

            System.Runtime.InteropServices.GCHandle gch = System.Runtime.InteropServices.GCHandle.FromIntPtr(pUserData);
            Delegate callback = (Delegate)gch.Target;
            if (callback == null)
            {
                return false;
            }

            SharpVk.Multivendor.DebugUtilsMessengerCallbackData userData = SharpVk.Multivendor.DebugUtilsMessengerCallbackData.MarshalFrom((SharpVk.Interop.Multivendor.DebugUtilsMessengerCallbackData*)pCallbackData.ToPointer());
            return callback(messageSeverity, messageTypes, userData);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="messageSeverity"></param>
        /// <param name="messageType"></param>
        /// <param name="flags"></param>
        public DebugUtilsMessengerWrapper(Delegate callback, SharpVk.Multivendor.DebugUtilsMessageSeverityFlags messageSeverity, SharpVk.Multivendor.DebugUtilsMessageTypeFlags messageType, SharpVk.Multivendor.DebugUtilsMessengerCreateFlags? flags = null)
        {
            gch_ = System.Runtime.InteropServices.GCHandle.Alloc(callback);
            Flags = flags;
            MessageSeverity = messageSeverity;
            MessageType = messageType;
            Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        ~DebugUtilsMessengerWrapper()
        {
            gch_.Free();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="that"></param>
        public static implicit operator SharpVk.Multivendor.DebugUtilsMessengerCreateInfo(DebugUtilsMessengerWrapper that)
        {
            return new SharpVk.Multivendor.DebugUtilsMessengerCreateInfo {
                Flags = that.Flags,
                MessageSeverity = that.MessageSeverity,
                MessageType = that.MessageType,
                UserCallback = DebugUtilsMessengerWrapperCallback,
                UserData = System.Runtime.InteropServices.GCHandle.ToIntPtr(that.gch_)
            };
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
        /// <param name="allocator">
        /// An optional AllocationCallbacks instance that controls host memory
        /// allocation.
        /// </param>
        public unsafe SharpVk.Instance CreateInstance(ArrayProxy<string>? enabledLayerNames, ArrayProxy<string>? enabledExtensionNames, SharpVk.InstanceCreateFlags? flags = null, SharpVk.ApplicationInfo? applicationInfo = null, SharpVk.Multivendor.ValidationFlags? validationFlagsExt = null, AllocationCallbacks? allocator = null)
        {
            if (Enabled)
            {
                var extensionsIn = enabledExtensionNames.GetValueOrDefault();
                int len = extensionsIn.Length;
                var extensionsOut = new string[len + 1];
                bool found = false;
                for (int i = 0; i != len ; ++i)
                {
                    extensionsOut[i] = extensionsIn[i];
                    found = found || extensionsIn[i] == SharpVk.Multivendor.ExtExtensions.DebugUtils;
                }
                if (!found)
                {
                    extensionsOut[len] = SharpVk.Multivendor.ExtExtensions.DebugUtils;
                }

                SharpVk.Instance result = Instance.Create(enabledLayerNames, found ? extensionsIn : extensionsOut, flags, applicationInfo, null, validationFlagsExt, this, allocator);
                result.CreateDebugUtilsMessenger(MessageSeverity, MessageType, DebugUtilsMessengerWrapperCallback, Flags, System.Runtime.InteropServices.GCHandle.ToIntPtr(gch_), allocator);
                return result;
            }
            return Instance.Create(enabledLayerNames, enabledExtensionNames, flags, applicationInfo, null, validationFlagsExt, null, allocator);
        }
    }
}
