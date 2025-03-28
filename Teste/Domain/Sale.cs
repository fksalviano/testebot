namespace Teste.Domain;

public class Sale
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public double Value { get; set; }   

    public Sale(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}
