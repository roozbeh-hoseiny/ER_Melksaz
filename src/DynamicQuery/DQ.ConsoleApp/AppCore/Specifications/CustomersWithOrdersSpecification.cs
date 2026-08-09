using DQ.Abstraction.Specifications;
using DQ.ConsoleApp.AppCore.Entities;
using DQ.Core.Specifications;

namespace DQ.ConsoleApp.AppCore.Specifications;

public sealed class CustomersWithOrdersSpecification
{
    public ISpecification<Customer> Build()
    {
        return new SpecificationBuilder<Customer>()
            .Include(x => x.Orders)
            .OrderBy(x => x.Name)
            .Build();
    }
}