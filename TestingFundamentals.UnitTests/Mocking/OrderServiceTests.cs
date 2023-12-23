using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingFundamentals.Mocking;

namespace TestingFundamentals.UnitTests.Mocking;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IStorage> _mockStorage;
    private OrderService _orderService;
    [SetUp]
    public void Setup()
    {
        _mockStorage = new Mock<IStorage>();
        _orderService = new OrderService(_mockStorage.Object);
    }
    [Test]
    public void PlaceOrder_WhenCalled_ShouldSaveOrder()
    {
        //var mockStorage = new Mock<IStorage>();
        //var service = new OrderService(mockStorage.Object);
        var order = new Order();
        _orderService.PlaceOrder(order);

        _mockStorage.Verify(s => s.Store(order));
    }
}
