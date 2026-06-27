using ER.Melksaz.BuildingBlocks.Api;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.PrimitiveResults;
using Wolverine;

namespace ER.Melksaz.Modules.IdentityModule.Api.Features.Users.CreateUser;

internal sealed class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            UserEndpoints.Instance.CreateUserEndpoint.Url.ToString(),
            async (
                CreateUserCommand req,
                IMessageBus bus,
                //IIdentityUnitOfWork identityUnitOfWork,
                IResultHandler resultHandler,
                CancellationToken cancellationToken) =>
            {
                var result = await bus.InvokeAsync<PrimitiveResult<User>>(req)
                    .ConfigureAwait(false);
                return resultHandler.Handle(result, v => TypedResults.Created(v.Id));
                //var result = await PasswordHash.Create(req.password)
                //    .Map(password => User.Create(
                //        FirstName.CreateUnsafe(req.FirstName),
                //        req.LastName,
                //        req.NationalCode,
                //        req.Mobile,
                //        req.Email ?? Email.Empty,
                //        req.Username,
                //        password))
                //    .Map(newUser => identityUnitOfWork.WriteRepo.AddUser(newUser).Map(() => newUser))
                //    .Map(newUser => identityUnitOfWork.SaveChangesWithResultAsync(CancellationToken.None).Map(_ => newUser))
                //    .ConfigureAwait(false);
                //return resultHandler.Handle(result, v => TypedResults.Created(v.Id));
            });
    }
}
internal sealed class CreateUserHandler
{
    public static async Task<PrimitiveResult<User>> Handle(
        CreateUserCommand command,
        IIdentityUnitOfWork identityUnitOfWork,
        CancellationToken cancellationToken)
    {
        return await PasswordHash.Create(command.password)
                    .Map(password => User.Create(
                        FirstName.CreateUnsafe(command.FirstName),
                        command.LastName,
                        command.NationalCode,
                        command.Mobile,
                        command.Email ?? Email.Empty,
                        command.Username,
                        password))
                    .Map(newUser => identityUnitOfWork.WriteRepo.AddUser(newUser).Map(() => newUser))
                    .Map(newUser => identityUnitOfWork.SaveChangesWithResultAsync(CancellationToken.None).Map(_ => newUser))
                    .ConfigureAwait(false);
    }
}
public sealed record CreateUserCommand(
    string FirstName,
    LastName LastName,
    NationalCode NationalCode,
    Mobile Mobile,
    Email? Email,
    Username Username,
    string password);