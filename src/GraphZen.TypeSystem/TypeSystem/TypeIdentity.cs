// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class TypeIdentity : IMutableNamed, IMutableClrType, IMutableDefinition
    {
        private static int _typeIdSeed = 1;
        private readonly TypeKind? _kind;
        private ConfigurationSource? _clrTypeConfigurationSource;

        private ConfigurationSource _nameConfigurationSource = ConfigurationSource.Explicit;


        private INamedTypeDefinition? _typeDefinition;


        public TypeIdentity(string name, SchemaDefinition schema, TypeKind? kind = null)
        {
            Name = name.IsValidGraphQLName()
                ? name
                : throw new InvalidNameException(
                    $"Cannot create Type Identity: \"{name}\" is not a valid GraphQL name.");
            Schema = schema;
            _kind = kind;
        }

        public TypeIdentity(Type clrType, SchemaDefinition schema, TypeKind? kind = null)
        {
            ClrType = clrType.GetEffectiveClrType();
            _clrTypeConfigurationSource = ConfigurationSource.Convention;
            Schema = schema;
            _kind = kind;
            if (ClrType.TryGetGraphQLNameFromDataAnnotation(out var annotated))
            {
                _nameConfigurationSource = ConfigurationSource.DataAnnotation;
                Name = annotated;
            }
            else
            {
                Name = ClrType.Name;
                _nameConfigurationSource = ConfigurationSource.Convention;
            }
        }

        public int Id { get; } = _typeIdSeed++;

        private SchemaDefinition Schema { get; }


        public INamedTypeDefinition? Definition
        {
            get => _typeDefinition;
            set
            {
                if (_typeDefinition != null)
                {
                    throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(Definition)} with value {value}, it's value has already been set with {_typeDefinition}.");
                }


                _typeDefinition =
                    value ?? throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(Definition)} to null.");
            }
        }

        public TypeKind? Kind => _typeDefinition?.Kind ?? _kind;



        public bool IsInputType()
        {
            if (Definition is IInputTypeDefinition)
            {
                return true;
            }

            var typeRefs = Schema.GetTypeReferences();
            return typeRefs
                .Any(_ => ReferenceEquals(_.Identity, this) && _.DeclaringMember is IInputDefinition);
        }

        public bool IsOutputType()
        {
            if (Definition is IOutputTypeDefinition)
            {
                return true;
            }

            return Schema.GetTypeReferences()
                .Any(_ => ReferenceEquals(_.Identity, this) && _.DeclaringMember is IOutputDefinition);
        }




        internal string DebuggerDisplay
        {
            get
            {
                if (Definition != null)
                {
                    return $"id: {Definition} ({Id})";
                }

                var input = IsInputType();
                var output = IsOutputType();
                var io = input && output ? "input/output" : input ? "input" : "output";
                return $"id: unknown {io} type {Name} ({Id})";
            }
        }


        public Type? ClrType { get; private set; }

        public bool SetClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetClrTypeConfigurationSource()))
            {
                return false;
            }

            if (Schema.TryGetType(clrType, out var existingTyped) && !existingTyped.Equals(Definition))
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotChangeClrType(this, clrType,
                        existingTyped));
            }

            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    $"Cannot set CLR type on {Definition} with custom name: the custom name \"{name}\" is not a valid GraphQL name.");
            }

            if (Schema.TryGetType(name, out var existingNamed) && !existingNamed.Equals(Definition))
            {
                throw new DuplicateItemException(
                    $"Cannot set CLR type on {Definition} with custom name: the custom name \"{name}\" conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named '{existingNamed.Name}'. All type names must be unique.");
            }

            SetName(name, configurationSource);
            return SetClrType(clrType, false, configurationSource);
        }

        public bool SetClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_clrTypeConfigurationSource))
            {
                return false;
            }

            if (Definition != null && Schema.TryGetType(clrType, out var existingTyped) &&
                !existingTyped.Equals(Definition))
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotChangeClrType(Definition, clrType,
                        existingTyped));
            }

            if (inferName)
            {
                if (clrType.TryGetGraphQLNameFromDataAnnotation(out var annotated))
                {
                    if (!annotated.IsValidGraphQLName())
                    {
                        throw new InvalidNameException(
                            $"Cannot set CLR type on {Definition} and infer name: the annotated name \"{annotated}\" on CLR {clrType.GetClrTypeKind()} '{clrType.Name}' is not a valid GraphQL name.");
                    }

                    if (Schema.TryGetType(annotated, out var existingNamed) && !existingNamed.Equals(Definition))
                    {
                        throw new DuplicateItemException(
                            $"Cannot set CLR type on {Definition} and infer name: the annotated name \"{annotated}\" on CLR {clrType.GetClrTypeKind()} '{clrType.Name}' conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named {existingNamed.Name}. All GraphQL type names must be unique.");
                    }

                    SetName(annotated, configurationSource);
                }
                else
                {
                    if (!clrType.Name.IsValidGraphQLName())
                    {
                        throw new InvalidNameException(
                            $"Cannot set CLR type on {Definition} and infer name: the CLR {clrType.GetClrTypeKind()} name '{clrType.Name}' is not a valid GraphQL name.");
                    }

                    if (Schema.TryGetType(clrType.Name, out var existingNamed) && !existingNamed.Equals(Definition))
                    {
                        throw new DuplicateItemException(
                            $"Cannot set CLR type on {Definition} and infer name: the CLR {clrType.GetClrTypeKind()} name '{clrType.Name}' conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named {existingNamed.Name}. All GraphQL type names must be unique.");
                    }

                    SetName(clrType.Name, configurationSource);
                }
            }

            _clrTypeConfigurationSource = configurationSource;
            ClrType = clrType;
            return true;
        }

        public bool RemoveClrType(ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_clrTypeConfigurationSource))
            {
                return false;
            }

            _clrTypeConfigurationSource = configurationSource;
            ClrType = null;
            return true;
        }

        public ConfigurationSource? GetClrTypeConfigurationSource() => _clrTypeConfigurationSource;
        public ConfigurationSource GetConfigurationSource() => throw new NotImplementedException();


        public string Name { get; private set; }

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_nameConfigurationSource))
            {
                return false;
            }

            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    $"Cannot rename {Definition}: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            }

            if (Definition == null)
            {
            }
            else if (Schema.TryGetType(name, out var existingName) &&
                     !existingName.Equals(Definition))
            {
                throw new DuplicateItemException(
                    $"Cannot rename {Definition} to \"{name}\": a type with that name ({existingName}) already exists. All GraphQL type names must be unique.");
            }

            if (Schema.TryGetTypeIdentity(name, out var existing) && !existing.Equals(this))
            {
                foreach (var typeReference in Schema.GetTypeReferences().Where(typeRef => typeRef.Identity.Equals(existing)))
                {
                    typeReference.Identity = this;
                }
                Schema.RemoveTypeIdentity(existing);
            }
            Schema.RemoveTypeIdentity(this);
            Name = name;
            _nameConfigurationSource = configurationSource;
            Schema.AddTypeIdentity(this);
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;


        private bool Equals(TypeIdentity other) => Overlaps(other);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((TypeIdentity)obj);
        }

        // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
        public override int GetHashCode() => base.GetHashCode();


        public bool Overlaps(TypeIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            if (ClrType != null && identity.ClrType != null)
            {
                if (IsInputType() && identity.IsInputType()
                    || IsOutputType() && identity.IsOutputType())
                {
                    return ClrType == identity.ClrType;
                }
            }

            return string.Equals(Name, identity.Name);
        }

        public override string ToString() => DebuggerDisplay;
    }
}