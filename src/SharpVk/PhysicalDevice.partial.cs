namespace SharpVk
{
    public partial class PhysicalDevice
    {
        /// <summary>
        /// Returns properties of available physical device extensions.
        /// </summary>
        public unsafe SharpVk.ExtensionProperties[] EnumerateDeviceExtensionProperties()
        {
            return EnumerateDeviceExtensionProperties(null);
        }
    }
}
