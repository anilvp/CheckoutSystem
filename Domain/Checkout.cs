using Domain.PricingRules.Interfaces;

namespace Domain;

public class Checkout
{
    public Checkout(List<IRule> pricingRules)
    {
        PricingRules = pricingRules;
        Basket = new Dictionary<string, int>();
        Prices = new Dictionary<string, float>();
    }

    public void Scan(string itemId, float price)
    {
        if (Basket.TryGetValue(itemId, out int _))
        {
            Basket[itemId] += 1;
        }
        else
        {
            Basket[itemId] = 1;
        }
        Prices[itemId] = price;
    }

    public float CalculateTotal()
    {
        float total = 0;
        Dictionary<string, int> remainingItems = new(Basket);
        foreach (IRule rule in PricingRules)
        {
            (remainingItems, total) = rule.ApplyPricingRuleToBasket(remainingItems, Prices, total);
        }
        foreach (string itemId in remainingItems.Keys)
        {
            total += Prices[itemId] * remainingItems[itemId];
        }
        return total;
    }

    public List<IRule> PricingRules { get; private set; }

    public Dictionary<string, int> Basket { get; private set; }

    public Dictionary<string, float> Prices { get; private set; }
}