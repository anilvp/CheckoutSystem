namespace Domain.PricingRules.Interfaces;

public interface IRule
{
    public (Dictionary<Item, int>, float) ApplyPricingRuleToBasket(Dictionary<Item, int> basket, float total);
}