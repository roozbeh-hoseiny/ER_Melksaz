namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Core;

public abstract class NoTrackingSelectorSpecification<T, TResult> : EFSpecification<T, TResult>
    where T : class
{
    protected NoTrackingSelectorSpecification()
    {
        _ = this.WithNoTracking();
    }
}
