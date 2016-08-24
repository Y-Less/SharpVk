﻿using SharpVk.Spirv;

namespace SharpVk.Shanq
{
    public abstract class Pointer<T>
        where T : struct
    {
        public Pointer(T value)
        {
            this.Value = value;
        }

        public T Value
        {
            get;
            private set;
        }
    }

    public class OutputPointer<T>
        : Pointer<T>
        where T : struct
    {
        public OutputPointer(T value)
            : base(value)
        {
        }

        public static StorageClass Storage => StorageClass.Output;
    }

    public class InputPointer<T>
        : Pointer<T>
        where T : struct
    {
        public InputPointer(T value)
            : base(value)
        {
        }

        public static StorageClass Storage => StorageClass.Input;
    }
}
