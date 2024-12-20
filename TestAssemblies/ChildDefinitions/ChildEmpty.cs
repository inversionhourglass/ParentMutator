using OriginalParentDefinitions;
using NewParentDefinitions;

[assembly: ParentMutator.MutateParent(typeof(Empty), typeof(NewEmpty))]

namespace ChildDefinitions;

public class ChildEmpty : Empty
{
}
