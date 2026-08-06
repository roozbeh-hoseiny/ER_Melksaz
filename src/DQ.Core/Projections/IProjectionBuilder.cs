namespace DQ.Core.Projections;

/*
    هدف:
    
    ورودی:
    [
        "Id",
        "Name",
        "Orders.Id",
        "Orders.Amount"
    ]
    
    خروجی:
    ProjectionRootNode
     |
     +-- Id
     +-- Name
     +-- Orders
           |
           +-- Id
           +-- Amount

    مثال استفاده:

    var definition =
        new ProjectionBuilder<Customer>()
            .Include("Id")
            .Include("Name", "DisplayName")
            .Include("Orders.Id")
            .Include("Orders.Amount")
            .Build<CustomerDto>();


    var expression =
        projectionExpressionBuilder
            .Build(definition);


    var query =
        db.Customers
          .Select(expression);

    خروجی Expression:

    x => new CustomerDto
    {
        Id = x.Id,

        DisplayName = x.Name,

        Orders =
            x.Orders
              .Select(item => new OrderDto
              {
                  Id = item.Id,
                  Amount = item.Amount
              })
              .ToList()
    }
    
 */
public interface IProjectionBuilder<TEntity>
{
    IProjectionBuilder<TEntity> Include(string propertyName);
    IProjectionBuilder<TEntity> Include(string sourceProperty, string targetProperty);
    ProjectionDefinition<TEntity, TProjection> Build<TProjection>();
}
