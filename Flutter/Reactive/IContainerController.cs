using StructureMap;

namespace Flutter.Reactive
{
    public interface IContainerController
    {
        IContainer ScopedContainer { get; set; }
    }
}