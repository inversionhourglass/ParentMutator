using NewParentDefinitions;
using OriginalParentDefinitions;
using ParentMutator;

[assembly: MutateParent(typeof(ExplicitInterface), typeof(NewExplicitInterface))]

namespace ChildDefinitions
{
    public class ChildExplicitInterface : ExplicitInterface
    {
        public string GetTypeName() => ((IExplicitInterface)this).TypeName;
    }
}
