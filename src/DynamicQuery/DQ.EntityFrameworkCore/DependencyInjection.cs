using DQ.Core.Projections;
using DQ.Core.Queries;
using DQ.Core.Specifications;
using DQ.EntityFrameworkCore.Projections;
using DQ.EntityFrameworkCore.Queries;
using DQ.EntityFrameworkCore.Specifications;
using Microsoft.Extensions.DependencyInjection;

namespace DQ.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddDynamicQueryEntityFrameworkCore(this IServiceCollection services)
    {
        services.AddTransient(typeof(ISpecificationBuilder<>), typeof(SpecificationBuilder<>));
        services.AddTransient(typeof(IProjectionBuilder<>), typeof(ProjectionBuilder<>));
        services.AddTransient(typeof(IQueryBuilder<>), typeof(QueryBuilder<>));
        services.AddScoped<IQueryExecutor, QueryExecutor>();


        services.AddScoped<ISpecificationEvaluator, SpecificationEvaluator>();
        services.AddScoped<IProjectionEvaluator, ProjectionEvaluator>();

        services.AddScoped<ISpecificationPartEvaluator, CriteriaEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, IncludeEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, SplitQueryEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, OrderEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, PagingEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, AsNoTrackingEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, AsNoTrackingWithIdentityResolutionEvaluator>();

        services.AddScoped<ProjectionMetadataResolver>();

        return services;
    }
}