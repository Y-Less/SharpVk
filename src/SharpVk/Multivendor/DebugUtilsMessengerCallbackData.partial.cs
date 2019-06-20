using System;

namespace SharpVk.Multivendor
{
    public partial struct DebugUtilsMessengerCallbackData
    {
        /// <summary>
        /// 
        /// </summary>
        internal static unsafe DebugUtilsMessengerCallbackData MarshalFrom(SharpVk.Interop.Multivendor.DebugUtilsMessengerCallbackData* pointer)
        {
            DebugUtilsMessengerCallbackData result = default(DebugUtilsMessengerCallbackData);
            result.Flags = pointer->Flags;
            result.MessageIdName = Interop.HeapUtil.MarshalStringFrom(pointer->MessageIdName);
            result.MessageIdNumber = pointer->MessageIdNumber;
            result.Message = Interop.HeapUtil.MarshalStringFrom(pointer->Message);

            if (pointer->QueueLabels == null)
            {
                result.QueueLabels = null;
            }
            else
            {
                result.QueueLabels = new DebugUtilsLabel[pointer->QueueLabelCount];
                for (int index = 0; index < (uint)(result.QueueLabels.Length); index++)
                {
                    result.QueueLabels[index] = SharpVk.Multivendor.DebugUtilsLabel.MarshalFrom(&pointer->QueueLabels[index]);
                }
            }

            if (pointer->CommandBufLabels == null)
            {
                result.CommandBufLabels = null;
            }
            else
            {
                result.CommandBufLabels = new DebugUtilsLabel[pointer->CommandBufLabelCount];
                for (int index = 0; index < (uint)(result.CommandBufLabels.Length); index++)
                {
                    result.CommandBufLabels[index] = SharpVk.Multivendor.DebugUtilsLabel.MarshalFrom(&pointer->CommandBufLabels[index]);
                }
            }

            if (pointer->Objects == null)
            {
                result.Objects = null;
            }
            else
            {
                result.Objects = new DebugUtilsObjectNameInfo[pointer->ObjectCount];
                for (int index = 0; index < (uint)(result.Objects.Length); index++)
                {
                    result.Objects[index] = SharpVk.Multivendor.DebugUtilsObjectNameInfo.MarshalFrom(&pointer->Objects[index]);
                }
            }

            return result;
        }
    }
}
