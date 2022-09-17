using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NoStringEvaluating.Models;

namespace NoStringEvaluating.Extensions;

internal static class InternalExtensions
{
    public static bool IsSimpleVariable(this char ch)
    {
        return char.IsLetterOrDigit(ch) || ch == '_' || ch == '.';
    }

    public static bool IsFloatingPointSymbol(this char ch)
    {
        return GlobalOptions.FloatingPointSymbol switch
        {
            FloatingPointSymbol.Dot => ch == '.',
            FloatingPointSymbol.Comma => ch == ',',
            FloatingPointSymbol.DotComma => ch == '.' || ch == ',',
            _ => false
        };
    }

    public static bool IsDigit(this char ch)
    {
        return char.IsDigit(ch);
    }

    public static bool IsWhiteSpace(this char ch)
    {
        return char.IsWhiteSpace(ch);
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action?.Invoke(item);
        }
    }

    public static IEnumerable<T> CreateInstances<T>(this IEnumerable<Assembly> sourceAssemblies)
        where T : class
    {
        return sourceAssemblies.SelectMany(x => CreateInstances<T>(x));
    }

    public static IEnumerable<T> CreateInstances<T>(this Assembly sourceAssembly)
        where T : class
    {
        var filteredTypes = sourceAssembly
            .GetTypes()
            .Where(w => w.IsClass)
            .Where(w => !w.IsAbstract)
            .Where(w => typeof(T).IsAssignableFrom(w))
            .ToArray();

        foreach (var typeToCreate in filteredTypes)
        {
            yield return (T)Activator.CreateInstance(typeToCreate);
        }
    }
}
