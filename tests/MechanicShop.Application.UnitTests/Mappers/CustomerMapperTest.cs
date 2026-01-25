using MechanicShop.Application.Features.Customers.Mappers;
using MechanicShop.Tests.Common.Customers;

using Xunit;

namespace MechanicShop.Application.UnitTests.Mappers;

public class CustomerMapperTest
{
	[Fact]
	public void ToDto_ShouldMapCustomerAndVehicles()
	{
		var customer = CustomerFactory.CreateCustomer().Value;

		var dto = customer.ToDto();

		Assert.Equal(customer.Id, dto.CustomerId);
		Assert.Equal(customer.Name, dto.Name);
		Assert.Equal(customer.PhoneNumber, dto.PhoneNumber);
		Assert.Equal(customer.Email, dto.Email);

		Assert.Equal(customer.Vehicles.Count(), dto.Vehicles.Count);
		var vehicle = customer.Vehicles.First();
		var vehicleDto = dto.Vehicles.First();
		Assert.Equal(vehicle.Id, vehicleDto.VehicleId);
		Assert.Equal(vehicle.Make, vehicleDto.Make);
		Assert.Equal(vehicle.Model, vehicleDto.Model);
		Assert.Equal(vehicle.Year, vehicleDto.Year);
		Assert.Equal(vehicle.LicensePlate, vehicleDto.LicensePlate);
	}

	[Fact]
	public void ToDtos_ShouldMapCustomerList()
	{
		var customer1 = CustomerFactory.CreateCustomer(name: "Customer #A", email: "a@localhost").Value;
		var customer2 = CustomerFactory.CreateCustomer(name: "Customer #B", email: "b@localhost").Value;
		var customers = new List<Domain.Customers.Customer> { customer1, customer2 };

		var dtos = customers.ToDtos();

		Assert.Equal(2, dtos.Count);

		Assert.Collection(
			dtos,
			dto =>
			{
				Assert.Equal(customer1.Id, dto.CustomerId);
				Assert.Equal(customer1.Name, dto.Name);
			},
			dto =>
			{
				Assert.Equal(customer2.Id, dto.CustomerId);
				Assert.Equal(customer2.Name, dto.Name);
			});
	}

	[Fact]
	public void VehicleToDto_ShouldMapCorrectly()
	{
		var vehicle = VehicleFactory.CreateVehicle(make: "Toyota", model: "Corolla", year: 2020, licensePlate: "XYZ 999").Value;

		var dto = vehicle.ToDto();

		Assert.Equal(vehicle.Id, dto.VehicleId);
		Assert.Equal("Toyota", dto.Make);
		Assert.Equal("Corolla", dto.Model);
		Assert.Equal(2020, dto.Year);
		Assert.Equal("XYZ 999", dto.LicensePlate);
	}
}