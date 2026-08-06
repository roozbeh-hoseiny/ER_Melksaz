namespace DQ.Core.Specifications;

/// <summary>
/// Provides factory methods for creating specification builders.
/// </summary>
public static class SpecificationFactory
{
    /// <summary>
    /// Creates a new specification builder.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <returns>
    /// A new <see cref="SpecificationBuilder_Old{TEntity}"/>.
    /// </returns>
    public static SpecificationBuilder<TEntity> For<TEntity>()
    {
        return new SpecificationBuilder<TEntity>();
    }
}
