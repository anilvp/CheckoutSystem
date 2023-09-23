using Domain.PricingRules.Interfaces;

namespace Domain.PricingRules;

public class PercentOffRule : IRule
{
    public PercentOffRule(string productId, float percentage)
    {
        ProductId = productId;
        Percentage = percentage;
    }

    public (Dictionary<Item, int>, float) ApplyPricingRuleToBasket(Dictionary<Item, int> basket, float total)
    {
        if (basket.Keys.Select(x => x.Id).Contains(ProductId))
        {
            Item item = basket.Keys.Where(x => x.Id == ProductId).Single();
            total += (item.Price - item.Price * Percentage / 100) * basket[item];
            basket[item] = 0;
        }
        return (basket, total);
    }

    string ProductId;

    float Percentage;
}