// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.LanguageModel
{
    public partial class NamedTypeSyntax
    {
        #region SyntaxNodeGenerator

        /// <summary>Empty, read-only list of <see cref="NamedTypeSyntax" /> nodes.</summary>
        public static IReadOnlyList<NamedTypeSyntax> EmptyList { get; } = ImmutableList<NamedTypeSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NamedTypeSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NamedTypeSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NamedTypeSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NamedTypeSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveNamedType(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.NamedType;

        #endregion
    }
}
// Source Hash Code: 17444776496748685786