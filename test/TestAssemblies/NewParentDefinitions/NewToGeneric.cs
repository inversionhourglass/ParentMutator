using OriginalParentDefinitions;
 
namespace NewParentDefinitions
{
    public class NewToGeneric<T> : ToGeneric
    {
        public override string TypeName => typeof(T).Name;
    }
}
