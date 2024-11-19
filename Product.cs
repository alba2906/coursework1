
// класс, представляющий автозапчасть.

public class Product
{
   
    public int Id { get; set; }

    public string Name { get; set; }

    
    public decimal Price { get; set; }

    // кол-во 
    public int Quantity { get; set; }

    // поставщик
    public Supplier Supplier { get; set; }
}

