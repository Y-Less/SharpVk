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

namespace SharpVk
{
    /// <summary>
    /// -
    /// </summary>
    public struct ObjectTableIndexBufferEntry
    {
        /// <summary>
        /// -
        /// </summary>
        public ObjectEntryType Type
        {
            get;
            set;
        }
        
        /// <summary>
        /// -
        /// </summary>
        public ObjectEntryUsageFlags Flags
        {
            get;
            set;
        }
        
        /// <summary>
        /// -
        /// </summary>
        public Buffer Buffer
        {
            get;
            set;
        }
        
        internal unsafe Interop.ObjectTableIndexBufferEntry Pack()
        {
            Interop.ObjectTableIndexBufferEntry result = default(Interop.ObjectTableIndexBufferEntry);
            result.Buffer = this.Buffer?.Pack() ?? Interop.Buffer.Null;
            result.Type = this.Type;
            result.Flags = this.Flags;
            return result;
        }
        
        internal unsafe Interop.ObjectTableIndexBufferEntry* MarshalTo()
        {
            return (Interop.ObjectTableIndexBufferEntry*)Interop.HeapUtil.AllocateAndMarshal(this.Pack()).ToPointer();
        }
    }
}