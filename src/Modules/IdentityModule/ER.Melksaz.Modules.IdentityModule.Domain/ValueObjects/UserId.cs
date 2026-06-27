using ER.Melksaz.BuildingBlocks.Helpers;

namespace ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

public readonly record struct UserId
{
    public static readonly UserId Empty = new UserId();

    public string Value { get; } = string.Empty;
    public UserId(string value) => this.Value = value;

    public static implicit operator string(UserId value) => value.Value;

    public static UserId Create() => new UserId(IdHelper.CreateNewUlid());

    public override string ToString() => this.Value ?? string.Empty;

    public static bool TryParse(string val, out UserId result)
    {
        result = UserId.Empty;
        try
        {
            result = new(val);
            return true;
        }
        catch { }
        return false;
    }
}