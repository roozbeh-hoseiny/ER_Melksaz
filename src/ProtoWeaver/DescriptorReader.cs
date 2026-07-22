using Google.Protobuf.Reflection;
using ProtoWeaver.Models;

namespace ProtoWeaver;

public static class DescriptorReader
{
    public static ProtoModel Read(IEnumerable<FileDescriptor> descriptors)
    {
        ArgumentNullException.ThrowIfNull(descriptors);

        var model = new ProtoModel();

        //
        // Pass 1
        // همه Message ها را ثبت می‌کنیم تا Reference ها Resolve شوند.
        //
        foreach (var descriptor in descriptors)
        {
            RegisterMessages(
                descriptor.MessageTypes,
                descriptor,
                model);
        }

        //
        // Pass 2
        // Property ها
        //
        foreach (var descriptor in descriptors)
        {
            ReadMessages(
                descriptor.MessageTypes,
                model);
        }

        //
        // Pass 3
        // Service ها
        //
        foreach (var descriptor in descriptors)
        {
            ReadServices(
                descriptor,
                model);
        }

        return model;
    }

    private static void RegisterMessages(
        IEnumerable<MessageDescriptor> descriptors,
        FileDescriptor file,
        ProtoModel model)
    {
        foreach (var descriptor in descriptors)
        {
            model.Messages.Add(
                descriptor.FullName,
                new ProtoMessage
                {
                    Name = descriptor.Name,
                    FullName = descriptor.FullName,
                    Package = file.Package,
                    FileName = file.Name
                });

            //
            // Nested Message ها
            //
            RegisterMessages(
                descriptor.NestedTypes,
                file,
                model);
        }
    }

    private static void ReadMessages(
        IEnumerable<MessageDescriptor> descriptors,
        ProtoModel model)
    {
        foreach (var descriptor in descriptors)
        {
            ReadMessage(
                descriptor,
                model);

            ReadMessages(
                descriptor.NestedTypes,
                model);
        }
    }

    private static void ReadMessage(
        MessageDescriptor descriptor,
        ProtoModel model)
    {
        var message = model.Messages[descriptor.FullName];

        foreach (var field in descriptor.Fields.InDeclarationOrder())
        {
            message.Properties.Add(
                new ProtoProperty
                {
                    BaseMessage = message,

                    Name = ToPascalCase(field.Name),

                    ProtoName = field.Name,

                    FieldType = field.FieldType,

                    IsRepeated = field.IsRepeated,

                    IsNullable = field.HasPresence,

                    IsMessage = field.FieldType == FieldType.Message,

                    IsEnum = field.FieldType == FieldType.Enum,

                    IsPrimitive =
                        field.FieldType != FieldType.Message &&
                        field.FieldType != FieldType.Enum,


                    Message =
                        field.FieldType == FieldType.Message
                            ? model.Messages[field.MessageType.FullName]
                            : null,

                    EnumName =
                        field.FieldType == FieldType.Enum
                            ? field.EnumType.FullName
                            : null
                });
        }
    }

    private static void ReadServices(
        FileDescriptor descriptor,
        ProtoModel model)
    {
        foreach (var service in descriptor.Services)
        {
            var protoService =
                new ProtoService
                {
                    Name = service.Name,
                    Package = descriptor.Package
                };

            foreach (var method in service.Methods)
            {
                protoService.Methods.Add(
                    new ProtoMethod
                    {
                        Name = method.Name,

                        Request =
                            model.Messages[
                                method.InputType.FullName],

                        Response =
                            model.Messages[
                                method.OutputType.FullName],

                        ClientStreaming =
                            method.IsClientStreaming,

                        ServerStreaming =
                            method.IsServerStreaming
                    });
            }

            model.Services.Add(protoService);
        }
    }

    private static string ToPascalCase(
        string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        var parts = value.Split(
            '_',
            StringSplitOptions.RemoveEmptyEntries);

        return string.Concat(
            parts.Select(static part =>
                char.ToUpperInvariant(part[0]) +
                part.Substring(1)));
    }
}
