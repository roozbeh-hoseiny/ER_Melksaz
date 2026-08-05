namespace DQ.Core.Specifications.Buidlers;

/// <summary>
/// Provides factory methods for creating specification builders.
/// </summary>
public static class Specification
{
    /// <summary>
    /// Creates a new specification builder.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <returns>
    /// A new <see cref="SpecificationBuilder{TEntity}"/>.
    /// </returns>
    public static SpecificationBuilder<TEntity> For<TEntity>()
    {
        return new SpecificationBuilder<TEntity>();
    }
}