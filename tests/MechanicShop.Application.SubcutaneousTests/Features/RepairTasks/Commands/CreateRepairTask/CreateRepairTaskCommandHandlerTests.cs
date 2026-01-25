using MechanicShop.Application.Common.Interfaces;
using MechanicShop.Application.Features.RepairTasks.Commands.CreateRepairTask;
using MechanicShop.Application.SubcutaneousTests.Common;
using MechanicShop.Domain.RepairTasks.Enums;

using MediatR;

using Xunit;

namespace MechanicShop.Application.SubcutaneousTests.Features.RepairTasks.Commands.CreateRepairTask;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateRepairTaskCommandHandlerTests(WebAppFactory factory)
{
    private readonly IMediator _mediator = factory.CreateMediator();
    private readonly IAppDbContext _context = factory.CreateAppDbContext();

    [Fact]
    public async Task Handle_WithValidData_ShouldSucceed()
    {
        var command = new CreateRepairTaskCommand(
            Name: "Oil Change",
            LaborCost: 50m,
            EstimatedDurationInMins: RepairDurationInMinutes.Min60,
            Parts: []);

        var result = await _mediator.Send(command);

        Assert.True(result.IsSuccess);
        Assert.Equal("Oil Change", result.Value.Name);
        Assert.Equal(50m, result.Value.LaborCost);
    }

    [Fact]
    public async Task Handle_WithNegativeCost_ShouldFail()
    {
        var command = new CreateRepairTaskCommand(
            Name: "Brake Repair",
            LaborCost: -10m,
            EstimatedDurationInMins: RepairDurationInMinutes.Min60,
            Parts: []);

        var result = await _mediator.Send(command);

        Assert.True(result.IsError);
    }
}
