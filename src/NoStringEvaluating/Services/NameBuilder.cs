namespace NoStringEvaluating.Services;

/// <summary>
/// Name builder
/// </summary>
public struct NameBuilder(string expectedName)
{
    /// <summary>
    /// Expected name
    /// </summary>
    public string ExpectedName { get; private set; } = expectedName;

    /// <summary>
    /// Length
    /// </summary>
    public int Length { get; private set; } = 0;

    /// <summary>
    /// Is finished
    /// </summary>
    public readonly bool IsFinished => Length == ExpectedName.Length;

    /// <summary>
    /// Try remembed char
    /// </summary>
    public bool TryRemember(char ch)
    {
        if (Length >= ExpectedName.Length)
        {
            return false;
        }

        bool equal = char.ToUpperInvariant(ExpectedName[Length]) == char.ToUpperInvariant(ch);
        if (equal)
        {
            Length++;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Reset name
    /// </summary>
    public void Reset(string name)
    {
        ExpectedName = name;
        Length = 0;
    }
}
