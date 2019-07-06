// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel.Internal.Parser
{
    public class DirectiveDefinitionParsingTests : ParserTestBase
    {
        [Fact]
        public void InlineDirectiveDefinition()
        {
            var result = ParseDocument("directive @skip(if: Boolean!) on FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT");
            var expected = SyntaxFactory.Document(new DirectiveDefinitionSyntax(
                SyntaxFactory.Name(
                    "skip"), SyntaxFactory.Names("FIELD", "FRAGMENT_SPREAD", "INLINE_FRAGMENT"),
                null,
                new[]
                {
                    SyntaxFactory.InputValueDefinition(
                        SyntaxFactory.Name("if"),
                        SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("Boolean"))))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void Multiline()
        {
            var result = ParseDocument(@"
directive @include(if: Boolean!)
  on FIELD
   | FRAGMENT_SPREAD
   | INLINE_FRAGMENT
");
            var expected = SyntaxFactory.Document(new DirectiveDefinitionSyntax(
                SyntaxFactory.Name(
                    "include"),
                SyntaxFactory.Names("FIELD", "FRAGMENT_SPREAD", "INLINE_FRAGMENT"),
                null,
                new[]
                {
                    SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("if"),
                        SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("Boolean"))))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }

        [Fact]
        public void MultilinePipePrefix()
        {
            var result = ParseDocument(@"
directive @include2(if: Boolean!) on 
   | FIELD
   | FRAGMENT_SPREAD
   | INLINE_FRAGMENT
");
            var expected = SyntaxFactory.Document(new DirectiveDefinitionSyntax(SyntaxFactory.Name("include2"),
                SyntaxFactory.Names("FIELD", "FRAGMENT_SPREAD", "INLINE_FRAGMENT"),
                null,
                new[]
                {
                    SyntaxFactory.InputValueDefinition(SyntaxFactory.Name("if"),
                        SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("Boolean"))))
                }));
            Assert.Equal(expected, result);
            Assert.Equal(expected, PrintAndParse(result));
        }
    }
}