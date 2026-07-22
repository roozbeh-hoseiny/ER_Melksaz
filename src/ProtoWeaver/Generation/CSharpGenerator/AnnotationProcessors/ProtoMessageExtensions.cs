using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors;

public static class ProtoMessageExtensions
{
    extension(ProtoMessage src)
    {
        public bool IsSharedMessage => src.Annotations.GetDerived<MessageTypeBase>() is SharedMessageType;

    }
}