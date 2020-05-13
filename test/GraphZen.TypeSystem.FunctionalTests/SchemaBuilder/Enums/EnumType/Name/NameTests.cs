﻿using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.Name
{
    public class NameTests
    {
        [Spec(nameof(named_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("  ()(#$")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Enum("Foo");
                Action rename = () => foo.Name(name);
                rename.Should()
                    .Throw<InvalidNameException>()
                    .WithMessage(
                        @$"Cannot rename enum Foo. ""{name}"" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo");
                var bar = _.Enum("Bar");
                Action rename = () => bar.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename enum Bar to ""Foo"", enum Foo already exists. All GraphQL type names must be unique.");
            });
        }

        [Spec(nameof(named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Enum("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }
    }
}