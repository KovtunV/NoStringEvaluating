using System;

namespace NoStringEvaluating.Models.Values;

internal readonly struct ExtraTypeIdContainerReleaser : IDisposable
{
    public ExtraTypeIdContainer Container { get; }

    public ExtraTypeIdContainerReleaser(ExtraTypeIdContainer container)
    {
        Container = container;
    }

    public void Dispose()
    {
        Container.Release();
    }

    public static implicit operator ExtraTypeIdContainerReleaser(ExtraTypeIdContainer container)
    {
        return new ExtraTypeIdContainerReleaser(container);
    }

    public static implicit operator ExtraTypeIdContainer(ExtraTypeIdContainerReleaser releaser)
    {
        return releaser.Container;
    }
}

