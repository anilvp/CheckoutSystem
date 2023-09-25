using Domain.PricingRules.Interfaces;

namespace Domain.PricingRules;

public class MultipriceRule : IRule
{
    public MultipriceRule(string itemId, int quantity, float price)
    {
        ItemId = itemId;
        Quantity = quantity;
        Price = price;
    }

    public (Dictionary<string, int>, float) ApplyPricingRuleToBasket(Dictionary<string, int> basket,
                                                                     Dictionary<string, float> prices,
                                                                     float total)
    {
        if (basket.ContainsKey(ItemId))
        {
            total += (basket[ItemId] / Quantity) * Price;
            basket[ItemId] -= (basket[ItemId] / Quantity) * Quantity;
        }
        return (basket, total);
    }

    string ItemId;

    int Quantity;

    float Price;
}