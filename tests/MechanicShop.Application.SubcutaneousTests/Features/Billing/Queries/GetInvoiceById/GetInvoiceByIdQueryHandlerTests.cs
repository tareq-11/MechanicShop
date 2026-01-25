using MechanicShop.Application.Common.Interfaces;
using MechanicShop.Application.Features.Billing.Queries.GetInvoiceById;
using MechanicShop.Application.SubcutaneousTests.Common;
using MechanicShop.Tests.Common.Billing;
using MechanicShop.Tests.Common.Customers;
using MechanicShop.Tests.Common.WorkOrders;

using MediatR;

using Xunit;

namespace MechanicShop.Application.SubcutaneousTests.Features.Billing.Queries.GetInvoiceById;

[Collection(WebAppFactoryCollection.CollectionName)]
public class GetInvoiceByIdQueryHandlerTests(WebAppFactory factory)
{
    private readonly IMediator _mediator = factory.CreateMediator();
    private readonly IAppDbContext _context = factory.CreateAppDbContext();

    [Fact]
    public async Task Handle_WithExistingInvoice_ShouldReturnInvoice()
    {
        // Arrange
        var customer = CustomerFactory.CreateCustomer().Value;
        var vehicle = customer.Vehicles.First();
        var workOrder = WorkOrderFactory.CreateWorkOrder(vehicleId: vehicle.Id).Value;
        var invoice = InvoiceFactory.CreateInvoice(workOrderId: workOrder.Id).Value;

        workOrder.Vehicle = vehicle;
        workOrder.Invoice = invoice;
        invoice.WorkOrder = workOrder;

        await _context.Customers.AddAsync(customer);
        await _context.WorkOrders.AddAsync(workOrder);
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync(default);

        var query = new GetInvoiceByIdQuery(invoice.Id);

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(invoice.Id, result.Value.InvoiceId);
        Assert.Equal(workOrder.Id, result.Value.WorkOrderId);
        Assert.NotNull(result.Value.Customer);
        Assert.NotNull(result.Value.Vehicle);
    }

    [Fact]
    public async Task Handle_WithNonExistingInvoice_ShouldFail()
    {
        // Arrange
        var query = new GetInvoiceByIdQuery(Guid.NewGuid());

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.True(result.IsError);
    }
}
