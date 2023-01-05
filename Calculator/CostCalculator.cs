using MyApp;

namespace Calculator;

public class CostCalculator
{
    public decimal calculate(string fileName)
    {
        var calls = new CallLogReader().ReadCalls(fileName);
        var cost = new MultipleCallCostCalculator().CalculateCost(calls);
        return cost;
    }
}