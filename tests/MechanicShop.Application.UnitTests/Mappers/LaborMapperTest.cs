using MechanicShop.Application.Features.Labors.Mappers;
using MechanicShop.Domain.Employees;
using MechanicShop.Tests.Common.Employees;

using Xunit;

namespace MechanicShop.Application.UnitTests.Mappers;

public class LaborMapperTest
{
    [Fact]
    public void ToDto_ShouldMapLabor()
    {
        var labor = EmployeeFactory.CreateLabor(firstName: "Jane", lastName: "Smith").Value;

        var dto = labor.ToDto();

        Assert.Equal(labor.Id, dto.LaborId);
        Assert.Equal(labor.FullName, dto.Name);
    }

    [Fact]
    public void ToDtos_ShouldMapLaborList()
    {
        var labor1 = EmployeeFactory.CreateLabor(firstName: "Alice", lastName: "Brown").Value;
        var labor2 = EmployeeFactory.CreateLabor(firstName: "Bob", lastName: "White").Value;
        var labors = new List<Employee> { labor1, labor2 };

        var dtos = labors.ToDtos();

        Assert.Equal(2, dtos.Count);
        Assert.Collection(
            dtos,
            dto => Assert.Equal(labor1.Id, dto.LaborId),
            dto => Assert.Equal(labor2.Id, dto.LaborId));
    }
}
