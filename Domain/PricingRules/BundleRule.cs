using Domain.PricingRules.Interfaces;

namespace Domain.PricingRules;

public class BundleRule : IRule
{
    public BundleRule(List<string> productIds, float price)
    {
        ProductIds = productIds;
        Price = price;
    }

    public (Dictionary<Item, int>, float) ApplyPricingRuleToBasket(Dictionary<Item, int> basket, float total)
    {
        if (basket.Keys.Select(x => x.Id).Intersect(ProductIds).Count() == ProductIds.Count())
        {
            int minQuantityOfProductInBundle = basket[basket.Keys.Where(x => x.Id == ProductIds[0]).Single()];
            foreach (string id in ProductIds)
            {
                Item item = basket.Keys.Where(x => x.Id == id).Single();
                if (basket[item] < minQuantityOfProductInBundle)
                {
                    minQuantityOfProductInBundle = basket[item];
                }
            }
            foreach (string id in ProductIds)
            {
                Item item = basket.Keys.Where(x => x.Id == id).Single();
                basket[item] -= minQuantityOfProductInBundle;
            }
            total += Price * minQuantityOfProductInBundle;
        }
        return (basket, total);
    }

    List<string> ProductIds;

    float Price;
}