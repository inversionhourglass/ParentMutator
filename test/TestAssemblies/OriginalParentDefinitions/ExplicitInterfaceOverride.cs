namespace OriginalParentDefinitions
{
    public class ExplicitInterfaceOverride : IExplicitInterface
    {
        string IExplicitInterface.TypeName => nameof(ExplicitInterfaceOverride);
    }

    public interface IExplicitInterface
    {
        string TypeName { get; }
    }
}
