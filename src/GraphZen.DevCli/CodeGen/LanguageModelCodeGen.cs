// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class LanguageModelCodeGen
    {
        private static List<(string kind, string type)> NodeTypes { get; } = typeof(SyntaxNode).Assembly.GetTypes()
            .Where(typeof(SyntaxNode).IsAssignableFrom)
            .Where(_ => !_.IsAbstract)
            .OrderBy(_ => _.Name)
            .Select(_ => (_.Name.Replace("Syntax", ""), _.Name))
            .ToList();

        private const string LanguageModelNamespace = nameof(GraphZen) + "." + nameof(LanguageModel);


        public static void Generate()
        {
            GenSyntaxVisitors();
            GenSyntaxKind();
            GenSyntaxNodePartials();
        }

        private static void GenSyntaxVisitors()
        {
            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(LanguageModelNamespace, ns =>
            {
                ns.AbstractPartialClass(
                    "GraphQLSyntaxVisitor"
                    , @class =>
                    {
                        foreach (var (kind, nodeType) in NodeTypes)
                        {
                            @class.AppendLine($@"
     /// <summary>Called when the visitor enters a <see cref=""{nodeType}""/> node.</summary>
        public virtual void Enter{kind}( {nodeType} node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref=""{nodeType}""/> node.</summary>
        public virtual void Leave{kind}( {nodeType} node) => OnLeave(node);
");
                        }
                    });

                ns.AbstractPartialClass(
                    "GraphQLSyntaxVisitor<TResult>"
                    , @class =>
                    {
                        foreach (var (kind, nodeType) in NodeTypes)
                        {
                            @class.AppendLine($@"
     /// <summary>Called when the visitor enters a <see cref=""{nodeType}""/> node.</summary>
        public virtual TResult  Enter{kind}( {nodeType} node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref=""{nodeType}""/> node.</summary>
        public virtual TResult Leave{kind}( {nodeType} node) => OnLeave(node);
");
                        }
                    });
            });
            csharp.WriteToFile(nameof(LanguageModel), "GraphQLSyntaxVisitor");
        }

        private static void GenSyntaxKind()
        {
            var schema = Schema.Create(sb =>
            {
                var syntaxKind = sb.Enum("SyntaxKind");
                foreach (var (kind, type) in NodeTypes)
                {
                    syntaxKind.Value(kind,
                        v => v.Description($"Indicates an <see cref=\"{type}\"/> node."));
                }
            });

            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(LanguageModelNamespace, ns => { ns.Enum(schema.GetEnum("SyntaxKind")); });
            csharp.WriteToFile(nameof(LanguageModel), "SyntaxKind");
        }

        private static void GenSyntaxNodePartials()
        {
            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(LanguageModelNamespace, ns =>
            {
                foreach (var (kind, type) in NodeTypes)
                {
                    ns.PartialClass(type, @class =>
                    {
                        @class.AppendLine($@"
	    /// <summary>Empty, read-only list of <see cref=""{type}""/> nodes.</summary>
		public static IReadOnlyList<{type}> EmptyList {{get;}} = ImmutableList<{type}>.Empty; 
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor""/> enters a <see cref=""{type}""/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.Enter{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor""/> leaves a <see cref=""{type}""/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.Leave{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor{{TResult}}""/> enters a <see cref=""{type}""/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.Enter{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor{{TResult}}""/> leaves a <see cref=""{type}""/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.Leave{kind}(this);
		public override SyntaxKind Kind {{get;}} = SyntaxKind.{kind};	
");
                    });
                }
            });

            csharp.WriteToFile(nameof(LanguageModel), "SyntaxNode");
        }
    }
}