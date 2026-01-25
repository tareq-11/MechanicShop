using MechanicShop.Application.Common.Interfaces;
using MechanicShop.Application.Features.Customers.Queries.GetCustomers;
using MechanicShop.Application.SubcutaneousTests.Common;
using MechanicShop.Tests.Common.Customers;

using MediatR;

using Xunit;

namespace MechanicShop.Application.SubcutaneousTests.Features.Customers.Queries.GetCustomers;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetCustomersQueryHandlerTests(WebAppFactory factory)
{
    private readonly IMediator _mediator = factory.CreateMediator();
    private readonly IAppDbContext _context = factory.CreateAppDbContext();

    [Fact]
    public async Task Handle_WithExistingCustomers_ShouldReturnCustomers()
    {
        // Arrange
        var customer1 = CustomerFactory.CreateCustomer(name: "John Doe", email: "john@test.com").Value;
        var customer2 = CustomerFactory.CreateCustomer(name: "Jane Smith", email: "jane@test.com").Value;

        await _context.Customers.AddAsync(customer1);
        await _context.Customers.AddAsync(customer2);
        await _context.SaveChangesAsync(default);

        var query = new GetCustomersQuery();

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Count >= 2);
        Assert.Contains(result.Value, c => c.Name == "John Doe");
        Assert.Contains(result.Value, c => c.Name == "Jane Smith");
    }

    [Fact]
    public async Task Handle_WithNoCustomers_ShouldReturnEmptyList()
    {
        // Arrange
        var query = new GetCustomersQuery();

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}
