using System;
using Calculator;
using NUnit.Framework;

namespace CalculatorTests
{
    public class OneCallCostCalculatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetRate()
        {
            var start = new DateTime(2022, 1, 1, 15, 0, 0);

            Assert.IsTrue(OneCallCostCalculator.IsAfter5Minutes(start, start.AddSeconds(5 * 60)));
            Assert.IsTrue(OneCallCostCalculator.IsAfter5Minutes(start, start.AddSeconds(6 * 60)));

            Assert.IsFalse(OneCallCostCalculator.IsAfter5Minutes(start, start.AddSeconds(4 * 60)));
            Assert.IsFalse(OneCallCostCalculator.IsAfter5Minutes(start, start.AddSeconds(5 * 60 - 1)));
        }

        [Test]
        public void TestIsInHighRate()
        {
            Assert.IsTrue(OneCallCostCalculator.IsInHighRateTime(new DateTime(2022, 1, 1, 8, 0, 0)));
            Assert.IsTrue(OneCallCostCalculator.IsInHighRateTime(new DateTime(2022, 1, 1, 15, 59, 59)));

            Assert.IsFalse(OneCallCostCalculator.IsInHighRateTime(new DateTime(2022, 1, 1, 16, 0, 0)));
            Assert.IsFalse(OneCallCostCalculator.IsInHighRateTime(new DateTime(2022, 1, 1, 20, 0, 0)));
            Assert.IsFalse(OneCallCostCalculator.IsInHighRateTime(new DateTime(2022, 1, 1, 4, 0, 0)));
        }

        [Test]
        public void TestCalculateCallCost_WhenInLowRate()
        {
            var call = new Call(420441212878, new DateTime(2022, 1, 3, 4, 16, 37), new DateTime(2022, 1, 3, 4, 19, 15));
            Assert.AreEqual(1.5m, new OneCallCostCalculator().CalculateCallCost(call));
        }

        [Test]
        public void TestCalculateCallCost_WhenInHighRate()
        {
            var call = new Call(420441212878, new DateTime(2022, 1, 3, 8, 16, 37), new DateTime(2022, 1, 3, 8, 19, 15));
            Assert.AreEqual(3, new OneCallCostCalculator().CalculateCallCost(call));
        }

        [Test]
        public void TestCalculateCallCost_WhenInLowRateAndHighRate_WhenExactly5Minutes()
        {
            var call = new Call(420441212878, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 3, 20));
            // 2 * 0.5 + 3 * 1
            Assert.AreEqual(4m, new OneCallCostCalculator().CalculateCallCost(call));
        }

        [Test]
        public void TestCalculateCallCost_WhenInLowRateAndHighRate_WhenExactly6Minutes()
        {
            var call = new Call(420441212878, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 4, 20));
            // 2 * 0.5 + 3 * 1 + 1 * 0.2
            Assert.AreEqual(4.2m, new OneCallCostCalculator().CalculateCallCost(call));
        }

        [Test]
        public void TestCalculateCallCost_WhenInHighRateAndLowRate_WhenLongerThan5Minutes()
        {
            var call = new Call(420441212878, new DateTime(2022, 1, 3, 15, 58, 20), new DateTime(2022, 1, 3, 16, 5, 21));
            // 2 * 1 + 3 * 0.5 + 3 * 0.2 
            Assert.AreEqual(4.1m, new OneCallCostCalculator().CalculateCallCost(call));
        }
    }
}