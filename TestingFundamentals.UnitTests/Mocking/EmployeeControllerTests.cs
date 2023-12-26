using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingFundamentals.Mocking;

namespace TestingFundamentals.UnitTests.Mocking;

[TestFixture]
public class EmployeeControllerTests
{
    private Mock<IEmployeeStorage> _mockStorage;
    private EmployeeController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockStorage = new Mock<IEmployeeStorage>();
        _controller = new EmployeeController(_mockStorage.Object);
    }

    [Test]
    public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
    {
        //var mockStorage = new Mock<IEmployeeStorage>();

        //var controller = new EmployeeController(mockStorage.Object);

        _controller.DeleteEmployee(1);

        _mockStorage.Verify(s => s.DeleteEmployee(1));
    }

    [Test]
    public void DeleteEmployee_WhenCalled_ReturnsAnObjectOfRedirectToEmployeesAction()
    {
        _mockStorage.Setup(s => s.DeleteEmployee(It.IsAny<int>()));

        var result = _controller.DeleteEmployee(1);
        Assert.That(result, Is.InstanceOf<ActionResult>());
    }
}
