using MechanicShop.Application.Features.Billing.Mappers;
using MechanicShop.Tests.Common.Billing;
using MechanicShop.Tests.Common.Customers;
using MechanicShop.Tests.Common.Employees;
using MechanicShop.Tests.Common.WorkOrders;

using Xunit;

namespace MechanicShop.Application.UnitTests.Mappers;

public class InvoiceMapperTest
{
    [Fact]
    public void ToDto_ShouldMapInvoiceWithRelationsAndItems()
    {
        var customer = CustomerFactory.CreateCustomer().Value;
        var vehicle = customer.Vehicles.First();
        vehicle.Customer = customer;
        var labor = EmployeeFactory.CreateLabor().Value;

        var workOrder = WorkOrderFactory.CreateWorkOrder(vehicleId: vehicle.Id, laborId: labor.Id).Value;
        workOrder.Vehicle = vehicle;
        workOrder.Labor = labor;

        var lineItem = InvoiceLineItemFactory.CreateInvoiceLineItem(lineNumber: 2, description: "Engine diagnostics", quantity: 3, unitPrice: 75m).Value;
        var invoice = InvoiceFactory.CreateInvoice(workOrderId: workOrder.Id, items: [lineItem], discount: 10m, taxAmount: 5m).Value;
        invoice.WorkOrder = workOrder;

        var dto = invoice.ToDto();

        Assert.Equal(invoice.Id, dto.InvoiceId);
        Assert.Equal(invoice.WorkOrderId, dto.WorkOrderId);
        Assert.Equal(invoice.IssuedAtUtc, dto.IssuedAtUtc);
        Assert.Equal(invoice.Subtotal, dto.Subtotal);
        Assert.Equal(invoice.DiscountAmount, dto.DiscountAmount);
        Assert.Equal(invoice.TaxAmount, dto.TaxAmount);
        Assert.Equal(invoice.Total, dto.Total);
        Assert.Equal(invoice.Status.ToString(), dto.PaymentStatus);

        Assert.NotNull(dto.Customer);
        Assert.Equal(customer.Id, dto.Customer!.CustomerId);
        Assert.NotNull(dto.Vehicle);
        Assert.Equal(vehicle.Id, dto.Vehicle!.VehicleId);
        Assert.Equal(vehicle.Make, dto.Vehicle.Make);
        Assert.Equal(vehicle.Model, dto.Vehicle.Model);
        Assert.Equal(vehicle.Year, dto.Vehicle.Year);
        Assert.Equal(vehicle.LicensePlate, dto.Vehicle.LicensePlate);

        Assert.Single(dto.Items);
        var itemDto = dto.Items[0];
        Assert.Equal(lineItem.InvoiceId, itemDto.InvoiceId);
        Assert.Equal(2, itemDto.LineNumber);
        Assert.Equal("Engine diagnostics", itemDto.Description);
        Assert.Equal(3, itemDto.Quantity);
        Assert.Equal(75m, itemDto.UnitPrice);
        Assert.Equal(lineItem.LineTotal, itemDto.LineTotal);
    }

    [Fact]
    public void ToDtos_ShouldMapInvoiceList()
    {
        var customer = CustomerFactory.CreateCustomer().Value;
        var vehicle = customer.Vehicles.First();
        vehicle.Customer = customer;
        var labor = EmployeeFactory.CreateLabor().Value;

        var workOrder1 = WorkOrderFactory.CreateWorkOrder(vehicleId: vehicle.Id, laborId: labor.Id).Value;
        workOrder1.Vehicle = vehicle;
        var workOrder2 = WorkOrderFactory.CreateWorkOrder(vehicleId: vehicle.Id, laborId: labor.Id).Value;
        workOrder2.Vehicle = vehicle;

        var invoice1 = InvoiceFactory.CreateInvoice(workOrderId: workOrder1.Id).Value;
        invoice1.WorkOrder = workOrder1;
        var invoice2 = InvoiceFactory.CreateInvoice(workOrderId: workOrder2.Id).Value;
        invoice2.WorkOrder = workOrder2;

        var invoices = new List<Domain.Workorders.Billing.Invoice> { invoice1, invoice2 };

        var dtos = invoices.ToDtos();

        Assert.Equal(2, dtos.Count);
        Assert.Collection(
            dtos,
            dto => Assert.Equal(invoice1.Id, dto.InvoiceId),
            dto => Assert.Equal(invoice2.Id, dto.InvoiceId));
    }
}
