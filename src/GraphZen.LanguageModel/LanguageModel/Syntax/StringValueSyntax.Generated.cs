// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public partial class StringValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="StringValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<StringValueSyntax> EmptyList { get; } = ImmutableList<StringValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="StringValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="StringValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="StringValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="StringValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveStringValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.StringValue;
    }
}