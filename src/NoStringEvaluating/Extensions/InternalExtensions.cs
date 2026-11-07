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
            _ => false,
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

    /// <summary>
    /// Is the word at index, case insensitive and whole: not followed by a simple variable char
    /// </summary>
    public static bool IsWholeWordAt(this ReadOnlySpan<char> formula, int index, string word)
    {
        if (index + word.Length > formula.Length)
        {
            return false;
        }

        if (!formula.Slice(index, word.Length).Equals(word, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var endIndex = index + word.Length;
        return endIndex == formula.Length || !formula[endIndex].IsSimpleVariable();
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
