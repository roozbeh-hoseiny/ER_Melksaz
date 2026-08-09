using ConsoleApp1.AppCore;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1;

internal class ServiceWorker : BackgroundService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IIdentityReadRepository _identityReadRepository;
    private readonly INotificationService _notificationService;
    private readonly IEnumerable<INotificationService> _notificationServices;
    private readonly ILogger<ServiceWorker> _logger;

    public ServiceWorker(
        IIdentityUnitOfWork identityUnitOfWork,
        IIdentityReadRepository identityReadRepository,
        //INotificationService notificationService,
        //IEnumerable<INotificationService> notificationServices,
        ILogger<ServiceWorker> logger)
    {
        this._identityUnitOfWork = identityUnitOfWork;
        this._identityReadRepository = identityReadRepository;
        //this._notificationService = notificationService;
        //this._notificationServices = notificationServices;
        this._logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.Clear();

        Guid x = default;
        this._logger.LogInformation(x.ToString());

        //this._notificationService.Send("roozbeh");
        //Console.WriteLine(new string('-', 30));
        //foreach (var s in this._notificationServices)
        //{
        //    s.Send("Ehsan");
        //}

        //var users = await this._identityReadRepository.GetUsers(stoppingToken).ConfigureAwait(false);
        //foreach (var u in users)
        //{
        //    Console.WriteLine($"{u.Id} : {u.FirstName} {u.LastName} : {u.NationalCode}: {u.Mobile}: {u.Email}");
        //}

        //var user = await PasswordHash.Create("Ehsan@123456@")
        //    .Map(password => User.Create(
        //        FirstName.CreateUnsafe("احسان"),
        //        LastName.CreateUnsafe("شایان"),
        //        NationalCode.CreateUnsafe("0100000010"),
        //        Mobile.CreateUnsafe("09126666666"),
        //        Email.CreateUnsafe("ehsan2.shayan@gmail.com"),
        //        Username.CreateUnsafe("ehsan2.shayan"),
        //        password))
        //    .Map(newUser => this._identityUnitOfWork.WriteRepo.AddUser(newUser).Map(() => newUser))
        //    .Map(newUser => this._identityUnitOfWork.SaveChangesWithResultAsync(CancellationToken.None).Map(_ => newUser))
        //    .ConfigureAwait(false);
    }
}