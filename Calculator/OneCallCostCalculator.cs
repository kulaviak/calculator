namespace MyApp;

public class OneCallCostCalculator
{
    private static decimal LOW_RATE = 0.5m;
    
    private static decimal HIGH_RATE = 1m;
    
    private static decimal BONUS_RATE = 0.2m;

    public decimal CalculateCallCost(Call call)
    {
        decimal ret = 0;
        for (var dateTime = call.Start; dateTime < call.End; dateTime = dateTime.AddMinutes(1))
        {
            ret += GetRate(call.Start, dateTime);
        }
        return ret;
    }

    public static decimal GetRate(DateTime callStart, DateTime dateTime)
    {
        if (!IsAfter5Minutes(callStart, dateTime))
        {
            if (IsInHighRateTime(dateTime))
            {
                return HIGH_RATE;
            }
            else
            {
                return LOW_RATE;
            }
        }
        else
        {
            return BONUS_RATE;
        }
    }

    public static bool IsAfter5Minutes(DateTime callStart, DateTime dateTime)
    {
        var ret = dateTime.Subtract(callStart).Minutes >= 5;
        return ret;
    }

    public static bool IsInHighRateTime(DateTime dateTime)
    {
        var ret = 8 <= dateTime.Hour && dateTime.Hour < 16;
        return ret;
    }
}