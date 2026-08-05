using DQ.EntityFrameworkCore.Evaluators;
using Microsoft.Extensions.DependencyInjection;

namespace DQ.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddDynamicQueryEntityFrameworkCore(
        this IServiceCollection services)
    {
        services.AddScoped<ISpecificationEvaluator, SpecificationEvaluator>();

        services.AddScoped<ISpecificationPartEvaluator, CriteriaEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, IncludeEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, SplitQueryEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, OrderEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, PagingEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, AsNoTrackingEvaluator>();
        services.AddScoped<ISpecificationPartEvaluator, AsNoTrackingWithIdentityResolutionEvaluator>();

        return services;
    }
}