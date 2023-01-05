using System;
using System.Collections.Generic;
using MyApp;
using NUnit.Framework;

namespace CalculatorTests;

public class CostCalculatorTest
{
    [SetUp]
    public void Setup() {}

    [Test]
    public void TestCalculateCost_WhenTwoCalls()
    {
        // expected cost 4
        var call1 = new Call(1111, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 3, 20));
        // expected cost 4.2
        var call2 = new Call(2222, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 4, 20));
        // expected 4 because call2 is the most frequent
        Assert.AreEqual(4m, new CostCalculator().CalculateCost(new List<Call>{call1, call2}));
    }
    
    [Test]
    public void TestCalculateCost_WhenNoCalls()
    {
        Assert.AreEqual(0, new CostCalculator().CalculateCost(new List<Call>()));
    }

    [Test]
    public void TestGetMostFrequentNumber_WhenTwoMostFrequentNumbers()
    {
        var call1 = new Call(111111, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 4, 20));
        var call2 = new Call(111111, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 4, 20));
        var call3 = new Call(222222, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 4, 20));
        var call4 = new Call(222222, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 4, 20));
        var call5 = new Call(333333, new DateTime(2022, 1, 3, 7, 58, 20), new DateTime(2022, 1, 3, 8, 4, 20));

        Assert.AreEqual(222222, CostCalculator.GetMostFrequentNumber(new List<Call>(){call1, call2, call3, call4, call5}));
    }
}