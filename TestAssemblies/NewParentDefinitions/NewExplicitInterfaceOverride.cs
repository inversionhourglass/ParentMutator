using OriginalParentDefinitions;

namespace NewParentDefinitions
{
    public class NewExplicitInterfaceOverride : ExplicitInterfaceOverride, IExplicitInterface
    {
        string IExplicitInterface.TypeName => nameof(NewExplicitInterfaceOverride);
    }
}
