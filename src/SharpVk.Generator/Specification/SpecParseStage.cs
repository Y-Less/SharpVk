﻿using Microsoft.Extensions.DependencyInjection;
using SharpVk.Generator.Pipeline;
using SharpVk.Generator.Specification.Rules;

namespace SharpVk.Generator.Specification
{
    public class SpecParseStage
        : IStage
    {
        private readonly IVkXmlCache xmlCache;

        public SpecParseStage(IVkXmlCache xmlCache)
        {
            this.xmlCache = xmlCache;
        }

        public void Configure(IServiceCollection services)
        {
            var specServices = new ServiceCollection();

            specServices.AddSingleton(this.xmlCache);
            specServices.AddSingleton<ExtensionSet>();
            specServices.AddSingleton<NameParser>();
            specServices.AddSingleton<ITypeExtensionRule, FunctionPointerTypeRule>();
            specServices.AddSingleton<ITypeExtensionRule, MemberTypeRule>();

            var specProvider = specServices.BuildServiceProvider();

            specProvider.CreateInstance<TypeElementReader>().ReadTo(services);
            specProvider.CreateInstance<EnumElementReader>().ReadTo(services);
            specProvider.CreateInstance<CommandElementReader>().ReadTo(services);
        }
    }
}