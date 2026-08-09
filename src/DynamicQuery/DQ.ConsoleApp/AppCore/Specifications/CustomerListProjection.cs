using DQ.Abstraction.Projections;
using DQ.ConsoleApp.AppCore.DTOs;
using DQ.ConsoleApp.AppCore.Entities;
using DQ.Core.Projections;

namespace DQ.ConsoleApp.AppCore.Specifications;

public sealed class CustomerListProjection
{
    public IProjection<Customer, CustomerListDto> Build()
    {
        return new ProjectionBuilder<Customer>()
            .Include(nameof(Customer.Id))
            .Include(nameof(Customer.Name))
            .Include(nameof(Customer.IsActive))
            .Build<CustomerListDto>();
    }
}