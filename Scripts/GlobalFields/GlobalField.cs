namespace Jamie.Framework
{
    public class GlobalField<T>
    {
        public string Name { get; }
        public GlobalField(string name) => Name = name;
    }
}