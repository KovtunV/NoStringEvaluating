using System;
using System.Linq;
using NoStringEvaluating.Contract;
using NoStringEvaluating.Functions.Base;

namespace NoStringEvaluating;

/// <summary>
/// Add functions to reader from your assembly
/// </summary>
public static class NoStringFunctionsInitializer
{
    /// <summary>
    /// Initialize functions from assembly
    /// </summary>
    public static void InitializeFunctions(IFunctionReader functionReader, Type rootTypeForSearching, bool replace = false)
    {
        var funcInterfaceType = typeof(IFunction);

        var types = rootTypeForSearching.Assembly.GetTypes();
        var filteredTypes = types
            .Where(w => w.IsClass)
            .Where(w => !w.IsAbstract)
            .Where(w => funcInterfaceType.IsAssignableFrom(w))
            .ToArray();

        for (int i = 0; i < filteredTypes.Length; i++)
        {
            var func = (IFunction)Activator.CreateInstance(filteredTypes[i]);
            functionReader.AddFunction(func, replace);
        }
    }
}
