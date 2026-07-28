using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator;

public static class ProtoPropertyExtensions
{
    extension(ProtoProperty src)
    {
        public bool IsMyId => src.Annotations.Has<MyIdPropertyAnnotation>();
        public bool IsResourceId => src.Annotations.Has<ResourceIdPropertyAnnotation>();
        public bool IdRestrictedId => src.Annotations.Has<RestrictedIdPropertyAnnotation>();
    }
}