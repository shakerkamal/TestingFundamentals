using TestingFundamentals.Fundamentals;

namespace TestingFundamentals.UnitTests;

[TestFixture]
public class FizzBuzzTests
{

    [Test]
    public void GetOutput_InputDivisibleByThreeAndFive_ReturnsFizzBuzz()
    {
        var setup = FizzBuzz.GetOutput(15);

        Assert.That(setup, Is.EqualTo("FizzBuzz"));
    }

    [Test]
    public void GetOutput_InputDivisibleByThreee_ReturnsFizz()
    {
        var setup = FizzBuzz.GetOutput(6);

        Assert.That(setup, Is.EqualTo("Fizz"));
    }

    [Test]
    public void GetOutput_InputDivisibleByFive_ReturnsBuzz()
    {
        var setup = FizzBuzz.GetOutput(10);

        Assert.That(setup, Is.EqualTo("Buzz"));
    }

    [Test]
    public void GetOutput_InputNotDivisibleByThreeAndFive_ReturnsNumberInStringFormat()
    {
        var setup = FizzBuzz.GetOutput(7);

        Assert.That(setup, Is.EqualTo("7"));
    }
}