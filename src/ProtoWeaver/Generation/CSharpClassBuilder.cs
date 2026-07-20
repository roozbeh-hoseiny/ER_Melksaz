namespace ProtoWeaver.Generation;

public sealed class CSharpClassBuilder
{
    public string Namespace { get; private set; } = string.Empty;
    public string ClassName { get; private set; } = string.Empty;
    public string ClassDefenition { get; private set; } = string.Empty;

    public CSharpClassBuilder SetNamespace(string value)
    {
        this.Namespace = value;
        return this;
    }
    public CSharpClassBuilder SetClassName(string value)
    {
        this.ClassName = value;
        return this;
    }
    public CSharpClassBuilder SetClassDefenition(string value)
    {
        this.ClassDefenition = value;
        return this;
    }
}