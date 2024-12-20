using NewParentDefinitions;
using OriginalParentDefinitions;
using ParentMutator;

[assembly: MutateParent(typeof(Constructor), typeof(NewConstructor))]

namespace ChildDefinitions
{
    public class ChildConstructor(string name) : Constructor(name)
    {
    }
}
