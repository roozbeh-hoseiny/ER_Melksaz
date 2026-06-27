using ER.Melksaz.BuildingBlocks.Api;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Api.Features.Users.CreateUser;

internal sealed class CreateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            UserEndpoints.Instance.CreateUserEndpoint.Url.ToString(),
            async (
                CreateUserApiRequest req,
                IIdentityUnitOfWork identityUnitOfWork,
                IResultHandler resultHandler,
                CancellationToken cancellationToken) =>
            {
                var result = await PasswordHash.Create(req.password)
                    .Map(password => User.Create(
                        req.FirstName,
                        req.LastName,
                        req.NationalCode,
                        req.Mobile,
                        req.Email,
                        req.Username,
                        password))
                    .Map(newUser => identityUnitOfWork.WriteRepo.AddUser(newUser).Map(() => newUser))
                    .Map(newUser => identityUnitOfWork.SaveChangesWithResultAsync(CancellationToken.None).Map(_ => newUser))
                    .ConfigureAwait(false);
                return resultHandler.Handle(result, v => TypedResults.Created(v.Id));
            });
    }
}
public sealed record CreateUserApiRequest(
    FirstName FirstName,
    LastName LastName,
    NationalCode NationalCode,
    Mobile Mobile,
    Email Email,
    Username Username,
    string password);