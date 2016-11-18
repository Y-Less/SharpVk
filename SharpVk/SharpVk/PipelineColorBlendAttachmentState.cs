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
    /// Structure specifying a pipeline color blend attachment state.
    /// </para>
    /// <para>
    /// .Valid Usage **** * If the &lt;&lt;features-features-dualSrcBlend,dual
    /// source blending&gt;&gt; feature is not enabled,
    /// pname:srcColorBlendFactor must: not be
    /// ename:VK_BLEND_FACTOR_SRC1_COLOR, ename:VK_BLEND_FACTOR_ONE_MINUS_SRC1_COLOR,
    /// ename:VK_BLEND_FACTOR_SRC1_ALPHA, or
    /// ename:VK_BLEND_FACTOR_ONE_MINUS_SRC1_ALPHA * If the
    /// &lt;&lt;features-features-dualSrcBlend,dual source blending&gt;&gt;
    /// feature is not enabled, pname:dstColorBlendFactor must: not be
    /// ename:VK_BLEND_FACTOR_SRC1_COLOR, ename:VK_BLEND_FACTOR_ONE_MINUS_SRC1_COLOR,
    /// ename:VK_BLEND_FACTOR_SRC1_ALPHA, or
    /// ename:VK_BLEND_FACTOR_ONE_MINUS_SRC1_ALPHA * If the
    /// &lt;&lt;features-features-dualSrcBlend,dual source blending&gt;&gt;
    /// feature is not enabled, pname:srcAlphaBlendFactor must: not be
    /// ename:VK_BLEND_FACTOR_SRC1_COLOR, ename:VK_BLEND_FACTOR_ONE_MINUS_SRC1_COLOR,
    /// ename:VK_BLEND_FACTOR_SRC1_ALPHA, or
    /// ename:VK_BLEND_FACTOR_ONE_MINUS_SRC1_ALPHA * If the
    /// &lt;&lt;features-features-dualSrcBlend,dual source blending&gt;&gt;
    /// feature is not enabled, pname:dstAlphaBlendFactor must: not be
    /// ename:VK_BLEND_FACTOR_SRC1_COLOR, ename:VK_BLEND_FACTOR_ONE_MINUS_SRC1_COLOR,
    /// ename:VK_BLEND_FACTOR_SRC1_ALPHA, or
    /// ename:VK_BLEND_FACTOR_ONE_MINUS_SRC1_ALPHA ****
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct PipelineColorBlendAttachmentState
    {
        /// <summary>
        /// 
        /// </summary>
        public PipelineColorBlendAttachmentState(Bool32 blendEnable, BlendFactor sourceColorBlendFactor, BlendFactor destinationColorBlendFactor, BlendOp colorBlendOp, BlendFactor sourceAlphaBlendFactor, BlendFactor destinationAlphaBlendFactor, BlendOp alphaBlendOp, ColorComponentFlags colorWriteMask)
        {
            this.BlendEnable = blendEnable;
            this.SourceColorBlendFactor = sourceColorBlendFactor;
            this.DestinationColorBlendFactor = destinationColorBlendFactor;
            this.ColorBlendOp = colorBlendOp;
            this.SourceAlphaBlendFactor = sourceAlphaBlendFactor;
            this.DestinationAlphaBlendFactor = destinationAlphaBlendFactor;
            this.AlphaBlendOp = alphaBlendOp;
            this.ColorWriteMask = colorWriteMask;
        }
        
        /// <summary>
        /// pname:blendEnable controls whether blending is enabled for the
        /// corresponding color attachment. If blending is not enabled, the
        /// source fragment's color for that attachment is passed through
        /// unmodified.
        /// </summary>
        public Bool32 BlendEnable; 
        
        /// <summary>
        /// pname:srcColorBlendFactor selects which blend factor is used to
        /// determine the source factors [eq]#(S~r~,S~g~,S~b~)#.
        /// </summary>
        public BlendFactor SourceColorBlendFactor; 
        
        /// <summary>
        /// pname:dstColorBlendFactor selects which blend factor is used to
        /// determine the destination factors [eq]#(D~r~,D~g~,D~b~)#.
        /// </summary>
        public BlendFactor DestinationColorBlendFactor; 
        
        /// <summary>
        /// pname:colorBlendOp selects which blend operation is used to
        /// calculate the RGB values to write to the color attachment.
        /// </summary>
        public BlendOp ColorBlendOp; 
        
        /// <summary>
        /// pname:srcAlphaBlendFactor selects which blend factor is used to
        /// determine the source factor [eq]#S~a~#.
        /// </summary>
        public BlendFactor SourceAlphaBlendFactor; 
        
        /// <summary>
        /// pname:dstAlphaBlendFactor selects which blend factor is used to
        /// determine the destination factor [eq]#D~a~#.
        /// </summary>
        public BlendFactor DestinationAlphaBlendFactor; 
        
        /// <summary>
        /// pname:alphaBlendOp selects which blend operation is use to
        /// calculate the alpha values to write to the color attachment.
        /// </summary>
        public BlendOp AlphaBlendOp; 
        
        /// <summary>
        /// pname:colorWriteMask is a bitmask selecting which of the R, G, B,
        /// and/or A components are enabled for writing, as described later in
        /// this chapter.
        /// </summary>
        public ColorComponentFlags ColorWriteMask; 
        
        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("PipelineColorBlendAttachmentState");
            builder.AppendLine("{");
            builder.AppendLine($"BlendEnable: {this.BlendEnable}");
            builder.AppendLine($"SourceColorBlendFactor: {this.SourceColorBlendFactor}");
            builder.AppendLine($"DestinationColorBlendFactor: {this.DestinationColorBlendFactor}");
            builder.AppendLine($"ColorBlendOp: {this.ColorBlendOp}");
            builder.AppendLine($"SourceAlphaBlendFactor: {this.SourceAlphaBlendFactor}");
            builder.AppendLine($"DestinationAlphaBlendFactor: {this.DestinationAlphaBlendFactor}");
            builder.AppendLine($"AlphaBlendOp: {this.AlphaBlendOp}");
            builder.AppendLine($"ColorWriteMask: {this.ColorWriteMask}");
            builder.Append("}");
            return builder.ToString();
        }
    }
}
