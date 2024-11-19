using System;
using System.Collections.Generic;
using System.Linq;


/// класс, управляющий всеми операциями магазина.

public class StoreManager
{
    // список заказов
    public List<Order> Orders { get; set; }

    // список покупателей
    public List<Customer> Customers { get; set; }

    // склад
    public Warehouse Warehouse { get; set; }

    // список заявок
    public List<Request> Requests { get; set; }

    // касса
    public CashRegister CashRegister { get; set; }

 
    public StoreManager()
    {
        Orders = new List<Order>();
        Customers = new List<Customer>();
        Warehouse = new Warehouse();
        Requests = new List<Request>();
        CashRegister = new CashRegister();
    }

    // метод для добавления заказа в магазин
    public void AddOrder(Order order)
    {
        Orders.Add(order);
        foreach (var product in order.Products)
        {
            Warehouse.AddProduct(Warehouse.Storage.Count + 1, product);
        }
        CashRegister.AddOrder(order);
    }

    // метод для получения перечня и общего числа поставщиков определенной категории
    public List<Supplier> GetSuppliersByCategory(string category)
    {
        return Orders.SelectMany(o => o.Products.Select(p => p.Supplier))
                     .Where(s => s.GetSupplierType() == category)
                     .Distinct()
                     .ToList();
    }

    // метод для получения сведений о конкретном виде деталей
    public List<Product> GetProductDetails(string productName)
    {
        return Orders.SelectMany(o => o.Products)
                     .Where(p => p.Name == productName)
                     .ToList();
    }

    // метод для получения перечня и общего числа покупателей, купивших указанный вид товара
    public List<Customer> GetCustomersByProduct(string productName)
    {
        return Customers.Where(c => c.Orders.Any(o => o.Products.Any(p => p.Name == productName)))
                        .ToList();
    }

    // метод для получения перечня, объема и номера ячейки для всех деталей, хранящихся на складе
    public List<Product> GetAllStoredProducts()
    {
        return Warehouse.Storage.Values.ToList();
    }

    // метод для получения списка самых продаваемых товаров
    public List<Product> GetTopSellingProducts(int count)
    {
        return Orders.SelectMany(o => o.Products)
                     .GroupBy(p => p.Name)
                     .OrderByDescending(g => g.Sum(p => p.Quantity))
                     .Take(count)
                     .Select(g => g.First())
                     .ToList();
    }

    // метод для получения списка самых дешевых поставщиков
    public List<Supplier> GetCheapestSuppliers(int count)
    {
        return Orders.SelectMany(o => o.Products.Select(p => p.Supplier))
                     .GroupBy(s => s.Name)
                     .OrderBy(g => g.Average(s => s.GetType() == typeof(Manufacturer) || s.GetType() == typeof(Dealer) ? 1 : 0))
                     .Take(count)
                     .Select(g => g.First())
                     .ToList();
    }

    // метод для получения среднего числа продаж на месяц по указанному виду деталей
    public decimal GetAverageSalesPerMonth(string productName)
    {
        var totalSales = Orders.SelectMany(o => o.Products)
                               .Where(p => p.Name == productName)
                               .Sum(p => p.Quantity);
        var totalMonths = (DateTime.Now - Orders.Min(o => o.OrderDate)).TotalDays / 30;
        return (decimal)(totalSales / totalMonths);
    }

    // метод для получения доли товара конкретного поставщика в процентах, деньгах, единицах от всего оборота магазина
    public decimal GetSupplierShare(Supplier supplier)
    {
        var supplierSales = Orders.SelectMany(o => o.Products)
                                  .Where(p => p.Supplier == supplier)
                                  .Sum(p => p.Quantity);
        var totalSales = Orders.SelectMany(o => o.Products).Sum(p => p.Quantity);
        return supplierSales / totalSales;
    }

    // метод для получения накладных расходов в процентах от объема продаж
    public decimal GetOverheadCostsPercentage()
    {
        var totalSales = Orders.SelectMany(o => o.Products).Sum(p => p.Quantity * p.Price);
        var overheadCosts = Orders.Sum(o => o.Products.Sum(p => p.Price * 0.1m)); // Предположим, что накладные расходы составляют 10% от стоимости товара
        return overheadCosts / totalSales;
    }

    // метод для получения перечня и общего количества непроданного товара на складе за определенный период (залежалого) и его объем от общего товара в процентах
    public List<Product> GetUnsoldProducts(DateTime startDate, DateTime endDate)
    {
        return Warehouse.Storage.Values
                       .Where(p => p.Quantity > 0 && p.Supplier.GetType() != typeof(Manufacturer) && p.Supplier.GetType() != typeof(Dealer))
                       .ToList();
    }

    // метод для получения перечня и общего количества бракованного товара, пришедшего за определенный период и список поставщиков, поставивших товар
    public List<Product> GetDefectiveProducts(DateTime startDate, DateTime endDate)
    {
        
        return Warehouse.Storage.Values
                       .Where(p => p.Quantity > 0 && p.Supplier.GetType() != typeof(Manufacturer) && p.Supplier.GetType() != typeof(Dealer))
                       .ToList();
    }

    // метод для получения перечня, общего количества и стоимости товара, реализованного за конкретный день
    public List<Product> GetSoldProducts(DateTime date)
    {
        return Orders.Where(o => o.OrderDate.Date == date.Date)
                     .SelectMany(o => o.Products)
                     .ToList();
    }

    // метод для получения кассового отчета за определенный период
    public List<Order> GetCashReport(DateTime startDate, DateTime endDate)
    {
        return CashRegister.GetCashReport(startDate, endDate);
    }

    // метод для получения инвентаризационной ведомости
    public List<Product> GetInventoryReport()
    {
        return Warehouse.Storage.Values.ToList();
    }

    // метод для получения скорости оборота денежных средств, вложенных в товар
    public decimal GetInventoryTurnoverRate()
    {
        var totalSales = Orders.SelectMany(o => o.Products).Sum(p => p.Quantity);
        var totalInventory = Warehouse.Storage.Values.Sum(p => p.Quantity);
        return totalSales / totalInventory;
    }

    // метод для подсчета количества пустых ячеек на складе
    public int GetEmptyCellsCount()
    {
        return Warehouse.Storage.Count(kvp => kvp.Value.Quantity == 0);
    }

    // метод для получения общей вместимости склада
    public int GetTotalCapacity()
    {
        return Warehouse.Storage.Count;
    }

    // метод для получения перечня и общего количества заявок от покупателей на ожидаемый товар
    public List<Request> GetRequestsByProduct(string productName)
    {
        return Requests.Where(r => r.Product.Name == productName).ToList();
    }

    // метод для подсчета общей суммы заявок на указанный товар
    public decimal GetTotalRequestAmount(string productName)
    {
        return Requests.Where(r => r.Product.Name == productName).Sum(r => r.TotalAmount);
    }
}

