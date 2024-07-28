using System.Runtime.InteropServices;

namespace NoStringEvaluating.Models.Values;

/// <summary>
/// Keeper for values
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
    /// DateTime value
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Boolean value
    /// </summary>
    public bool Boolean { get; set; }

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
