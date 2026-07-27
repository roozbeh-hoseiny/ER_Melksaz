using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator;

internal sealed class MessageNameResolver : IMessageNameResolver
{
    private static readonly Dictionary<MessageKey, MessageName> _messages = [];
    public void Add(MessageKey key, MessageName value)
    {
        _messages.TryAdd(key, value);
    }

    public MessageName? Get(MessageKey key)
    {
        if (!_messages.TryGetValue(key, out var result)) return null;
        return result;
    }
    public MessageName? Create(ProtoMessage message)
    {
        var classAnnotation = message.Annotations.Get<CSharpClassAnnotation>();

        if (classAnnotation is null) return null;

        return new MessageName(classAnnotation.Namespace, classAnnotation.ClassName);
    }

    public MessageName? GetOrCreate(ProtoMessage message)
    {
        var result = this.Get(new MessageKey(
            message.Package,
            message.FullName));
        return result ?? this.Create(message);
    }

    public MessageName GetRequired(ProtoMessage message)
    {
        return this.GetOrCreate(message)
            ?? throw new InvalidOperationException(
                $"No C# type mapping has been registered for proto message '{message.FullName}'.");
    }
}