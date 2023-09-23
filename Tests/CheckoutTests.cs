using Domain;
using Domain.PricingRules;
using Domain.PricingRules.Interfaces;

namespace Tests;

[TestClass]
public class CheckoutTests
{
    readonly Item A = new Item("A", 50);
    readonly Item B = new Item("B", 30);
    readonly Item C = new Item("C", 20);
    readonly Item D = new Item("D", 15);

    readonly List<IRule> RULES = new List<IRule>()
    {
        new MultipriceRule("A", 3, 130),
        new MultipriceRule("B", 2, 45)
    };

    float Price(List<Item> items)
    {
        var co = new Checkout(RULES);
        foreach (Item item in items)
        {
            co.Scan(item);
        }
        return co.CalculateTotal();
    }


    [TestMethod]
    public void CalculateTotal_ManyExampleBaskets_CorrectTotalsCalculated()
    {
        Assert.AreEqual(0, Price(new List<Item>()));
        Assert.AreEqual(50, Price(new List<Item>() { A }));
        Assert.AreEqual(80, Price(new List<Item>() { A, B }));
        Assert.AreEqual(115, Price(new List<Item>() { C, D, B, A }));

        Assert.AreEqual(100, Price(new List<Item>() { A, A }));
        Assert.AreEqual(130, Price(new List<Item>() { A, A, A }));
        Assert.AreEqual(180, Price(new List<Item>() { A, A, A, A }));
        Assert.AreEqual(230, Price(new List<Item>() { A, A, A, A, A }));
        Assert.AreEqual(260, Price(new List<Item>() { A, A, A, A, A, A }));

        Assert.AreEqual(160, Price(new List<Item>() { A, A, A, B }));
        Assert.AreEqual(175, Price(new List<Item>() { A, A, A, B, B }));
        Assert.AreEqual(190, Price(new List<Item>() { A, A, A, B, B, D }));
        Assert.AreEqual(190, Price(new List<Item>() { D, A, B, A, B, A }));
    }

    [TestMethod]
    public void CalculateTotal_AfterEachScan_TotalIncrementedCorrectly()
    {
        var co = new Checkout(RULES);
        Assert.AreEqual(0, co.CalculateTotal());
        co.Scan(A);
        Assert.AreEqual(50, co.CalculateTotal());
        co.Scan(B);
        Assert.AreEqual(80, co.CalculateTotal());
        co.Scan(A);
        Assert.AreEqual(130, co.CalculateTotal());
        co.Scan(A);
        Assert.AreEqual(160, co.CalculateTotal());
        co.Scan(B);
        Assert.AreEqual(175, co.CalculateTotal());
    }
}