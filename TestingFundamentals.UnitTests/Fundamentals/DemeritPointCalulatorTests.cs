using TestingFundamentals.Fundamentals;

namespace TestingFundamentals.UnitTests.Fundamentals;

[TestFixture]
public class DemeritPointCalulatorTests
{
    private DemeritPointCalculator _calculator;
    [SetUp]
    public void SetUp()
    {
        _calculator = new DemeritPointCalculator();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(401)]
    public void CalulateDemritPoints_SpeedIsOutOfRange_ThrowsArgumentOutOfRangeException(int speed)
    {
        Assert.That(() => _calculator.CalulateDemritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(64, 0)]
    [TestCase(65, 0)]
    [TestCase(66, 0)]
    [TestCase(70, 1)]
    [TestCase(73, 1)]
    public void CalculateDemeritPoints_WhenCalled_ReturnsDemeritPoints(int speed, int output)
    {
        var response = _calculator.CalulateDemritPoints(speed);

        Assert.That(response, Is.EqualTo(output));
    }
}
