// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums.EnumType.Name
{
    [NoReorder]
    public abstract class NameSpecsTests
    {
        [Spec(nameof(NameSpecs.name_cannot_be_null))]
        [Fact(Skip = "TODO")]
        public void name_cannot_be_null_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NameSpecs.name_cannot_be_duplicate))]
        [Fact(Skip = "TODO")]
        public void name_cannot_be_duplicate_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NameSpecs.can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void can_be_renamed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NameSpecs.name_must_be_valid_name))]
        [Fact(Skip = "TODO")]
        public void name_must_be_valid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move NameSpecsTests into a separate file to start writing tests
    [NoReorder]
    public class NameSpecsTestsScaffold
    {
    }
}
// Source Hash Code: 16406337866332260111