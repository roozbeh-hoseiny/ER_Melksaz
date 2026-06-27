namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.EFSpecification.Core;

public abstract class NoTrackingSpecification<T> : EFSpecification<T>
    where T : class
{
    protected NoTrackingSpecification()
    {
        _ = this.WithNoTracking();
    }
}
