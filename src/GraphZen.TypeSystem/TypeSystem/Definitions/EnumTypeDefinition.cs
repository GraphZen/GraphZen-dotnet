// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [DisplayName("enum")]
    public partial class EnumTypeDefinition : NamedTypeDefinition, IMutableEnumTypeDefinition
    {
        private readonly Dictionary<string, ConfigurationSource> _ignoredValues =
            new Dictionary<string, ConfigurationSource>();

        internal readonly Dictionary<string, EnumValueDefinition> InternalValues =
            new Dictionary<string, EnumValueDefinition>();


        public EnumTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource)
            : base(identity, schema, configurationSource)
        {
            InternalBuilder = new InternalEnumTypeBuilder(this);
            Builder = new EnumTypeBuilder(InternalBuilder);
        }

        public override IEnumerable<IMemberDefinition> Children() => GetValues();


        private string DebuggerDisplay => $"enum {Name}";

        internal new InternalEnumTypeBuilder InternalBuilder { get; }
        public new EnumTypeBuilder Builder { get; }
        protected override INamedTypeBuilder GetBuilder() => Builder;

        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Enum;

        public override TypeKind Kind { get; } = TypeKind.Enum;

        [GenDictionaryAccessors(nameof(EnumValueDefinition.Name), nameof(EnumValueDefinition.Value))]
        public IReadOnlyDictionary<string, EnumValueDefinition> Values => InternalValues;

        public ConfigurationSource? FindIgnoredValueConfigurationSource(string name) =>
            _ignoredValues.TryGetValue(name, out var cs) ? cs : (ConfigurationSource?)null;

        public bool RemoveValue(EnumValueDefinition value)
        {
            InternalValues.Remove(value.Name);
            return true;
        }


        public bool IgnoreValue(string name, ConfigurationSource configurationSource)
        {
            var itemConfigurationSource = FindValue(name)?.GetConfigurationSource();
            if (configurationSource.Overrides(itemConfigurationSource))
            {
                var ignoredConfigurationSource = FindIgnoredValueConfigurationSource(name);
                if (configurationSource.Overrides(ignoredConfigurationSource))
                {
                    _ignoredValues[name] = configurationSource;
                    InternalValues.Remove(name);
                    return true;
                }
            }

            return false;
        }

        public bool UnignoreValue(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredValueConfigurationSource(name);
            if (!configurationSource.Overrides(ignoredConfigurationSource))
            {
                return false;
            }

            _ignoredValues.Remove(name);
            return true;
        }

        public EnumValueDefinition AddValue(string name, ConfigurationSource configurationSource,
            ConfigurationSource nameConfigurationSource)
        {
            var definition =
                new EnumValueDefinition(name, nameConfigurationSource, this, Schema, configurationSource);
            InternalValues[name] = definition;
            return definition;
        }

        public IEnumerable<EnumValueDefinition> GetValues() => Values.Values;

        IEnumerable<IEnumValueDefinition> IEnumValuesDefinition.GetValues() => GetValues();
    }
}