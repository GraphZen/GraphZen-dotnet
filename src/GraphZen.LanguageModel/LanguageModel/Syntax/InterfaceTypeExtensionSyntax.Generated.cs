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
    public partial class InterfaceTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InterfaceTypeExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<InterfaceTypeExtensionSyntax> EmptyList { get; } =
            ImmutableList<InterfaceTypeExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInterfaceTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInterfaceTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InterfaceTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInterfaceTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InterfaceTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInterfaceTypeExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.InterfaceTypeExtension;
    }
}