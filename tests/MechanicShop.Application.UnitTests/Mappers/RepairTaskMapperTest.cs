using MechanicShop.Application.Features.RepairTasks.Mappers;
using MechanicShop.Domain.RepairTasks;
using MechanicShop.Domain.RepairTasks.Enums;
using MechanicShop.Domain.RepairTasks.Parts;
using MechanicShop.Tests.Common.RepaireTasks;

using Xunit;

namespace MechanicShop.Application.UnitTests.Mappers;

public class RepairTaskMapperTest
{
    [Fact]
    public void ToDto_ShouldMapRepairTaskWithParts()
    {
        var part1 = PartFactory.CreatePart(name: "Filter", cost: 20m, quantity: 1).Value;
        var part2 = PartFactory.CreatePart(name: "Oil", cost: 15m, quantity: 4).Value;
        var task = RepairTaskFactory.CreateRepairTask(
            name: "Oil Change",
            laborCost: 150m,
            repairDurationInMinutes: RepairDurationInMinutes.Min90,
            parts: new List<Part> { part1, part2 }).Value;

        var dto = task.ToDto();

        Assert.Equal(task.Id, dto.RepairTaskId);
        Assert.Equal("Oil Change", dto.Name);
        Assert.Equal(RepairDurationInMinutes.Min90, dto.EstimatedDurationInMins);
        Assert.Equal(150m, dto.LaborCost);
        Assert.Equal(task.TotalCost, dto.TotalCost);

        Assert.Equal(2, dto.Parts.Count);
        var firstPart = dto.Parts[0];
        Assert.Equal(part1.Id, firstPart.PartId);
        Assert.Equal("Filter", firstPart.Name);
        Assert.Equal(20m, firstPart.Cost);
        Assert.Equal(1, firstPart.Quantity);

        var secondPart = dto.Parts[1];
        Assert.Equal(part2.Id, secondPart.PartId);
        Assert.Equal("Oil", secondPart.Name);
        Assert.Equal(15m, secondPart.Cost);
        Assert.Equal(4, secondPart.Quantity);
    }

    [Fact]
    public void ToDtos_ShouldMapRepairTaskList()
    {
        var task1 = RepairTaskFactory.CreateRepairTask(name: "Brake Check").Value;
        var task2 = RepairTaskFactory.CreateRepairTask(name: "Engine Tune").Value;
        var tasks = new List<RepairTask> { task1, task2 };

        var dtos = tasks.ToDtos();

        Assert.Equal(2, dtos.Count);
        Assert.Collection(
            dtos,
            dto => Assert.Equal(task1.Id, dto.RepairTaskId),
            dto => Assert.Equal(task2.Id, dto.RepairTaskId));
    }

    [Fact]
    public void PartToDto_ShouldMapPart()
    {
        var part = PartFactory.CreatePart(name: "Belt", cost: 45m, quantity: 2).Value;

        var dto = part.ToDto();

        Assert.Equal(part.Id, dto.PartId);
        Assert.Equal("Belt", dto.Name);
        Assert.Equal(45m, dto.Cost);
        Assert.Equal(2, dto.Quantity);
    }
}
