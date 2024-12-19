using NewParentDefinitions;
using OriginalParentDefinitions;
using ParentMutator;

[assembly: MutateParent(typeof(ToGeneric), typeof(NewToGeneric<string>))]

namespace ChildDefinitions
{
    public class ChildToGeneric : ToGeneric
    {
        public override string TypeName => base.TypeName;
    }
}
