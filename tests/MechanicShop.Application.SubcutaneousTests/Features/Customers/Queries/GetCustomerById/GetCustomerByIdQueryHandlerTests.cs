using MechanicShop.Application.Common.Interfaces;
using MechanicShop.Application.Features.Customers.Queries.GetCustomerById;
using MechanicShop.Application.SubcutaneousTests.Common;
using MechanicShop.Tests.Common.Customers;

using MediatR;

using Xunit;

namespace MechanicShop.Application.SubcutaneousTests.Features.Customers.Queries.GetCustomerById;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetCustomerByIdQueryHandlerTests(WebAppFactory factory)
{
    private readonly IMediator _mediator = factory.CreateMediator();
    private readonly IAppDbContext _context = factory.CreateAppDbContext();

    [Fact]
    public async Task Handle_WithExistingCustomer_ShouldReturnCustomer()
    {
        // Arrange
        var customer = CustomerFactory.CreateCustomer(name: "Test Customer").Value;

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync(default);

        var query = new GetCustomerByIdQuery(customer.Id);

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(customer.Id, result.Value.CustomerId);
        Assert.Equal("Test Customer", result.Value.Name);
        Assert.NotEmpty(result.Value.Vehicles);
    }

    [Fact]
    public async Task Handle_WithNonExistingCustomer_ShouldFail()
    {
        // Arrange
        var query = new GetCustomerByIdQuery(Guid.NewGuid());

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsError);
    }
}
