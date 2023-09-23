using Domain;
using Domain.PricingRules;

namespace Tests;

[TestClass]
public class PricingRulesTests
{
    [TestMethod]
    public void MultipriceRule_RuleConditionsMet_OfferApplied()
    {
        Item itemA = new Item("A", 50);
        Dictionary<Item, int> basket = new Dictionary<Item, int> { {itemA, 3} };
        float total = 0;
        MultipriceRule multipriceRule = new MultipriceRule("A", 3, 130);

        (basket, total) = multipriceRule.ApplyPricingRuleToBasket(basket, total);

        Assert.AreEqual(130, total);
        Assert.AreEqual(0, basket[itemA]);
    }

    [TestMethod]
    public void MultipriceRule_RuleConditionsMetMultipleTimes_OfferAppliedMultipleTimes()
    {
        Item itemA = new Item("A", 50);
        Dictionary<Item, int> basket = new Dictionary<Item, int> { { itemA, 10 } };
        float total = 0;
        MultipriceRule multipriceRule = new MultipriceRule("A", 3, 130);

        (basket, total) = multipriceRule.ApplyPricingRuleToBasket(basket, total);

        Assert.AreEqual(390, total);
        Assert.AreEqual(1, basket[itemA]);
    }

    [TestMethod]
    public void MultipriceRule_RuleConditionsNotMet_OfferNotApplied()
    {
        Item itemA = new Item("A", 50);
        Dictionary<Item, int> basket = new Dictionary<Item, int> { { itemA, 1 } };
        float total = 0;
        MultipriceRule multipriceRule = new MultipriceRule("A", 3, 130);

        (basket, total) = multipriceRule.ApplyPricingRuleToBasket(basket, total);

        Assert.AreEqual(0, total);
        Assert.AreEqual(1, basket[itemA]);
    }



    [TestMethod]
    public void PercentOffRule_RuleConditionsMet_OfferApplied()
    {
        Item itemA = new Item("A", 50);
        Dictionary<Item, int> basket = new Dictionary<Item, int> { { itemA, 3 } };
        float total = 0;
        PercentOffRule percentOffRule = new PercentOffRule("A", 30);

        (basket, total) = percentOffRule.ApplyPricingRuleToBasket(basket, total);

        Assert.AreEqual(105, total);
        Assert.AreEqual(0, basket[itemA]);
    }



    [TestMethod]
    public void BundleRule_RuleConditionsMet_OfferApplied()
    {
        Item itemA = new Item("A", 50);
        Item itemB = new Item("B", 30);
        Item itemC = new Item("C", 20);
        Dictionary<Item, int> basket = new Dictionary<Item, int> { { itemA, 3 }, { itemB, 3 }, { itemC, 2 } };
        float total = 0;
        BundleRule bundleRule = new BundleRule(new List<string> {"A", "B", "C"}, 75);

        (basket, total) = bundleRule.ApplyPricingRuleToBasket(basket, total);

        Assert.AreEqual(150, total);
        Assert.AreEqual(1, basket[itemA]);
        Assert.AreEqual(1, basket[itemB]);
        Assert.AreEqual(0, basket[itemC]);
    }

    [TestMethod]
    public void BundleRule_BasketOnlyContainsPartOfBundle_OfferNotApplied()
    {
        Item itemA = new Item("A", 50);
        Item itemC = new Item("C", 20);
        Dictionary<Item, int> basket = new Dictionary<Item, int> { { itemA, 3 }, { itemC, 2 } };
        float total = 0;
        BundleRule bundleRule = new BundleRule(new List<string> { "A", "B", "C" }, 75);

        (basket, total) = bundleRule.ApplyPricingRuleToBasket(basket, total);

        Assert.AreEqual(0, total);
        Assert.AreEqual(3, basket[itemA]);
        Assert.AreEqual(2, basket[itemC]);
    }
}