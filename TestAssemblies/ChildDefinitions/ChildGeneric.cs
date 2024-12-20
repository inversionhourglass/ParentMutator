using NewParentDefinitions;
using OriginalParentDefinitions;
using ParentMutator;

[assembly: MutateParent(typeof(Generic<>), typeof(NewGeneric<>))]

namespace ChildDefinitions
{
    public class ChildGeneric<T> : Generic<T>
    {
        public override string TypeName => "ChildGeneric";
    }

    public class ChildGenericDouble : Generic<double>
    {
        public override string TypeName => base.TypeName;
    }

    public class ChildGenericString : Generic<string>
    {
        public override string TypeName => base.TypeName;
    }
}
