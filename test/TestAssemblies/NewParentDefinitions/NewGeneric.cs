using OriginalParentDefinitions;

namespace NewParentDefinitions
{
    public class NewGeneric<T> : Generic<T>
    {
        public override string TypeName => $"NewGeneric`{typeof(T).Name}";
    }
}
