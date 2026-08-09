using DQ.Abstraction.Projections;
using DQ.ConsoleApp.AppCore.DTOs;
using DQ.ConsoleApp.AppCore.Entities;
using DQ.Core.Projections;

namespace DQ.ConsoleApp.AppCore.Specifications;

public sealed class CustomerDetailsProjection
{
    public IProjection<Customer, CustomerDetailsDto> Build()
    {
        return new ProjectionBuilder<Customer>()
            .Include(nameof(Customer.Id))
            .Include(nameof(Customer.Name))
            .Include(nameof(Customer.IsActive))
            .Include(
                "Orders.Id",
                "Orders.Id")
            .Include(
                "Orders.Amount",
                "Orders.Amount")
            .Include(
                "Orders.CreatedAt",
                "Orders.CreatedAt")
            .Build<CustomerDetailsDto>();
    }
}