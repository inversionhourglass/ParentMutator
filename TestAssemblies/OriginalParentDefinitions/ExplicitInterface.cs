namespace OriginalParentDefinitions
{
    public class ExplicitInterface : IExplicitInterface
    {
        string IExplicitInterface.TypeName => nameof(ExplicitInterface);
    }
}
