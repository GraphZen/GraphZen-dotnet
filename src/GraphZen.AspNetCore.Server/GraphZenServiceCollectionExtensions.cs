// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen;
using GraphZen.Infrastructure;
using GraphZen.QueryEngine.Validation;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class GraphZenServiceCollectionExtensions
    {
        public static void AddGraphQLContext(
            this IServiceCollection serviceCollection,
            Action<GraphQLContextOptionsBuilder>? optionsAction = null)
        {
            AddGraphQLContext<GraphQLContext>(serviceCollection, optionsAction);
        }


        public static void AddGraphQLContext<TContext>(
            this IServiceCollection serviceCollection,
            Action<GraphQLContextOptionsBuilder>? optionsAction = null)
            where TContext : GraphQLContext
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            var optionsActionImpl =
                optionsAction != null
                    // ReSharper disable once ConstantConditionalAccessQualifier
                    ? (p, b) => { optionsAction?.Invoke(b); }
            : (Action<IServiceProvider, GraphQLContextOptionsBuilder>?)null;

            var contextType = typeof(TContext);
            if (optionsAction != null)
            {
                var declaredConstructors = contextType.GetTypeInfo().DeclaredConstructors.ToList();
                if (declaredConstructors.Count == 1 && declaredConstructors[0].GetParameters().Length == 0)
                    throw new ArgumentException(
                        $"{nameof(AddGraphQLContext)} was called with configuration, but the context type '{contextType}' only declares a parameterless constructor. This means that the configuration passed to {nameof(AddGraphQLContext)} will never be used. If configuration is passed to {nameof(AddGraphQLContext)}, then '{contextType}' should declare a constructor that accepts a {nameof(GraphQLContextOptions)}<{contextType.Name}> and must pass it to the base constructor for {nameof(GraphQLContext)}.");
            }

            serviceCollection.TryAdd(new ServiceDescriptor(typeof(GraphQLContextOptions<TContext>),
                p => GraphQLContextOptionsFactory<TContext>(p, optionsActionImpl),
                ServiceLifetime.Scoped));

            serviceCollection.Add(new ServiceDescriptor(typeof(GraphQLContextOptions),
                p => p.GetRequiredService<GraphQLContextOptions<TContext>>(), ServiceLifetime.Scoped));


            serviceCollection.TryAdd(new ServiceDescriptor(typeof(TContext),
                typeof(TContext), ServiceLifetime.Scoped));

            serviceCollection.TryAdd(new ServiceDescriptor(typeof(GraphQLContext),
                p => p.GetRequiredService<TContext>(),
                ServiceLifetime.Scoped));

            serviceCollection.TryAddScoped<IQueryValidator>(_ => new QueryValidator());


            var tempServiceProvider = serviceCollection.BuildServiceProvider();

            var context = tempServiceProvider.GetRequiredService<TContext>();
            // ReSharper disable once ConstantConditionalAccessQualifier
            var queryClrType = context.Schema.QueryType?.ClrType;
            if (queryClrType != null) serviceCollection.TryAddScoped(queryClrType);

            var mutationClrType = context.Schema.MutationType?.ClrType;
            if (mutationClrType != null) serviceCollection.TryAddScoped(mutationClrType);
        }

        private static GraphQLContextOptions<TContext> GraphQLContextOptionsFactory<TContext>(
            IServiceProvider serviceProvider,
            Action<IServiceProvider, GraphQLContextOptionsBuilder>? optionsAction)
            where TContext : GraphQLContext
        {
            var builder = new GraphQLContextOptionsBuilder<TContext>(new GraphQLContextOptions<TContext>());
            optionsAction?.Invoke(serviceProvider, builder);
            builder.UseApplicationServiceProvider(serviceProvider);
            return builder.Options;
        }
    }
}