using Domain.PricingRules.Interfaces;

namespace Domain.PricingRules;

public class BundleRule : IRule
{
    public BundleRule(List<string> itemIds, float price)
    {
        ItemIds = itemIds;
        Price = price;
    }

    public (Dictionary<string, int>, float) ApplyPricingRuleToBasket(Dictionary<string, int> basket,
                                                                     Dictionary<string, float> prices,
                                                                     float total)
    {
        if (basket.Keys.Intersect(ItemIds).Count() == ItemIds.Count)
        {
            int minQuantityOfProductInBundle = basket[ItemIds[0]];
            foreach (string itemId in ItemIds)
            {
                if (basket[itemId] < minQuantityOfProductInBundle)
                {
                    minQuantityOfProductInBundle = basket[itemId];
                }
            }
            foreach (string itemId in ItemIds)
            {
                basket[itemId] -= minQuantityOfProductInBundle;
            }
            total += Price * minQuantityOfProductInBundle;
        }
        return (basket, total);
    }

    List<string> ItemIds;

    float Price;
}