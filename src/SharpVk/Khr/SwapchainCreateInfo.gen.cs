// The MIT License (MIT)
// 
// Copyright (c) Andrew Armstrong/FacticiusVir 2017
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

// This file was automatically generated and should not be edited directly.

using System;

namespace SharpVk.Khr
{
    /// <summary>
    /// 
    /// </summary>
    public struct SwapchainCreateInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public IntPtr Next
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Khr.SwapchainCreateFlags Flags
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Khr.Surface Surface
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public uint MinImageCount
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Format ImageFormat
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Khr.ColorSpace ImageColorSpace
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Extent2D ImageExtent
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public uint ImageArrayLayers
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ImageUsageFlags ImageUsage
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public SharingMode ImageSharingMode
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public uint QueueFamilyIndexCount
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public uint QueueFamilyIndices
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Khr.SurfaceTransformFlags PreTransform
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Khr.CompositeAlphaFlags CompositeAlpha
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public SharpVk.Khr.PresentMode PresentMode
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Bool32 Clipped
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Khr.Swapchain OldSwapchain
        {
            get;
            set;
        }
    }
}