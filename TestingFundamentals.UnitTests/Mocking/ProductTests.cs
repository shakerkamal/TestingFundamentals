using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingFundamentals.Mocking;

namespace TestingFundamentals.UnitTests.Mocking;

[TestFixture]
public class ProductTests
{
    [Test]
    public void GetPrice_GoldCustomer_Apply30PercentDiscount()
    {
        var product = new Product { ListPrice = 100 };

        var result = product.GetPrice(new Customer { IsGold = true });

        Assert.That(result, Is.EqualTo(70));
    }

    [Test]
    public void GetPrice_GoldCustomer_Apply30PercentDiscountWithAbuseOfMock()
    {
        //we always don't need to create mock object if it is a simple implementation
        var customer = new Mock<ICustomer>();
        customer.Setup(c => c.IsGold).Returns(true);

        var product = new Product { ListPrice = 100 };

        var result = product.GetPrice(customer.Object);

        Assert.That(result, Is.EqualTo(70));
    }
}
