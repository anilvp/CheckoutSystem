using Domain.PricingRules.Interfaces;

namespace Domain.PricingRules;

public class PercentOffRule : IRule
{
    public PercentOffRule(string itemId, float percentage)
    {
        ItemId = itemId;
        Percentage = percentage;
    }

    public (Dictionary<string, int>, float) ApplyPricingRuleToBasket(Dictionary<string, int> basket,
                                                                     Dictionary<string, float> prices,
                                                                     float total)
    {
        if (basket.ContainsKey(ItemId))
        {
            total += (prices[ItemId] - prices[ItemId] * Percentage / 100) * basket[ItemId];
            basket[ItemId] = 0;
        }
        return (basket, total);
    }

    string ItemId;

    float Percentage;
}