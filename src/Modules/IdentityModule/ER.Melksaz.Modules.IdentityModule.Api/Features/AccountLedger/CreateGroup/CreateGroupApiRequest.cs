using ER.Melksaz.BuildingBlocks.Api;
using ER.Melksaz.Modules.IdentityModule.Application.Persistence;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Api.Features.AccountLedger.CreateGroup;

public sealed record CreateGroupApiRequest(string code, string title);
public sealed record CreateGeneralApiRequest(int groupId, string code, string title);
public sealed record CreateSubsidaryApiRequest(int generalId, string code, string title);
internal sealed class CreateGroupEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            AccountEndpoints.Instance.CreateGroupEndpoint.Url.ToString(),
            async (
                CreateGroupApiRequest req,
                IIdentityUnitOfWork identityUnitOfWork,
                IResultHandler resultHandler,
                CancellationToken cancellationToken) =>
            {
                var result = await Domain.Aggregates.AccountAggregate.AccountLedger.CreateGroup(
                    req.title,
                    req.code)
                .Map(acc => identityUnitOfWork.WriteRepo.AddAccount(acc).Map(() => acc))
                .Map(acc => identityUnitOfWork.SaveChangesWithResultAsync(CancellationToken.None).Map(_ => acc))
                    .ConfigureAwait(false);

                var allAccounts = await identityUnitOfWork
                    .WriteRepo
                    .GetAllAccounts(cancellationToken)
                    .ConfigureAwait(false);


                return resultHandler.Handle(result, v => TypedResults.Created(v.Id.ToString()));
            });
    }
}
internal sealed class CreateGeneralEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            AccountEndpoints.Instance.CreateGeneralEndpoint.Url.ToString(),
            async (
                CreateGeneralApiRequest req,
                IIdentityUnitOfWork identityUnitOfWork,
                IResultHandler resultHandler,
                CancellationToken cancellationToken) =>
            {

                var result = await identityUnitOfWork
                    .WriteRepo
                    .GetOne(req.groupId, cancellationToken)
                    .Map(group => group.CreateGeneral(req.title, req.code)
                        .Map(acc => identityUnitOfWork.WriteRepo.AddAccount(acc).Map(() => acc))
                        .Map(acc => identityUnitOfWork.SaveChangesWithResultAsync(CancellationToken.None).Map(_ => acc)))
                    .ConfigureAwait(false);

                var allAccounts = await identityUnitOfWork
                    .WriteRepo
                    .GetAllAccounts(cancellationToken)
                    .ConfigureAwait(false);


                return resultHandler.Handle(result, v => TypedResults.Created(v.Id.ToString()));
            });
    }
}
internal sealed class CreateSubsidaryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            AccountEndpoints.Instance.CreateSubsidaryEndpoint.Url.ToString(),
            async (
                CreateSubsidaryApiRequest req,
                IIdentityUnitOfWork identityUnitOfWork,
                IResultHandler resultHandler,
                CancellationToken cancellationToken) =>
            {

                var result = await identityUnitOfWork
                    .WriteRepo
                    .GetOne(req.generalId, cancellationToken)
                    .Map(group => group.CreateSubsidary(req.title, req.code)
                        .Map(acc => identityUnitOfWork.WriteRepo.AddAccount(acc).Map(() => acc))
                        .Map(acc => identityUnitOfWork.SaveChangesWithResultAsync(CancellationToken.None).Map(_ => acc)))
                    .ConfigureAwait(false);

                var allAccounts = await identityUnitOfWork
                    .WriteRepo
                    .GetAllAccounts(cancellationToken)
                    .ConfigureAwait(false);


                return resultHandler.Handle(result, v => TypedResults.Created(v.Id.ToString()));
            });
    }
}
