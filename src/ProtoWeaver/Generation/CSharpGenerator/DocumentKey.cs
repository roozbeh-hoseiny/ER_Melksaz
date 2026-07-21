namespace ProtoWeaver.Generation.CSharpGenerator;

public readonly record struct DocumentKey(
    string Category,
    string Name)
{
    public override string ToString() => $"{this.Category}:{this.Name}";
}
