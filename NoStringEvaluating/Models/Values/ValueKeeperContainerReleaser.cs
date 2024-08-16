namespace NoStringEvaluating.Models.Values;

internal readonly struct ValueKeeperContainerReleaser(ValueKeeperContainer container) : IDisposable
{
    public ValueKeeperContainer Container { get; } = container;

    public void Dispose()
    {
        Container.Release();
    }

    public static implicit operator ValueKeeperContainerReleaser(ValueKeeperContainer container)
    {
        return new ValueKeeperContainerReleaser(container);
    }

    public static implicit operator ValueKeeperContainer(ValueKeeperContainerReleaser releaser)
    {
        return releaser.Container;
    }
}