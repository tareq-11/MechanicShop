using MechanicShop.Application.Common.Interfaces;
using MechanicShop.Application.Features.RepairTasks.Queries.GetRepairTaskById;
using MechanicShop.Application.SubcutaneousTests.Common;
using MechanicShop.Tests.Common.RepaireTasks;

using MediatR;

using Xunit;

namespace MechanicShop.Application.SubcutaneousTests.Features.RepairTasks.Queries.GetRepairTaskById;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetRepairTaskByIdQueryHandlerTests(WebAppFactory factory)
{
    private readonly IMediator _mediator = factory.CreateMediator();
    private readonly IAppDbContext _context = factory.CreateAppDbContext();

    [Fact]
    public async Task Handle_WithExistingRepairTask_ShouldReturnRepairTask()
    {
        var task = RepairTaskFactory.CreateRepairTask(name: "Transmission Service").Value;

        await _context.RepairTasks.AddAsync(task);
        await _context.SaveChangesAsync(default);

        var query = new GetRepairTaskByIdQuery(task.Id);

        var result = await _mediator.Send(query);

        Assert.True(result.IsSuccess);
        Assert.Equal(task.Id, result.Value.RepairTaskId);
        Assert.Equal("Transmission Service", result.Value.Name);
    }

    [Fact]
    public async Task Handle_WithNonExistingRepairTask_ShouldFail()
    {
        var query = new GetRepairTaskByIdQuery(Guid.NewGuid());

        var result = await _mediator.Send(query);

        Assert.True(result.IsError);
    }
}
