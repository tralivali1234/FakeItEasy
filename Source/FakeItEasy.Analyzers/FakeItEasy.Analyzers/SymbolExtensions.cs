using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace FakeItEasy.Analyzers
{
    public static class SymbolExtensions
    {
        public static string GetFullName(this INamedTypeSymbol type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var stack = new Stack<string>();
            stack.Push(type.Name);
            var tmp = type.ContainingType;
            while (tmp != null)
            {
                type = tmp;
                stack.Push(tmp.Name);
                tmp = tmp.ContainingType;
            }
            
            var ns = type.ContainingNamespace;
            while (!string.IsNullOrEmpty(ns?.Name))
            {
                stack.Push(ns.Name);
                ns = ns.ContainingNamespace;
            }
            return string.Join(".", stack);
        }
    }
}
