using Domain;
using Domain.PricingRules;

namespace Tests;

[TestClass]
public class PricingRulesTests
{

    [TestMethod]
    public void MultipriceRule_RuleConditionsMet_OfferApplied()
    {
        Dictionary<string, int> basket = new Dictionary<string, int> { {"A", 3} };
        float total = 0;
        MultipriceRule multipriceRule = new MultipriceRule("A", 3, 130);

        (basket, total) = multipriceRule.ApplyPricingRuleToBasket(basket, new Dictionary<string, float>(), total);

        Assert.AreEqual(130, total);
        Assert.AreEqual(0, basket["A"]);
    }

    [TestMethod]
    public void MultipriceRule_RuleConditionsMetMultipleTimes_OfferAppliedMultipleTimes()
    {
        Dictionary<string, int> basket = new Dictionary<string, int> { { "A", 10 } };
        float total = 0;
        MultipriceRule multipriceRule = new MultipriceRule("A", 3, 130);

        (basket, total) = multipriceRule.ApplyPricingRuleToBasket(basket, new Dictionary<string, float>(), total);

        Assert.AreEqual(390, total);
        Assert.AreEqual(1, basket["A"]);
    }

    [TestMethod]
    public void MultipriceRule_RuleConditionsNotMet_OfferNotApplied()
    {
        Dictionary<string, int> basket = new Dictionary<string, int> { { "A", 1 } };
        float total = 0;
        MultipriceRule multipriceRule = new MultipriceRule("A", 3, 130);

        (basket, total) = multipriceRule.ApplyPricingRuleToBasket(basket, new Dictionary<string, float>(), total);

        Assert.AreEqual(0, total);
        Assert.AreEqual(1, basket["A"]);
    }



    [TestMethod]
    public void PercentOffRule_RuleConditionsMet_OfferApplied()
    {
        Dictionary<string, int> basket = new Dictionary<string, int> { { "A", 3 } };
        Dictionary<string, float> prices = new Dictionary<string, float> { { "A", 50 } };
        float total = 0;
        PercentOffRule percentOffRule = new PercentOffRule("A", 30);

        (basket, total) = percentOffRule.ApplyPricingRuleToBasket(basket, prices, total);

        Assert.AreEqual(105, total);
        Assert.AreEqual(0, basket["A"]);
    }



    [TestMethod]
    public void BundleRule_RuleConditionsMet_OfferApplied()
    {
        Dictionary<string, int> basket = new Dictionary<string, int> { { "A", 3 }, { "B", 3 }, { "C", 2 } };
        float total = 0;
        BundleRule bundleRule = new BundleRule(new List<string> {"A", "B", "C"}, 75);

        (basket, total) = bundleRule.ApplyPricingRuleToBasket(basket, new Dictionary<string, float>(), total);

        Assert.AreEqual(150, total);
        Assert.AreEqual(1, basket["A"]);
        Assert.AreEqual(1, basket["B"]);
        Assert.AreEqual(0, basket["C"]);
    }

    [TestMethod]
    public void BundleRule_BasketOnlyContainsPartOfBundle_OfferNotApplied()
    {
        Dictionary<string, int> basket = new Dictionary<string, int> { { "A", 3 }, { "C", 2 } };
        float total = 0;
        BundleRule bundleRule = new BundleRule(new List<string> { "A", "B", "C" }, 75);

        (basket, total) = bundleRule.ApplyPricingRuleToBasket(basket, new Dictionary<string, float>(), total);

        Assert.AreEqual(0, total);
        Assert.AreEqual(3, basket["A"]);
        Assert.AreEqual(2, basket["C"]);
    }
}