using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class MultipleCallCostCalculator
    {
        public decimal CalculateCost(List<Call> calls)
        {
            if (!calls.Any())
            {
                return 0;
            }
            decimal ret = 0;
            var mostFrequentPhoneNumber = GetMostFrequentNumber(calls); 
            foreach (var call in calls)
            {
                var cost = call.PhoneNumber != mostFrequentPhoneNumber ? new OneCallCostCalculator().CalculateCallCost(call) : 0; 
                ret += cost;
            }
            return ret;
        }

        public static double GetMostFrequentNumber(List<Call> calls)
        {
            var pairs = calls.GroupBy(x => x.PhoneNumber).ToDictionary(x => x.Key, y => y.Count());
            var ret = pairs.OrderByDescending(x => x.Value).ThenByDescending(x => x.Key).First().Key;
            return ret;
        }
    }    
}

