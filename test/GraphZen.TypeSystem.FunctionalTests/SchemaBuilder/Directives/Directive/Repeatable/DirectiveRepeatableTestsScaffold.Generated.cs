// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Directives.Directive.Repeatable
{
    [NoReorder]
    public abstract class DirectiveRepeatableTests
    {
        [Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
        [Fact]
        public void optional_item_can_be_removed()
        {
            // Priority: High
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(OptionalSpecs.parent_can_be_created_without))]
        [Fact]
        public void parent_can_be_created_without()
        {
            // Priority: High
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }
    }

// Move DirectiveRepeatableTests into a separate file to start writing tests
    [NoReorder]
    public class DirectiveRepeatableTestsScaffold
    {
    }
}