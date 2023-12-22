namespace TestingFundamentals.Fundamentals;

public class DemeritPointCalculator
{
    private const int SpeedLimit = 65;
    private const int MaxSpeed = 400;

    public int CalulateDemritPoints(int speed)
    {
        if (speed < 0 || speed > MaxSpeed)
            throw new ArgumentOutOfRangeException();

        if (speed <= SpeedLimit) return 0;

        const int kmPerDemitPoint = 5;

        var demeritPoints = (speed - SpeedLimit) / kmPerDemitPoint;

        return demeritPoints;
    }
}
