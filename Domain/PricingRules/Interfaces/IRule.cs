namespace Domain.PricingRules.Interfaces;

public interface IRule
{
    public (Dictionary<string, int>, float) ApplyPricingRuleToBasket(Dictionary<string, int> basket,
                                                                     Dictionary<string, float> prices,
                                                                     float total);
}