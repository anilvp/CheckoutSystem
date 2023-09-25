using Domain;
using Domain.PricingRules;
using Domain.PricingRules.Interfaces;

namespace Tests;

[TestClass]
public class CheckoutTests
{

    readonly List<IRule> RULES = new List<IRule>()
    {
        new MultipriceRule("A", 3, 130),
        new MultipriceRule("B", 2, 45)
    };

    readonly Dictionary<string, float> PRICES = new()
    {
        {"A", 50},
        {"B", 30},
        {"C", 20},
        {"D", 15}
    };

    float Price(List<string> itemIds)
    {
        var co = new Checkout(RULES);
        foreach (string itemId in itemIds)
        {
            co.Scan(itemId, PRICES[itemId]);
        }
        return co.CalculateTotal();
    }


    [TestMethod]
    public void CalculateTotal_ManyExampleBaskets_CorrectTotalsCalculated()
    {
        Assert.AreEqual(0, Price(new()));
        Assert.AreEqual(50, Price(new() { "A" }));
        Assert.AreEqual(80, Price(new() { "A", "B" }));
        Assert.AreEqual(115, Price(new() { "C", "D", "B", "A" }));

        Assert.AreEqual(100, Price(new() { "A", "A" }));
        Assert.AreEqual(130, Price(new() { "A", "A", "A" }));
        Assert.AreEqual(180, Price(new() { "A", "A", "A", "A" }));
        Assert.AreEqual(230, Price(new() { "A", "A", "A", "A", "A" }));
        Assert.AreEqual(260, Price(new() { "A", "A", "A", "A", "A", "A" }));

        Assert.AreEqual(160, Price(new() { "A", "A", "A", "B" }));
        Assert.AreEqual(175, Price(new() { "A", "A", "A", "B", "B" }));
        Assert.AreEqual(190, Price(new() { "A", "A", "A", "B", "B", "D" }));
        Assert.AreEqual(190, Price(new() { "D", "A", "B", "A", "B", "A" }));
    }

    [TestMethod]
    public void CalculateTotal_AfterEachScan_TotalIncrementedCorrectly()
    {
        var co = new Checkout(RULES);
        Assert.AreEqual(0, co.CalculateTotal());
        co.Scan("A", 50);
        Assert.AreEqual(50, co.CalculateTotal());
        co.Scan("B", 30);
        Assert.AreEqual(80, co.CalculateTotal());
        co.Scan("A", 50);
        Assert.AreEqual(130, co.CalculateTotal());
        co.Scan("A", 50);
        Assert.AreEqual(160, co.CalculateTotal());
        co.Scan("B", 30);
        Assert.AreEqual(175, co.CalculateTotal());
    }
}