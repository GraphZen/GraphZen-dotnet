// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects.InputObjectType.Fields.InputField.Description
{
    [NoReorder]
    public abstract class DescriptionSpecsTests
    {
        [Spec(nameof(DescriptionSpecs.description_cannot_be_null))]
        [Fact(Skip = "TODO")]
        public void description_cannot_be_null_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(DescriptionSpecs.description_can_be_updated))]
        [Fact(Skip = "TODO")]
        public void description_can_be_updated_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(DescriptionSpecs.description_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void description_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move DescriptionSpecsTests into a separate file to start writing tests
    [NoReorder]
    public class DescriptionSpecsTestsScaffold
    {
    }
}
// Source Hash Code: 18125526192824806481