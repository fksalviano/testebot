using Teste.Domain;


var products = LoadProducts();

var saleItens = LoadSaleInput(products);

var totalSaleValue = GetTotalSaleValue(saleItens);

Console.WriteLine($"Total da venda: {totalSaleValue:C}");


double GetTotalSaleValue(IEnumerable<Sale> saleItens)
{
    var saleItensValues = saleItens.Select(item =>
    {
        item.Value = item.Quantity * item.Product.Value;

        if (item.Product.Promo is not null)
        {
            item.Value = CalculatePromoValue(item);
        }

        return item;
    });

    return saleItensValues.Sum(item => item.Value);
}

double CalculatePromoValue(Sale item)
{
    switch (item.Product.Promo)
    {
        case Promo.DoisPorUm :
        {
            var quantity = (item.Quantity / 2) + (item.Quantity % 2);
            return item.Product.Value * quantity;
        }

        case Promo.CompreDoisDesc20 :
        {
            if (item.Quantity >= 2)
            {
                var discount = item.Value * 0.2;
                return item.Value - discount;
            }
            else
                return item.Value;
        }

        default:
            return item.Product.Value;
    };
}

IEnumerable<Product> LoadProducts()
{
    return new List<Product>
    {
        new("Floratta", 60, Promo.DoisPorUm),
        new("Malbec", 210, Promo.CompreDoisDesc20),
        new("Uomini", 33)
    };
}

IEnumerable<Sale> LoadSaleInput(IEnumerable<Product> products)
{
    var path  = Path.Combine(AppContext.BaseDirectory, "input.csv");

    var saleItens = File.ReadAllLines(path).Select(line => 
    {
        var data =  line.Split(",");
        var product = products.FirstOrDefault(product => product.Name == data[0])!;

        return new Sale(product, Convert.ToInt16(data[1]));
    });

    return saleItens;
}