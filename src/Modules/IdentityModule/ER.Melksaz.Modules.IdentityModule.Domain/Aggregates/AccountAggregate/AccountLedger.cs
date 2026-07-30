using ER.Melksaz.BuildingBlocks.Domain.Core;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.AccountAggregate;

public sealed class AccountLedger : DomainAggregateRootBase<int>
{
    public string Title { get; set; } = string.Empty;
    public AccountLedgerType AccountLevel { get; private set; } = null!;
    public AccountLedgerCode AccountCode { get; private set; } = null!;
    public int? ParentId { get; private set; }

    // Persistence fields
    private string GroupCode { get; } = string.Empty;
    private string GeneralCode { get; } = string.Empty;
    private string SubsidiaryCode { get; } = string.Empty;

    private AccountLedger() : base(default) { }         // EF

    public static PrimitiveResult<AccountLedger> CreateGroup(
        string title,
        string code)
    {
        return new AccountLedger()
        {
            Title = title,
            AccountCode = GroupLedgerCode.Create(code),
            AccountLevel = AccountGroupLedger.Instance
        };
    }

    public PrimitiveResult<AccountLedger> CreateGeneral(
        string title,
        string code)
    {
        return new AccountLedger()
        {
            Title = title,
            AccountCode = GeneralLedgerCode.Create(
                code,
                this.AccountCode.As<GroupLedgerCode>()),
            AccountLevel = AccountGeneralLedger.Instance,
            ParentId = this.Id
        };
    }

    public PrimitiveResult<AccountLedger> CreateSubsidary(
        string title,
        string code)
    {
        return new AccountLedger()
        {
            Title = title,
            AccountCode = SubsidaryLedgerCode.Create(
                code,
                this.AccountCode.As<GeneralLedgerCode>()),
            AccountLevel = AccountGroupLedger.Instance,
            ParentId = this.Id
        };
    }

    public void LoadCode()
    {
        this.AccountCode =
            AccountLedgerCodeFactory.Create(
                this.AccountLevel,
                this.GroupCode,
                this.GeneralCode,
                this.SubsidiaryCode);
    }
}


public abstract record AccountLedgerCode
{
    public string Code { get; protected set; } = string.Empty;
    public abstract string FullCode { get; }

    public T As<T>() where T : AccountLedgerCode => (T)this;
}
public sealed record GroupLedgerCode : AccountLedgerCode
{
    public override string FullCode => this.Code;

    private GroupLedgerCode() { } // EF

    public static GroupLedgerCode Create(string code)
    {
        return new GroupLedgerCode()
        {
            Code = code
        };
    }
}
public sealed record GeneralLedgerCode : AccountLedgerCode
{
    public GroupLedgerCode GroupCode { get; private set; } = null!;
    public override string FullCode => $"{this.GroupCode.FullCode}{this.Code}";

    public static GeneralLedgerCode Create(string code, GroupLedgerCode group)
    {
        return new GeneralLedgerCode()
        {
            Code = code,
            GroupCode = group
        };
    }
}
public sealed record SubsidaryLedgerCode : AccountLedgerCode
{
    public GeneralLedgerCode GeneralCode { get; private set; } = null!;
    public override string FullCode => $"{this.GeneralCode.FullCode}{this.Code}";

    public static SubsidaryLedgerCode Create(string code, GeneralLedgerCode genral)
    {
        return new SubsidaryLedgerCode()
        {
            Code = code,
            GeneralCode = genral
        };
    }
}

public abstract record AccountLedgerType;
public sealed record AccountGroupLedger : AccountLedgerType
{
    public static readonly AccountGroupLedger Instance = new();
}
public sealed record AccountGeneralLedger : AccountLedgerType
{
    public static readonly AccountGeneralLedger Instance = new();
}
public sealed record AccountSubsidaryLedger : AccountLedgerType
{
    public static readonly AccountSubsidaryLedger Instance = new();
}
public static class AccountLedgerTypeExtensions
{
    extension(AccountLedgerType accountLedgerType)
    {
        public string GetDiscriminator() => accountLedgerType switch
        {
            AccountGroupLedger => "Group",
            AccountGeneralLedger => "General",
            AccountSubsidaryLedger => "Subsidary",
            _ => throw new InvalidOperationException($"Unknown account ledger level: {accountLedgerType.GetType().Name}")
        };
    }
    extension(string accountLedgerType)
    {
        public AccountLedgerType ToAccountLedgerType()
        {
            return accountLedgerType switch
            {
                "Group" => AccountGroupLedger.Instance,
                "General" => AccountGeneralLedger.Instance,
                "Subsidary" => AccountSubsidaryLedger.Instance,
                _ => throw new InvalidOperationException($"Unknown account ledger level: {accountLedgerType}")
            };
        }
    }
}
public static class AccountLedgerCodeFactory
{
    public static AccountLedgerCode Create(
        AccountLedgerType level,
        string group,
        string? general,
        string? subsidiary)
    {
        return level switch
        {
            AccountGroupLedger =>
                GroupLedgerCode.Create(group),


            AccountGeneralLedger =>
                GeneralLedgerCode.Create(
                    general!,
                    GroupLedgerCode.Create(group)),


            AccountSubsidaryLedger =>
                SubsidaryLedgerCode.Create(
                    subsidiary!,
                    GeneralLedgerCode.Create(
                        general!,
                        GroupLedgerCode.Create(group))),


            _ => throw new ArgumentOutOfRangeException()
        };
    }
}