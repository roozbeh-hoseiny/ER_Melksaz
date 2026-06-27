using ER.Melksaz.BuildingBlocks.Application.Security;
using ER.Melksaz.BuildingBlocks.Helpers;
using Microsoft.Extensions.Options;

namespace ER.Melksaz.BuildingBlocks.Infrastructure.Security;

public sealed class HasherServiceOptions
{
    public string HashKey { get; set; } = string.Empty;
}
public sealed class HasherService : IHasherService
{
    private readonly IOptionsMonitor<HasherServiceOptions> _opts;

    public HasherService(IOptionsMonitor<HasherServiceOptions> opts)
    {
        this._opts = opts;
    }
    public byte[] Hash(string val)
    {
        return StringHasherHelper.Get_HMACSHA256(val, this._opts.CurrentValue.HashKey);
    }
}
