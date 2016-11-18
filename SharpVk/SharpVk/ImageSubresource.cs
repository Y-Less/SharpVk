// The MIT License (MIT)
// 
// Copyright (c) Andrew Armstrong/FacticiusVir 2016
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
using System.Runtime.InteropServices;
using System.Text;

namespace SharpVk
{
    /// <summary>
    /// <para>
    /// Structure specifying a image subresource.
    /// </para>
    /// <para>
    /// .Valid Usage **** * pname:mipLevel must: be less than the
    /// pname:mipLevels specified in slink:VkImageCreateInfo when the image was
    /// created * pname:arrayLayer must: be less than the pname:arrayLayers
    /// specified in slink:VkImageCreateInfo when the image was created ****
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ImageSubresource
    {
        /// <summary>
        /// 
        /// </summary>
        public ImageSubresource(ImageAspectFlags aspectMask, uint mipLevel, uint arrayLayer)
        {
            this.AspectMask = aspectMask;
            this.MipLevel = mipLevel;
            this.ArrayLayer = arrayLayer;
        }
        
        /// <summary>
        /// pname:aspectMask is a elink:VkImageAspectFlags selecting the image
        /// _aspect_.
        /// </summary>
        public ImageAspectFlags AspectMask; 
        
        /// <summary>
        /// pname:mipLevel selects the mipmap level.
        /// </summary>
        public uint MipLevel; 
        
        /// <summary>
        /// pname:arrayLayer selects the array layer.
        /// </summary>
        public uint ArrayLayer; 
        
        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("ImageSubresource");
            builder.AppendLine("{");
            builder.AppendLine($"AspectMask: {this.AspectMask}");
            builder.AppendLine($"MipLevel: {this.MipLevel}");
            builder.AppendLine($"ArrayLayer: {this.ArrayLayer}");
            builder.Append("}");
            return builder.ToString();
        }
    }
}
