﻿using System.Collections.Generic;

namespace SharpVk.Generator.Generation
{
    public class StructDefinition
    {
        public string Name;
        public string[] Namespace;
        public bool IsUnsafe;
        public MethodDefinition Constructor;
        public List<MemberDefinition> Fields;
        public List<MemberDefinition> Properties;
    }
}