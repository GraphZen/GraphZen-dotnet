// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

#nullable disable


namespace GraphZen.LanguageModel.Internal
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#EnumTypeExtension
        /// </summary>
        private static TokenListParser<TokenKind, EnumTypeExtensionSyntax> EnumTypeExtension { get; } =
            (from extend in Keyword("extend")
             from @enum in Keyword("enum")
             from name in Name
             from directives in Directives.OptionalOrDefault()
             from values in EnumValuesDefinition
             select new EnumTypeExtensionSyntax(name, directives, values,
                 SyntaxLocation.FromMany(extend, name, directives.GetLocation(), values.GetLocation()))).Try()
            .Or((from extend in Keyword("extend")
                 from @enum in Keyword("enum")
                 from name in Name
                 from directives in Directives
                 select new EnumTypeExtensionSyntax(name, directives, null,
                     SyntaxLocation.FromMany(extend, name, directives.GetLocation()))).Try())
            .Try()
            .Named("enum type extension");
    }
}