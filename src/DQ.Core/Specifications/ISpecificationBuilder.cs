using DQ.Abstraction.Specifications;
using DQ.Core.Projections;
using System.Linq.Expressions;

namespace DQ.Core.Specifications;

public interface ISpecificationBuilder<TEntity>
{
    ISpecificationBuilder<TEntity> And(Expression<Func<TEntity, bool>> expression);
    ISpecificationBuilder<TEntity> AsNoTracking();
    ISpecificationBuilder<TEntity> AsNoTrackingWithIdentityResolution();
    ISpecificationBuilder<TEntity> AsSplitQuery();
    ISpecificationBuilder<TEntity> AsTracking();
    ISpecificationBuilder<TEntity> Include(string navigationPath);
    ISpecificationBuilder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> expression);
    ISpecificationBuilder<TEntity> Or(Expression<Func<TEntity, bool>> expression);
    ISpecificationBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> expression);
    ISpecificationBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> expression);
    ISpecificationBuilder<TEntity> Select<TProjection>(Expression<Func<TEntity, TProjection>> expression);
    ISpecificationBuilder<TEntity> Select<TProjection>(ProjectionDefinition<TEntity, TProjection> definition);
    ISpecificationBuilder<TEntity> Skip(int value);
    ISpecificationBuilder<TEntity> Take(int value);
    ISpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression);
    ISpecification<TEntity> Build();
}