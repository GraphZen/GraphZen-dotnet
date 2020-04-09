﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

[assembly: InternalsVisibleTo("LINQPadQuery")]
namespace GraphZen.Infrastructure
{
    [AttributeUsage(AttributeTargets.Constructor)]
    internal class GenFactoryAttribute : Attribute
    {
        public string FactoryClassName { get; }

        public GenFactoryAttribute(string factoryClassName)
        {
            FactoryClassName = factoryClassName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal class GenAccessorExtensionsAttribute : Attribute
    {
        public string ItemName { get; }
        public GenAccessorExtensionsAttribute(string itemName)
        {
            ItemName = itemName;
        }
    }
}