using NewParentDefinitions;
using OriginalParentDefinitions;
using ParentMutator;

[assembly: MutateParent(typeof(ExplicitInterfaceOverride), typeof(NewExplicitInterfaceOverride))]

namespace ChildDefinitions
{
    public class ChildExplicitInterfaceOverride : ExplicitInterfaceOverride
    {
        public string GetTypeName() => ((IExplicitInterface)this).TypeName;
    }
}
