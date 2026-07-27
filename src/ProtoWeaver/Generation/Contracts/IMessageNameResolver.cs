using ProtoWeaver.Generation.CSharpGenerator;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.Contracts;

public interface IMessageNameResolver
{
    void Add(MessageKey key, MessageName value);
    MessageName? GetOrCreate(ProtoMessage message);
    MessageName? Get(MessageKey key);
    MessageName? Create(ProtoMessage message);
    MessageName GetRequired(ProtoMessage message);

}
