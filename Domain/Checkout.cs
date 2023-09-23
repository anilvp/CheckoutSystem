using Domain.PricingRules.Interfaces;

namespace Domain;

public class Checkout
{
    public Checkout(List<IRule> pricingRules)
    {
        PricingRules = pricingRules;
        Basket = new Dictionary<Item, int>();
    }

    public void Scan(Item item)
    {
        if (Basket.TryGetValue(item, out int _))
        {
            Basket[item] += 1;
        }
        else
        {
            Basket[item] = 1;
        }
    }

    public float CalculateTotal()
    {
        float total = 0;
        Dictionary<Item, int> remainingItems = new Dictionary<Item, int>(Basket);
        foreach (IRule rule in PricingRules)
        {
            (remainingItems, total) = rule.ApplyPricingRuleToBasket(remainingItems, total);
        }
        foreach (Item item in remainingItems.Keys)
        {
            total += item.Price * remainingItems[item];
        }
        return total;
    }

    public List<IRule> PricingRules { get; private set; }

    public Dictionary<Item, int> Basket { get; private set; }
}