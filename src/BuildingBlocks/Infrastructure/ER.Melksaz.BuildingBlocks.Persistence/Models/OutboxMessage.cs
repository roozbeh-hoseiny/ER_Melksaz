using ER.Melksaz.BuildingBlocks.Domain.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ER.Melksaz.BuildingBlocks.Persistence.Models;

public sealed class OutboxMessage
{
    private static readonly JsonSerializerSettings _defaultJsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        SerializationBinder = new OutboxMessageSerializationBinder(),
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    public string Id { get; private set; } = string.Empty;
    public string EventType { get; private set; } = string.Empty;
    public string Event { get; private set; } = string.Empty;
    public DateTimeOffset OccuredOn { get; private set; }
    public bool IsIntegrationEvent { get; private set; }
    public DateTimeOffset? ProcessedOn { get; private set; }
    public string Error { get; private set; } = string.Empty;

    private OutboxMessage() { }

    public static OutboxMessage Create(
        IDomainEvent @event,
        DateTimeOffset timestamp)
    {
        return new OutboxMessage()
        {
            Id = Ulid.NewUlid(timestamp).ToString(),
            EventType = @event.GetType()!.AssemblyQualifiedName!,
            Event = Newtonsoft.Json.JsonConvert.SerializeObject(@event, _defaultJsonSerializerSettings),
            OccuredOn = DateTimeOffset.Now,
            IsIntegrationEvent = @event.IsIntegrationEvent
        };
    }

    private sealed class OutboxMessageSerializationBinder : ISerializationBinder
    {
        private static readonly Dictionary<string, Type> Types =
            typeof(IDomainEvent)
                .Assembly
                .GetTypes()
                .Where(t => typeof(IDomainEvent).IsAssignableFrom(t))
                .ToDictionary(t => t.FullName!);

        public Type BindToType(
            string? assemblyName,
            string typeName)
        {
            if (Types.TryGetValue(typeName, out var type))
                return type;

            throw new JsonSerializationException(
                $"Type '{typeName}' is not allowed.");
        }

        public void BindToName(
            Type serializedType,
            out string? assemblyName,
            out string? typeName)
        {
            assemblyName = null;
            typeName = serializedType.FullName;
        }
    }
}
