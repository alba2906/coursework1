using System.Collections.Generic;


// класс, представляющий склад.

public class Warehouse
{
    // хранилище товаров на складе
    public Dictionary<int, Product> Storage { get; set; }

    // конструктор класса 
    public Warehouse()
    {
        Storage = new Dictionary<int, Product>();
    }

    // метод для добавления товара на склад
    public void AddProduct(int cellNumber, Product product)
    {
        if (Storage.ContainsKey(cellNumber))
        {
            Storage[cellNumber].Quantity += product.Quantity;
        }
        else
        {
            Storage[cellNumber] = product;
        }
    }

    // метод для получения товара из ячейки
    public Product GetProduct(int cellNumber)
    {
        if (Storage.ContainsKey(cellNumber))
        {
            return Storage[cellNumber];
        }
        return null;
    }
}
