using System.Runtime.InteropServices;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// Keeper for reference values (Word, WordList, NumberList, Object): an <see cref="InternalEvaluatorValue"/> holds its handle, so that the value struct
/// has no object reference of its own and goes on and off the evaluation stack without GC write barriers
/// </summary>
public sealed class ValueKeeper : IDisposable
{
    /// <summary>
    /// Keeper for values
    /// </summary>
    public ValueKeeper()
    {
        var handle = GCHandle.Alloc(this, GCHandleType.Normal);
        Ptr = GCHandle.ToIntPtr(handle);
    }

    /// <summary>
    /// Pointer to this object
    /// </summary>
    public IntPtr Ptr { get; }

    /// <summary>
    /// Word value
    /// </summary>
    public string Word { get; set; }

    /// <summary>
    /// WordList value
    /// </summary>
    public List<string> WordList { get; set; }

    /// <summary>
    /// NumberList value
    /// </summary>
    public List<double> NumberList { get; set; }

    /// <summary>
    /// Object value
    /// </summary>
    public object Object { get; set; }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        GCHandle.FromIntPtr(Ptr).Free();
    }

    /// <summary>
    /// Zero keeper
    /// </summary>
    public static ValueKeeper Zero { get; } = new();
}
