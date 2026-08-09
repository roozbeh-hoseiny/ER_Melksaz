using DQ.Abstraction.Specifications;
using DQ.ConsoleApp.AppCore.Entities;
using DQ.Core.Specifications;

namespace DQ.ConsoleApp.AppCore.Specifications;

public sealed class CustomerSearchSpecification
{
    public ISpecification<Customer> Build(string? search)
    {
        var builder = new SpecificationBuilder<Customer>();

        if (!string.IsNullOrWhiteSpace(search))
        {
            builder.Where(x => x.Name.Contains(search));
        }

        return builder.Build();
    }
}