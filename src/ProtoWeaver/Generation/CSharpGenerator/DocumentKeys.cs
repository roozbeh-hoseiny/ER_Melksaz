namespace ProtoWeaver.Generation.CSharpGenerator;

public static class DocumentKeys
{
    public static DocumentKey Service(string name) => new("Service", name);

    public static DocumentKey Message(string name) => new("Message", name);

    public static DocumentKey Method(
        string service,
        string method) => new("Method", $"{service}.{method}");

    public static DocumentKey Interface(string name) => new("Interface", name);

    public static DocumentKey Enum(string name)
            => new("Enum", name);
}
