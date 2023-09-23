using Domain.PricingRules.Interfaces;

namespace Domain.PricingRules;

public class MultipriceRule : IRule
{
    public MultipriceRule(string productId, int quantity, float price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public (Dictionary<Item, int>, float) ApplyPricingRuleToBasket(Dictionary<Item, int> basket, float total)
    {
        if (basket.Keys.Select(x => x.Id).Contains(ProductId))
        {
            Item item = basket.Keys.Where(x => x.Id == ProductId).Single();
            total += (basket[item] / Quantity) * Price;
            basket[item] -= (basket[item] / Quantity) * Quantity;
        }
        return (basket, total);
    }

    string ProductId;

    int Quantity;

    float Price;
}