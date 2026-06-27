using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.PrimitiveResults;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1;

internal class ServiceWorker : BackgroundService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IIdentityReadRepository _identityReadRepository;

    public ServiceWorker(
        IIdentityUnitOfWork identityUnitOfWork,
        IIdentityReadRepository identityReadRepository)
    {
        this._identityUnitOfWork = identityUnitOfWork;
        this._identityReadRepository = identityReadRepository;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.Clear();
        var users = await this._identityReadRepository.GetUsers(stoppingToken).ConfigureAwait(false);
        foreach (var u in users)
        {
            Console.WriteLine($"{u.Id} : {u.FirstName} {u.LastName} : {u.NationalCode}: {u.Mobile}: {u.Email}");
        }

        var user = await PasswordHash.Create("Ehsan@123456@")
            .Map(password => User.Create(
                FirstName.CreateUnsafe("احسان"),
                LastName.CreateUnsafe("شایان"),
                NationalCode.CreateUnsafe("0100000010"),
                Mobile.CreateUnsafe("09126666666"),
                Email.CreateUnsafe("ehsan2.shayan@gmail.com"),
                Username.CreateUnsafe("ehsan2.shayan"),
                password))
            .Map(newUser => this._identityUnitOfWork.WriteRepo.AddUser(newUser).Map(() => newUser))
            .Map(newUser => this._identityUnitOfWork.SaveChangesWithResultAsync(CancellationToken.None).Map(_ => newUser))
            .ConfigureAwait(false);

        Console.WriteLine(user.Error.Message);
    }
}