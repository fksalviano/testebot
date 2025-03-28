namespace Teste.Domain;

public class Product
{
    public string Name { get; }
    public double Value { get; }
    public Promo? Promo { get; set; }

    public Product(string name, double value, Promo? promo = null)
    {
        Name = name;
        Value = value;
        Promo = promo;
    }
}
