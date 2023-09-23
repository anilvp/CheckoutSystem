namespace Domain;

public class Item
{
    public Item(string id, float price)
    {
        Id = id;
        Price = price;
    }

    public string Id { get; private set; }

    public float Price { get; private set; }
}