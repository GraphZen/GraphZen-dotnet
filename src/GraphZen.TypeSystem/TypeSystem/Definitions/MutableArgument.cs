// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [DisplayName("argument")]
    public class MutableArgument : MutableInputValue, IMutableArgument
    {
        public MutableArgument(string name,
            ConfigurationSource nameConfigurationSource,
            TypeIdentity typeIdentity,
            TypeSyntax typeSyntax,
            ConfigurationSource configurationSource, IMutableArguments declaringMember,
            ParameterInfo? clrInfo) : base(
            name, nameConfigurationSource,
            typeIdentity, typeSyntax,
            declaringMember.Schema, configurationSource, clrInfo, declaringMember)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptions.InvalidNameException.CannotCreateArgumentWithInvalidName(this, name));
            }

            InternalBuilder = new InternalArgumentBuilder(this);
            Builder = new ArgumentBuilder(InternalBuilder);
        }

        public IArgumentBuilder Builder {get;}

        private string DebuggerDisplay => $"argument {Name}";

        internal new InternalArgumentBuilder InternalBuilder { get; }
        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.ArgumentDefinition;

        public override bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!name.IsValidGraphQLName())
            {
                throw InvalidNameException.ForRename(this, name);
            }

            if (!configurationSource.Overrides(GetNameConfigurationSource()))
            {
                return false;
            }


            if (DeclaringMember.TryGetArgument(name, out var existing))
            {
                if (!existing.Equals(this))
                {
                    throw TypeSystemExceptions.DuplicateItemException.ForRename(this, name);
                }

                return true;
            }

            DeclaringMember.RemoveArgument(this);
            Name = name;
            NameConfigurationSource = configurationSource;
            DeclaringMember.AddArgument(this);
            return true;
        }

        IGraphQLTypeReference IArgument.ArgumentType => ArgumentType;

        public TypeReference ArgumentType => TypeReference;

        public new IMutableArguments DeclaringMember =>
            (IMutableArguments)base.DeclaringMember;

        public bool SetArgumentType(TypeIdentity identity, TypeSyntax syntax,
            ConfigurationSource configurationSource) => TypeReference.Update(identity, syntax, configurationSource);

        public bool SetArgumentType(string type, ConfigurationSource configurationSource) =>
            TypeReference.Update(type, configurationSource);

        public new ParameterInfo? ClrInfo => base.ClrInfo as ParameterInfo;
        IArguments IArgument.DeclaringMember => DeclaringMember;
    }
}