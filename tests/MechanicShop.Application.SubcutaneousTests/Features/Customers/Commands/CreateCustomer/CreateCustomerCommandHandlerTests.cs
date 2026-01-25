using MechanicShop.Application.Common.Interfaces;
using MechanicShop.Application.Features.Customers.Commands.CreateCustomer;
using MechanicShop.Application.SubcutaneousTests.Common;

using MediatR;

using Xunit;

namespace MechanicShop.Application.SubcutaneousTests.Features.Customers.Commands.CreateCustomer;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateCustomerCommandHandlerTests(WebAppFactory factory)
{
    private readonly IMediator _mediator = factory.CreateMediator();
    private readonly IAppDbContext _context = factory.CreateAppDbContext();

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var command = new CreateCustomerCommand(
            Name: "John Doe",
            Email: "john@test.com",
            PhoneNumber: "1234567890",
            Vehicles: []);

        var result = await _mediator.Send(command);

        Assert.True(result.IsSuccess);
        Assert.Equal("John Doe", result.Value.Name);
        Assert.Equal("john@test.com", result.Value.Email);
    }

    [Fact]
    public async Task Handle_WithInvalidEmail_ShouldFail()
    {
        var command = new CreateCustomerCommand(
            Name: "Jane Doe",
            Email: "invalid-email",
            PhoneNumber: "1234567890",
            Vehicles: []);

        var result = await _mediator.Send(command);

        Assert.True(result.IsError);
    }
}
