// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class SchemaBuilder : SchemaBuilder<GraphQLContext>
    {
        public SchemaBuilder()
        {

        }

        public SchemaBuilder(SchemaDefinition schema) : base(schema)
        {

        }
    }

    public partial class SchemaBuilder<TContext> : ISchemaBuilder<TContext> where TContext : GraphQLContext
    {

        public SchemaBuilder() : this(new SchemaDefinition())
        {

        }

        public SchemaBuilder(SchemaDefinition schema)
        {
            Builder = schema.Builder;
        }

        private InternalSchemaBuilder Builder { get; }

        public ScalarTypeBuilder<object, ValueSyntax> Scalar(string name) =>
            new ScalarTypeBuilder<object, ValueSyntax>(Builder.Scalar(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);


        public ScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>() where TScalar : notnull
        {
            var internalBuilder = Builder.Scalar(typeof(TScalar),
                ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<TScalar, ValueSyntax>(internalBuilder);
        }

        public ScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>(string name) where TScalar : notnull
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Scalar(typeof(TScalar), name, ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<TScalar, ValueSyntax>(internalBuilder);
        }

        public ScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Scalar(clrType, ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<object, ValueSyntax>(internalBuilder);
        }

        public ScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Scalar(clrType, name, ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<object, ValueSyntax>(internalBuilder);
        }

        public ScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax where TScalar : notnull
        {
            var internalBuilder = Builder.Scalar(typeof(TScalar), ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<TScalar, TValueNode>(internalBuilder);
        }

        public SchemaBuilder<TContext> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> QueryType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.QueryType(type, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> QueryType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.QueryType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> MutationType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.MutationType(type, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> MutationType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.MutationType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> SubscriptionType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.SubscriptionType(type, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public SchemaBuilder<TContext> UpdateOrAddDirectiveAnnotation(string name, object? value)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public SchemaBuilder<TContext> RemoveDirectiveAnnotations() => throw new NotImplementedException();

        InternalSchemaBuilder IInfrastructure<InternalSchemaBuilder>.Instance => Builder;

        SchemaDefinition IInfrastructure<SchemaDefinition>.Instance => Builder.Definition;


        public SchemaBuilder<TContext> DirectiveAnnotation(string name) => UpdateOrAddDirectiveAnnotation(name, null);

        public SchemaBuilder<TContext> DirectiveAnnotation(object directive) =>
            throw new NotImplementedException();

        public SchemaBuilder<TContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }
    }
}