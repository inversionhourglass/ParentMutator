using OriginalParentDefinitions;

namespace NewParentDefinitions
{
    public class NewConstructor(string name) : Constructor(Format(name))
    {
        public static string Format(string name) => $"[changed] {name}";
    }
}
