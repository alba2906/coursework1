using System;
using System.Collections.Generic;


// класс, представляющий заказ.

public class Order
{
    
    public int Id { get; set; }

    
    public DateTime OrderDate { get; set; }

    // список товаров в заказе
    public List<Product> Products { get; set; }

    public Supplier Supplier { get; set; }
}
