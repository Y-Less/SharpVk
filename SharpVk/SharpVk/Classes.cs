﻿//The MIT License (MIT)
//
//Copyright (c) 2016 Andrew Armstrong/FacticiusVir
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;

namespace SharpVk
{

	public class AllocationCallbacks
	{

		public IntPtr UserData
		{
			get;
			set;
		}

        internal unsafe Interop.AllocationCallbacks Pack()
        {
            var result = new Interop.AllocationCallbacks();
			result.UserData = this.UserData.ToPointer();

            return result;
        }

		internal unsafe Interop.AllocationCallbacks* MarshalTo()
        {
            return (Interop.AllocationCallbacks*)Interop.HeapUtil.AllocateAndMarshal(this.Pack()).ToPointer();
		}
	}

	public class ApplicationInfo
	{

		public string ApplicationName
		{
			get;
			set;
		}

		public Version ApplicationVersion
		{
			get;
			set;
		}

		public string EngineName
		{
			get;
			set;
		}

		public Version EngineVersion
		{
			get;
			set;
		}

		public Version ApiVersion
		{
			get;
			set;
		}

        internal unsafe Interop.ApplicationInfo Pack()
        {
            var result = new Interop.ApplicationInfo();
			result.SType = StructureType.ApplicationInfo;
			result.ApplicationName = Interop.HeapUtil.MarshalTo(this.ApplicationName);
			result.EngineName = Interop.HeapUtil.MarshalTo(this.EngineName);
			result.ApplicationVersion = this.ApplicationVersion;
			result.EngineVersion = this.EngineVersion;
			result.ApiVersion = this.ApiVersion;

            return result;
        }

		internal unsafe Interop.ApplicationInfo* MarshalTo()
        {
            return (Interop.ApplicationInfo*)Interop.HeapUtil.AllocateAndMarshal(this.Pack()).ToPointer();
		}
	}

	public class BufferMemoryBarrier
	{

		public AccessFlags SourceAccessMask
		{
			get;
			set;
		}

		public AccessFlags DestinationAccessMask
		{
			get;
			set;
		}

		public uint SourceQueueFamilyIndex
		{
			get;
			set;
		}

		public uint DestinationQueueFamilyIndex
		{
			get;
			set;
		}

		public Buffer Buffer
		{
			get;
			set;
		}

		public ulong Offset
		{
			get;
			set;
		}

		public ulong Size
		{
			get;
			set;
		}

        internal unsafe Interop.BufferMemoryBarrier Pack()
        {
            var result = new Interop.BufferMemoryBarrier();
			result.SType = StructureType.BufferMemoryBarrier;
			result.Buffer = this.Buffer == null ? Interop.Buffer.Null : this.Buffer.MarshalTo();
			result.SourceAccessMask = this.SourceAccessMask;
			result.DestinationAccessMask = this.DestinationAccessMask;
			result.SourceQueueFamilyIndex = this.SourceQueueFamilyIndex;
			result.DestinationQueueFamilyIndex = this.DestinationQueueFamilyIndex;
			result.Offset = this.Offset;
			result.Size = this.Size;

            return result;
        }

		internal unsafe Interop.BufferMemoryBarrier* MarshalTo()
        {
            return (Interop.BufferMemoryBarrier*)Interop.HeapUtil.AllocateAndMarshal(this.Pack()).ToPointer();
		}
	}

	public class ImageMemoryBarrier
	{

		public AccessFlags SourceAccessMask
		{
			get;
			set;
		}

		public AccessFlags DestinationAccessMask
		{
			get;
			set;
		}

		public ImageLayout OldLayout
		{
			get;
			set;
		}

		public ImageLayout NewLayout
		{
			get;
			set;
		}

		public uint SourceQueueFamilyIndex
		{
			get;
			set;
		}

		public uint DestinationQueueFamilyIndex
		{
			get;
			set;
		}

		public Image Image
		{
			get;
			set;
		}

		public ImageSubresourceRange SubresourceRange
		{
			get;
			set;
		}

        internal unsafe Interop.ImageMemoryBarrier Pack()
        {
            var result = new Interop.ImageMemoryBarrier();
			result.SType = StructureType.ImageMemoryBarrier;
			result.Image = this.Image == null ? Interop.Image.Null : this.Image.MarshalTo();
			result.SourceAccessMask = this.SourceAccessMask;
			result.DestinationAccessMask = this.DestinationAccessMask;
			result.OldLayout = this.OldLayout;
			result.NewLayout = this.NewLayout;
			result.SourceQueueFamilyIndex = this.SourceQueueFamilyIndex;
			result.DestinationQueueFamilyIndex = this.DestinationQueueFamilyIndex;
			result.SubresourceRange = this.SubresourceRange;

            return result;
        }

		internal unsafe Interop.ImageMemoryBarrier* MarshalTo()
        {
            return (Interop.ImageMemoryBarrier*)Interop.HeapUtil.AllocateAndMarshal(this.Pack()).ToPointer();
		}
	}

	public class InstanceCreateInfo
	{

		public InstanceCreateFlags Flags
		{
			get;
			set;
		}

		public ApplicationInfo ApplicationInfo
		{
			get;
			set;
		}

		public string[] EnabledLayerNames
		{
			get;
			set;
		}

		public string[] EnabledExtensionNames
		{
			get;
			set;
		}

        internal unsafe Interop.InstanceCreateInfo Pack()
        {
            var result = new Interop.InstanceCreateInfo();
			result.SType = StructureType.InstanceCreateInfo;
			result.ApplicationInfo = this.ApplicationInfo == null ? null : this.ApplicationInfo.MarshalTo();
			result.EnabledLayerCount = this.EnabledLayerNames == null ? 0 : (uint)this.EnabledLayerNames.Length;
			result.EnabledLayerNames = this.EnabledLayerNames == null ? null : Interop.HeapUtil.MarshalTo(this.EnabledLayerNames);
			result.EnabledExtensionCount = this.EnabledExtensionNames == null ? 0 : (uint)this.EnabledExtensionNames.Length;
			result.EnabledExtensionNames = this.EnabledExtensionNames == null ? null : Interop.HeapUtil.MarshalTo(this.EnabledExtensionNames);
			result.Flags = this.Flags;

            return result;
        }

		internal unsafe Interop.InstanceCreateInfo* MarshalTo()
        {
            return (Interop.InstanceCreateInfo*)Interop.HeapUtil.AllocateAndMarshal(this.Pack()).ToPointer();
		}
	}

	public class MemoryBarrier
	{

		public AccessFlags SourceAccessMask
		{
			get;
			set;
		}

		public AccessFlags DestinationAccessMask
		{
			get;
			set;
		}

        internal unsafe Interop.MemoryBarrier Pack()
        {
            var result = new Interop.MemoryBarrier();
			result.SType = StructureType.MemoryBarrier;
			result.SourceAccessMask = this.SourceAccessMask;
			result.DestinationAccessMask = this.DestinationAccessMask;

            return result;
        }

		internal unsafe Interop.MemoryBarrier* MarshalTo()
        {
            return (Interop.MemoryBarrier*)Interop.HeapUtil.AllocateAndMarshal(this.Pack()).ToPointer();
		}
	}

	public class PhysicalDeviceLimits
	{

		public uint MaxImageDimension1D
		{
			get;
			set;
		}

		public uint MaxImageDimension2D
		{
			get;
			set;
		}

		public uint MaxImageDimension3D
		{
			get;
			set;
		}

		public uint MaxImageDimensionCube
		{
			get;
			set;
		}

		public uint MaxImageArrayLayers
		{
			get;
			set;
		}

		public uint MaxTexelBufferElements
		{
			get;
			set;
		}

		public uint MaxUniformBufferRange
		{
			get;
			set;
		}

		public uint MaxStorageBufferRange
		{
			get;
			set;
		}

		public uint MaxPushConstantsSize
		{
			get;
			set;
		}

		public uint MaxMemoryAllocationCount
		{
			get;
			set;
		}

		public uint MaxSamplerAllocationCount
		{
			get;
			set;
		}

		public ulong BufferImageGranularity
		{
			get;
			set;
		}

		public ulong SparseAddressSpaceSize
		{
			get;
			set;
		}

		public uint MaxBoundDescriptorSets
		{
			get;
			set;
		}

		public uint MaxPerStageDescriptorSamplers
		{
			get;
			set;
		}

		public uint MaxPerStageDescriptorUniformBuffers
		{
			get;
			set;
		}

		public uint MaxPerStageDescriptorStorageBuffers
		{
			get;
			set;
		}

		public uint MaxPerStageDescriptorSampledImages
		{
			get;
			set;
		}

		public uint MaxPerStageDescriptorStorageImages
		{
			get;
			set;
		}

		public uint MaxPerStageDescriptorInputAttachments
		{
			get;
			set;
		}

		public uint MaxPerStageResources
		{
			get;
			set;
		}

		public uint MaxDescriptorSetSamplers
		{
			get;
			set;
		}

		public uint MaxDescriptorSetUniformBuffers
		{
			get;
			set;
		}

		public uint MaxDescriptorSetUniformBuffersDynamic
		{
			get;
			set;
		}

		public uint MaxDescriptorSetStorageBuffers
		{
			get;
			set;
		}

		public uint MaxDescriptorSetStorageBuffersDynamic
		{
			get;
			set;
		}

		public uint MaxDescriptorSetSampledImages
		{
			get;
			set;
		}

		public uint MaxDescriptorSetStorageImages
		{
			get;
			set;
		}

		public uint MaxDescriptorSetInputAttachments
		{
			get;
			set;
		}

		public uint MaxVertexInputAttributes
		{
			get;
			set;
		}

		public uint MaxVertexInputBindings
		{
			get;
			set;
		}

		public uint MaxVertexInputAttributeOffset
		{
			get;
			set;
		}

		public uint MaxVertexInputBindingStride
		{
			get;
			set;
		}

		public uint MaxVertexOutputComponents
		{
			get;
			set;
		}

		public uint MaxTessellationGenerationLevel
		{
			get;
			set;
		}

		public uint MaxTessellationPatchSize
		{
			get;
			set;
		}

		public uint MaxTessellationControlPerVertexInputComponents
		{
			get;
			set;
		}

		public uint MaxTessellationControlPerVertexOutputComponents
		{
			get;
			set;
		}

		public uint MaxTessellationControlPerPatchOutputComponents
		{
			get;
			set;
		}

		public uint MaxTessellationControlTotalOutputComponents
		{
			get;
			set;
		}

		public uint MaxTessellationEvaluationInputComponents
		{
			get;
			set;
		}

		public uint MaxTessellationEvaluationOutputComponents
		{
			get;
			set;
		}

		public uint MaxGeometryShaderInvocations
		{
			get;
			set;
		}

		public uint MaxGeometryInputComponents
		{
			get;
			set;
		}

		public uint MaxGeometryOutputComponents
		{
			get;
			set;
		}

		public uint MaxGeometryOutputVertices
		{
			get;
			set;
		}

		public uint MaxGeometryTotalOutputComponents
		{
			get;
			set;
		}

		public uint MaxFragmentInputComponents
		{
			get;
			set;
		}

		public uint MaxFragmentOutputAttachments
		{
			get;
			set;
		}

		public uint MaxFragmentDualSourceAttachments
		{
			get;
			set;
		}

		public uint MaxFragmentCombinedOutputResources
		{
			get;
			set;
		}

		public uint MaxComputeSharedMemorySize
		{
			get;
			set;
		}

		public uint[] MaxComputeWorkGroupCount
		{
			get;
			set;
		}

		public uint MaxComputeWorkGroupInvocations
		{
			get;
			set;
		}

		public uint[] MaxComputeWorkGroupSize
		{
			get;
			set;
		}

		public uint SubPixelPrecisionBits
		{
			get;
			set;
		}

		public uint SubTexelPrecisionBits
		{
			get;
			set;
		}

		public uint MipmapPrecisionBits
		{
			get;
			set;
		}

		public uint MaxDrawIndexedIndexValue
		{
			get;
			set;
		}

		public uint MaxDrawIndirectCount
		{
			get;
			set;
		}

		public float MaxSamplerLodBias
		{
			get;
			set;
		}

		public float MaxSamplerAnisotropy
		{
			get;
			set;
		}

		public uint MaxViewports
		{
			get;
			set;
		}

		public uint[] MaxViewportDimensions
		{
			get;
			set;
		}

		public float[] ViewportBoundsRange
		{
			get;
			set;
		}

		public uint ViewportSubPixelBits
		{
			get;
			set;
		}

		public UIntPtr MinMemoryMapAlignment
		{
			get;
			set;
		}

		public ulong MinTexelBufferOffsetAlignment
		{
			get;
			set;
		}

		public ulong MinUniformBufferOffsetAlignment
		{
			get;
			set;
		}

		public ulong MinStorageBufferOffsetAlignment
		{
			get;
			set;
		}

		public int MinTexelOffset
		{
			get;
			set;
		}

		public uint MaxTexelOffset
		{
			get;
			set;
		}

		public int MinTexelGatherOffset
		{
			get;
			set;
		}

		public uint MaxTexelGatherOffset
		{
			get;
			set;
		}

		public float MinInterpolationOffset
		{
			get;
			set;
		}

		public float MaxInterpolationOffset
		{
			get;
			set;
		}

		public uint SubPixelInterpolationOffsetBits
		{
			get;
			set;
		}

		public uint MaxFramebufferWidth
		{
			get;
			set;
		}

		public uint MaxFramebufferHeight
		{
			get;
			set;
		}

		public uint MaxFramebufferLayers
		{
			get;
			set;
		}

		public SampleCountFlags FramebufferColorSampleCounts
		{
			get;
			set;
		}

		public SampleCountFlags FramebufferDepthSampleCounts
		{
			get;
			set;
		}

		public SampleCountFlags FramebufferStencilSampleCounts
		{
			get;
			set;
		}

		public SampleCountFlags FramebufferNoAttachmentsSampleCounts
		{
			get;
			set;
		}

		public uint MaxColorAttachments
		{
			get;
			set;
		}

		public SampleCountFlags SampledImageColorSampleCounts
		{
			get;
			set;
		}

		public SampleCountFlags SampledImageIntegerSampleCounts
		{
			get;
			set;
		}

		public SampleCountFlags SampledImageDepthSampleCounts
		{
			get;
			set;
		}

		public SampleCountFlags SampledImageStencilSampleCounts
		{
			get;
			set;
		}

		public SampleCountFlags StorageImageSampleCounts
		{
			get;
			set;
		}

		public uint MaxSampleMaskWords
		{
			get;
			set;
		}

		public Bool32 TimestampComputeAndGraphics
		{
			get;
			set;
		}

		public float TimestampPeriod
		{
			get;
			set;
		}

		public uint MaxClipDistances
		{
			get;
			set;
		}

		public uint MaxCullDistances
		{
			get;
			set;
		}

		public uint MaxCombinedClipAndCullDistances
		{
			get;
			set;
		}

		public uint DiscreteQueuePriorities
		{
			get;
			set;
		}

		public float[] PointSizeRange
		{
			get;
			set;
		}

		public float[] LineWidthRange
		{
			get;
			set;
		}

		public float PointSizeGranularity
		{
			get;
			set;
		}

		public float LineWidthGranularity
		{
			get;
			set;
		}

		public Bool32 StrictLines
		{
			get;
			set;
		}

		public Bool32 StandardSampleLocations
		{
			get;
			set;
		}

		public ulong OptimalBufferCopyOffsetAlignment
		{
			get;
			set;
		}

		public ulong OptimalBufferCopyRowPitchAlignment
		{
			get;
			set;
		}

		public ulong NonCoherentAtomSize
		{
			get;
			set;
		}

		internal unsafe PhysicalDeviceLimits Unpack(Interop.PhysicalDeviceLimits value)
		{
			return MarshalFrom(&value);
		}

		internal unsafe PhysicalDeviceLimits MarshalFrom(Interop.PhysicalDeviceLimits* value)
		{
            var result = new PhysicalDeviceLimits();

			result.MaxComputeWorkGroupCount = Interop.HeapUtil.MarshalFrom(value->MaxComputeWorkGroupCount, 3);
			result.MaxComputeWorkGroupSize = Interop.HeapUtil.MarshalFrom(value->MaxComputeWorkGroupSize, 3);
			result.MaxViewportDimensions = Interop.HeapUtil.MarshalFrom(value->MaxViewportDimensions, 2);
			result.ViewportBoundsRange = Interop.HeapUtil.MarshalFrom(value->ViewportBoundsRange, 2);
			result.PointSizeRange = Interop.HeapUtil.MarshalFrom(value->PointSizeRange, 2);
			result.LineWidthRange = Interop.HeapUtil.MarshalFrom(value->LineWidthRange, 2);
			result.MaxImageDimension1D = value->MaxImageDimension1D;
			result.MaxImageDimension2D = value->MaxImageDimension2D;
			result.MaxImageDimension3D = value->MaxImageDimension3D;
			result.MaxImageDimensionCube = value->MaxImageDimensionCube;
			result.MaxImageArrayLayers = value->MaxImageArrayLayers;
			result.MaxTexelBufferElements = value->MaxTexelBufferElements;
			result.MaxUniformBufferRange = value->MaxUniformBufferRange;
			result.MaxStorageBufferRange = value->MaxStorageBufferRange;
			result.MaxPushConstantsSize = value->MaxPushConstantsSize;
			result.MaxMemoryAllocationCount = value->MaxMemoryAllocationCount;
			result.MaxSamplerAllocationCount = value->MaxSamplerAllocationCount;
			result.BufferImageGranularity = value->BufferImageGranularity;
			result.SparseAddressSpaceSize = value->SparseAddressSpaceSize;
			result.MaxBoundDescriptorSets = value->MaxBoundDescriptorSets;
			result.MaxPerStageDescriptorSamplers = value->MaxPerStageDescriptorSamplers;
			result.MaxPerStageDescriptorUniformBuffers = value->MaxPerStageDescriptorUniformBuffers;
			result.MaxPerStageDescriptorStorageBuffers = value->MaxPerStageDescriptorStorageBuffers;
			result.MaxPerStageDescriptorSampledImages = value->MaxPerStageDescriptorSampledImages;
			result.MaxPerStageDescriptorStorageImages = value->MaxPerStageDescriptorStorageImages;
			result.MaxPerStageDescriptorInputAttachments = value->MaxPerStageDescriptorInputAttachments;
			result.MaxPerStageResources = value->MaxPerStageResources;
			result.MaxDescriptorSetSamplers = value->MaxDescriptorSetSamplers;
			result.MaxDescriptorSetUniformBuffers = value->MaxDescriptorSetUniformBuffers;
			result.MaxDescriptorSetUniformBuffersDynamic = value->MaxDescriptorSetUniformBuffersDynamic;
			result.MaxDescriptorSetStorageBuffers = value->MaxDescriptorSetStorageBuffers;
			result.MaxDescriptorSetStorageBuffersDynamic = value->MaxDescriptorSetStorageBuffersDynamic;
			result.MaxDescriptorSetSampledImages = value->MaxDescriptorSetSampledImages;
			result.MaxDescriptorSetStorageImages = value->MaxDescriptorSetStorageImages;
			result.MaxDescriptorSetInputAttachments = value->MaxDescriptorSetInputAttachments;
			result.MaxVertexInputAttributes = value->MaxVertexInputAttributes;
			result.MaxVertexInputBindings = value->MaxVertexInputBindings;
			result.MaxVertexInputAttributeOffset = value->MaxVertexInputAttributeOffset;
			result.MaxVertexInputBindingStride = value->MaxVertexInputBindingStride;
			result.MaxVertexOutputComponents = value->MaxVertexOutputComponents;
			result.MaxTessellationGenerationLevel = value->MaxTessellationGenerationLevel;
			result.MaxTessellationPatchSize = value->MaxTessellationPatchSize;
			result.MaxTessellationControlPerVertexInputComponents = value->MaxTessellationControlPerVertexInputComponents;
			result.MaxTessellationControlPerVertexOutputComponents = value->MaxTessellationControlPerVertexOutputComponents;
			result.MaxTessellationControlPerPatchOutputComponents = value->MaxTessellationControlPerPatchOutputComponents;
			result.MaxTessellationControlTotalOutputComponents = value->MaxTessellationControlTotalOutputComponents;
			result.MaxTessellationEvaluationInputComponents = value->MaxTessellationEvaluationInputComponents;
			result.MaxTessellationEvaluationOutputComponents = value->MaxTessellationEvaluationOutputComponents;
			result.MaxGeometryShaderInvocations = value->MaxGeometryShaderInvocations;
			result.MaxGeometryInputComponents = value->MaxGeometryInputComponents;
			result.MaxGeometryOutputComponents = value->MaxGeometryOutputComponents;
			result.MaxGeometryOutputVertices = value->MaxGeometryOutputVertices;
			result.MaxGeometryTotalOutputComponents = value->MaxGeometryTotalOutputComponents;
			result.MaxFragmentInputComponents = value->MaxFragmentInputComponents;
			result.MaxFragmentOutputAttachments = value->MaxFragmentOutputAttachments;
			result.MaxFragmentDualSourceAttachments = value->MaxFragmentDualSourceAttachments;
			result.MaxFragmentCombinedOutputResources = value->MaxFragmentCombinedOutputResources;
			result.MaxComputeSharedMemorySize = value->MaxComputeSharedMemorySize;
			result.MaxComputeWorkGroupInvocations = value->MaxComputeWorkGroupInvocations;
			result.SubPixelPrecisionBits = value->SubPixelPrecisionBits;
			result.SubTexelPrecisionBits = value->SubTexelPrecisionBits;
			result.MipmapPrecisionBits = value->MipmapPrecisionBits;
			result.MaxDrawIndexedIndexValue = value->MaxDrawIndexedIndexValue;
			result.MaxDrawIndirectCount = value->MaxDrawIndirectCount;
			result.MaxSamplerLodBias = value->MaxSamplerLodBias;
			result.MaxSamplerAnisotropy = value->MaxSamplerAnisotropy;
			result.MaxViewports = value->MaxViewports;
			result.ViewportSubPixelBits = value->ViewportSubPixelBits;
			result.MinMemoryMapAlignment = value->MinMemoryMapAlignment;
			result.MinTexelBufferOffsetAlignment = value->MinTexelBufferOffsetAlignment;
			result.MinUniformBufferOffsetAlignment = value->MinUniformBufferOffsetAlignment;
			result.MinStorageBufferOffsetAlignment = value->MinStorageBufferOffsetAlignment;
			result.MinTexelOffset = value->MinTexelOffset;
			result.MaxTexelOffset = value->MaxTexelOffset;
			result.MinTexelGatherOffset = value->MinTexelGatherOffset;
			result.MaxTexelGatherOffset = value->MaxTexelGatherOffset;
			result.MinInterpolationOffset = value->MinInterpolationOffset;
			result.MaxInterpolationOffset = value->MaxInterpolationOffset;
			result.SubPixelInterpolationOffsetBits = value->SubPixelInterpolationOffsetBits;
			result.MaxFramebufferWidth = value->MaxFramebufferWidth;
			result.MaxFramebufferHeight = value->MaxFramebufferHeight;
			result.MaxFramebufferLayers = value->MaxFramebufferLayers;
			result.FramebufferColorSampleCounts = value->FramebufferColorSampleCounts;
			result.FramebufferDepthSampleCounts = value->FramebufferDepthSampleCounts;
			result.FramebufferStencilSampleCounts = value->FramebufferStencilSampleCounts;
			result.FramebufferNoAttachmentsSampleCounts = value->FramebufferNoAttachmentsSampleCounts;
			result.MaxColorAttachments = value->MaxColorAttachments;
			result.SampledImageColorSampleCounts = value->SampledImageColorSampleCounts;
			result.SampledImageIntegerSampleCounts = value->SampledImageIntegerSampleCounts;
			result.SampledImageDepthSampleCounts = value->SampledImageDepthSampleCounts;
			result.SampledImageStencilSampleCounts = value->SampledImageStencilSampleCounts;
			result.StorageImageSampleCounts = value->StorageImageSampleCounts;
			result.MaxSampleMaskWords = value->MaxSampleMaskWords;
			result.TimestampComputeAndGraphics = value->TimestampComputeAndGraphics;
			result.TimestampPeriod = value->TimestampPeriod;
			result.MaxClipDistances = value->MaxClipDistances;
			result.MaxCullDistances = value->MaxCullDistances;
			result.MaxCombinedClipAndCullDistances = value->MaxCombinedClipAndCullDistances;
			result.DiscreteQueuePriorities = value->DiscreteQueuePriorities;
			result.PointSizeGranularity = value->PointSizeGranularity;
			result.LineWidthGranularity = value->LineWidthGranularity;
			result.StrictLines = value->StrictLines;
			result.StandardSampleLocations = value->StandardSampleLocations;
			result.OptimalBufferCopyOffsetAlignment = value->OptimalBufferCopyOffsetAlignment;
			result.OptimalBufferCopyRowPitchAlignment = value->OptimalBufferCopyRowPitchAlignment;
			result.NonCoherentAtomSize = value->NonCoherentAtomSize;

			return result;
		}
	}

	public class PhysicalDeviceProperties
	{

		public Version ApiVersion
		{
			get;
			set;
		}

		public Version DriverVersion
		{
			get;
			set;
		}

		public uint VendorID
		{
			get;
			set;
		}

		public uint DeviceID
		{
			get;
			set;
		}

		public PhysicalDeviceType DeviceType
		{
			get;
			set;
		}

		public string DeviceName
		{
			get;
			set;
		}

		public byte[] PipelineCacheUUID
		{
			get;
			set;
		}

		public PhysicalDeviceLimits Limits
		{
			get;
			set;
		}

		public PhysicalDeviceSparseProperties SparseProperties
		{
			get;
			set;
		}

		internal unsafe PhysicalDeviceProperties Unpack(Interop.PhysicalDeviceProperties value)
		{
			return MarshalFrom(&value);
		}

		internal unsafe PhysicalDeviceProperties MarshalFrom(Interop.PhysicalDeviceProperties* value)
		{
            var result = new PhysicalDeviceProperties();

			result.DeviceName = Interop.HeapUtil.MarshalFrom(value->DeviceName, 16);
			result.PipelineCacheUUID = Interop.HeapUtil.MarshalFrom(value->PipelineCacheUUID, 16);
			result.ApiVersion = value->ApiVersion;
			result.DriverVersion = value->DriverVersion;
			result.VendorID = value->VendorID;
			result.DeviceID = value->DeviceID;
			result.DeviceType = value->DeviceType;
			result.SparseProperties = value->SparseProperties;

			return result;
		}
	}
}