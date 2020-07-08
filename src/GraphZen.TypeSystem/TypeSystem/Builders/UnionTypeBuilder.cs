// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class UnionTypeBuilder : IUnionTypeBuilder
    {
        protected UnionTypeBuilder(InternalUnionTypeBuilder builder)
        {
            Builder = builder;
        }

        protected InternalUnionTypeBuilder Builder { get; }

        InternalUnionTypeBuilder IInfrastructure<InternalUnionTypeBuilder>.Instance => Builder;
        UnionTypeDefinition IInfrastructure<UnionTypeDefinition>.Instance => Builder.Definition;

        public INamedBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }
    }

    public class UnionTypeBuilder<TUnion, TContext> :
        UnionTypeBuilder,
        IUnionTypeBuilder<TUnion, TContext>
        where TContext : GraphQLContext
    {
        public UnionTypeBuilder(InternalUnionTypeBuilder builder) : base(builder)
        {
        }

        public new UnionTypeBuilder<TUnion, TContext> Description(string description) =>
            (UnionTypeBuilder<TUnion, TContext>)base.Description(description);


        public new UnionTypeBuilder<TUnion, TContext> RemoveDescription() =>
            (UnionTypeBuilder<TUnion, TContext>)base.RemoveDescription();

        public UnionTypeBuilder<TUnion, TContext> ResolveType(TypeResolver<TUnion, TContext> resolveTypeFn)
        {
            Check.NotNull(resolveTypeFn, nameof(resolveTypeFn));
            Builder.ResolveType((value, context, info) => resolveTypeFn((TUnion)value, (TContext)context, info));
            return this;
        }

        public new UnionTypeBuilder<object, TContext> ClrType(Type clrType, bool inferName = false)
        {
            base.ClrType(clrType, inferName);
            return new UnionTypeBuilder<object, TContext>(Builder);
        }

        public new UnionTypeBuilder<object, TContext> ClrType(Type clrType, string name)
        {
            base.ClrType(clrType, name);
            return new UnionTypeBuilder<object, TContext>(Builder);
        }

        public new UnionTypeBuilder<object, TContext> RemoveClrType()
        {
            base.RemoveClrType();
            return new UnionTypeBuilder<object, TContext>(Builder);
        }

        public UnionTypeBuilder<T, TContext> ClrType<T>(bool inferName = false) where T : notnull
        {
            base.ClrType(typeof(T), inferName);
            return new UnionTypeBuilder<T, TContext>(Builder);
        }

        public UnionTypeBuilder<T, TContext> ClrType<T>(string name) where T : notnull
        {
            base.ClrType(typeof(T), name);
            return new UnionTypeBuilder<T, TContext>(Builder);
        }

        public UnionTypeBuilder<TUnion, TContext> OfTypes(params string[] objectTypes)
        {
            Check.NotNull(objectTypes, nameof(objectTypes));
            foreach (var type in objectTypes)
            {
                Check.NotNull(type, nameof(type));
                Builder.IncludesType(type, ConfigurationSource.Explicit);
            }

            return this;
        }

        public UnionTypeBuilder<TUnion, TContext> OfTypes(params Type[] types)
        {
            // TODO: Check.NotEmpty(types, nameof(types));
            foreach (var type in types)
            {
                if (type != null)
                {
                    Builder.IncludesType(type, ConfigurationSource.Explicit);
                }
            }

            return this;
        }

        public UnionTypeBuilder<TUnion, TContext> OfTypes<TObject>()
        {
            Builder.IncludesType(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2>() => OfTypes(typeof(T1), typeof(T2));

        public UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3>() => OfTypes(typeof(T1), typeof(T2), typeof(T3));

        public UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

        public UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

        public UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6>() => OfTypes(typeof(T1), typeof(T2),
            typeof(T3), typeof(T4), typeof(T5), typeof(T6));

        public UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7>() => OfTypes(typeof(T1),
            typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));

        public UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7),
                typeof(T8),
                typeof(T9));

        public UnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7),
                typeof(T8),
                typeof(T9), typeof(T10));

        public new UnionTypeBuilder<TUnion, TContext> Name(string name) =>
            (UnionTypeBuilder<TUnion, TContext>)base.Name(name);


        public new UnionTypeBuilder<TUnion, TContext> AddDirectiveAnnotation(string name, object value) =>
            (UnionTypeBuilder<TUnion, TContext>)base.AddDirectiveAnnotation(name, value);


        public new UnionTypeBuilder<TUnion, TContext> AddDirectiveAnnotation(string name) =>
            (UnionTypeBuilder<TUnion, TContext>)base.AddDirectiveAnnotation(name);


        public new UnionTypeBuilder<TUnion, TContext> RemoveDirectiveAnnotations(string name) =>
            (UnionTypeBuilder<TUnion, TContext>)base.RemoveDirectiveAnnotations(name);

        public new UnionTypeBuilder<TUnion, TContext> ClearDirectiveAnnotations() =>
            (UnionTypeBuilder<TUnion, TContext>)base.ClearDirectiveAnnotations();
    }
}