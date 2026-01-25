using MechanicShop.Application.Common.Interfaces;
using MechanicShop.Application.Features.Labors.Queries.GetLabors;
using MechanicShop.Application.SubcutaneousTests.Common;
using MechanicShop.Tests.Common.Employees;

using MediatR;

using Xunit;

namespace MechanicShop.Application.SubcutaneousTests.Features.Labors.Queries.GetLabors;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetLaborsQueryHandlerTests(WebAppFactory factory)
{
    private readonly IMediator _mediator = factory.CreateMediator();
    private readonly IAppDbContext _context = factory.CreateAppDbContext();

    [Fact]
    public async Task Handle_WithExistingLabors_ShouldReturnLabors()
    {
        // Arrange
        var labor1 = EmployeeFactory.CreateLabor(firstName: "Mike", lastName: "Johnson").Value;
        var labor2 = EmployeeFactory.CreateLabor(firstName: "Sarah", lastName: "Williams").Value;

        await _context.Employees.AddAsync(labor1);
        await _context.Employees.AddAsync(labor2);
        await _context.SaveChangesAsync(default);

        var query = new GetLaborsQuery();

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Count >= 2);
        Assert.Contains(result.Value, l => l.Name.Contains("Mike"));
        Assert.Contains(result.Value, l => l.Name.Contains("Sarah"));
    }

    [Fact]
    public async Task Handle_WithNoLabors_ShouldReturnEmptyList()
    {
        // Arrange
        var query = new GetLaborsQuery();

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}
