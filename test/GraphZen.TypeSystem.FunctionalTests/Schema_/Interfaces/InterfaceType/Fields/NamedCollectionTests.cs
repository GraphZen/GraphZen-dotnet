// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.Fields
{
    [NoReorder]
    public class NamedCollectionTests
    {
        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Field("foo", "String"); });
            schema.GetInterface("Foo").HasField("foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
                Action add = () => foo.Field(null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("{name}")]
        [InlineData("sdfa asf")]
        [InlineData("sdf*(#&aasf")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
                Action add = () => foo.Field(name, "Bar");
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot create field named \"{name}\" on interface Foo: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Field("foo", "String", f => f.Name("bar")); });
            var foo = schema.GetInterface("Foo");
            foo.HasField("foo").Should().BeFalse();
            foo.HasField("bar").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_removed))]
        [Fact]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Field("field", "String").RemoveField("field"); });
            schema.GetInterface("Foo").HasField("field").Should().BeFalse();
        }


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo").Field("field", "String");
                Action remove = () => foo.RemoveField(null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }
    }
}