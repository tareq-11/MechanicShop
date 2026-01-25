using MechanicShop.Application.Common.Interfaces;
using MechanicShop.Application.Features.RepairTasks.Queries.GetRepairTasks;
using MechanicShop.Application.SubcutaneousTests.Common;
using MechanicShop.Tests.Common.RepaireTasks;

using MediatR;

using Xunit;

namespace MechanicShop.Application.SubcutaneousTests.Features.RepairTasks.Queries.GetRepairTasks;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetRepairTasksQueryHandlerTests(WebAppFactory factory)
{
    private readonly IMediator _mediator = factory.CreateMediator();
    private readonly IAppDbContext _context = factory.CreateAppDbContext();

    [Fact]
    public async Task Handle_WithExistingRepairTasks_ShouldReturnRepairTasks()
    {
        // Arrange
        var task1 = RepairTaskFactory.CreateRepairTask(name: "Oil Change").Value;
        var task2 = RepairTaskFactory.CreateRepairTask(name: "Brake Inspection").Value;

        await _context.RepairTasks.AddAsync(task1);
        await _context.RepairTasks.AddAsync(task2);
        await _context.SaveChangesAsync(default);

        var query = new GetRepairTasksQuery();

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Count >= 2);
        Assert.Contains(result.Value, rt => rt.Name == "Oil Change");
        Assert.Contains(result.Value, rt => rt.Name == "Brake Inspection");
    }

    [Fact]
    public async Task Handle_WithNoRepairTasks_ShouldReturnEmptyList()
    {
        // Arrange
        var query = new GetRepairTasksQuery();

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}
