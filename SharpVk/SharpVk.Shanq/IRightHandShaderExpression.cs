﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpVk.Shanq
{
    public interface IRightHandShaderExpression
        : IShaderExpression
    {
        ShaderType ShaderType { get; }
    }
}