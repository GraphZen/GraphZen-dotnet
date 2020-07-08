// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract class InputValueDefinition : AnnotatableMemberDefinition, IMutableInputValueDefinition
    {
        private ConfigurationSource? _defaultValueConfigurationSource;
        protected ConfigurationSource NameConfigurationSource;

        protected InputValueDefinition(
            string name,
            ConfigurationSource nameConfigurationSource,
            TypeIdentity typeIdentity,
            TypeSyntax typeSyntax,
            SchemaDefinition schema,
            ConfigurationSource configurationSource,
            object? clrInfo, IMemberDefinition declaringMember) : base(configurationSource)
        {
            ClrInfo = clrInfo;
            Schema = schema;
            DeclaringMember = declaringMember;

            NameConfigurationSource = nameConfigurationSource;
            Name = name;
            TypeReference = this is ArgumentDefinition
                ? (TypeReference)new ArgumentTypeReference(typeIdentity, typeSyntax, this)
                : new FieldTypeReference(typeIdentity, typeSyntax, this);
        }

        public override SchemaDefinition Schema { get; }


        public IMemberDefinition DeclaringMember { get; }

        public bool SetDefaultValue(object? value, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_defaultValueConfigurationSource))
            {
                return false;
            }

            DefaultValue = value;
            HasDefaultValue = true;
            _defaultValueConfigurationSource = configurationSource;
            return true;
        }

        public bool RemoveDefaultValue(ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_defaultValueConfigurationSource))
            {
                return false;
            }

            DefaultValue = null;
            HasDefaultValue = false;
            _defaultValueConfigurationSource = configurationSource;
            return true;
        }

        public ConfigurationSource? GetDefaultValueConfigurationSource() => _defaultValueConfigurationSource;
        public object? DefaultValue { get; private set; }
        public bool HasDefaultValue { get; private set; }
        public string Name { get; protected set; }
        public abstract bool SetName(string name, ConfigurationSource configurationSource);
        public ConfigurationSource GetNameConfigurationSource() => NameConfigurationSource;
        public object? ClrInfo { get; }

        public ConfigurationSource GetTypeReferenceConfigurationSource() =>
            TypeReference.GetTypeReferenceConfigurationSource();

        public TypeReference TypeReference { get; }

        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => TypeReference;

        public override string ToString()
        {
            var grandparent = DeclaringMember.GetParentMember();
            return grandparent is null || grandparent is SchemaDefinition
                ? $"{DeclaringMember.GetTypeDisplayName()} {this.GetTypeDisplayName()} {DeclaringMember.GetName()}.{Name}"
                : $"{grandparent.GetTypeDisplayName()} {DeclaringMember.GetTypeDisplayName()} {this.GetTypeDisplayName()} {grandparent.GetName()}.{DeclaringMember.GetName()}.{Name}";
        }
    }
}