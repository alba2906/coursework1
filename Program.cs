
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        StoreManager storeManager = new StoreManager();

        // создание поставщиков
        Supplier manufacturer = new Manufacturer { Id = 1, Name = "Manufacturer A", Country = "USA", HasGuarantee = true, HasDiscounts = true };
        Supplier dealer = new Dealer { Id = 2, Name = "Dealer B", Country = "Germany", HasGuarantee = true, HasDiscounts = true };

        // создание товаров
        Product product1 = new Product { Id = 1, Name = "Engine", Price = 1000, Quantity = 10, Supplier = manufacturer };
        Product product2 = new Product { Id = 2, Name = "Tires", Price = 200, Quantity = 50, Supplier = dealer };

        // создание заказа
        Order order = new Order { Id = 1, OrderDate = DateTime.Now, Products = new List<Product> { product1, product2 }, Supplier = manufacturer };

        storeManager.AddOrder(order);

        // Пример запроса: Получить перечень и общее число поставщиков определенной категории
        List<Supplier> suppliers = storeManager.GetSuppliersByCategory("Manufacturer");
        Console.WriteLine($"Общее количество производителей: {suppliers.Count}");

        // Пример запроса: Получить сведения о конкретном виде деталей
        List<Product> productDetails = storeManager.GetProductDetails("Engine");
        Console.WriteLine($"Общее количество деталей: {productDetails.Count}");

        // Пример запроса: Получить перечень и общее число покупателей, купивших указанный вид товара
        List<Customer> customers = storeManager.GetCustomersByProduct("Engine");
        Console.WriteLine($"Общее число покупателей, купивших указанный вид товара: {customers.Count}");
      

        // Пример запроса: Вывести в порядке возрастания десять самых продаваемых деталей
        List<Product> topSellingProducts = storeManager.GetTopSellingProducts(10);
        Console.WriteLine($"10 самых продаваемых деталей: {string.Join(", ", topSellingProducts.Select(p => p.Name))}");

        // Пример запроса: Получить среднее число продаж на месяц по любому виду деталей
        decimal averageSalesPerMonth = storeManager.GetAverageSalesPerMonth("Engine");
        Console.WriteLine($"Средний объем продаж двигателей в месяц: {averageSalesPerMonth}");

        // Пример запроса: Получить долю товара конкретного поставщика в процентах, деньгах, единицах от всего оборота магазина
        decimal supplierShare = storeManager.GetSupplierShare(manufacturer);
        Console.WriteLine($"Доля поставщика для производителя А: {supplierShare}");

        // Пример запроса: Получить накладные расходы в процентах от объема продаж
        decimal overheadCostsPercentage = storeManager.GetOverheadCostsPercentage();
        Console.WriteLine($"Процент накладных расходов: {overheadCostsPercentage}");

        // Пример запроса: Получить перечень и общее количество непроданного товара на складе за определенный период (залежалого)
        List<Product> unsoldProducts = storeManager.GetUnsoldProducts(DateTime.Now.AddDays(-30), DateTime.Now);
        Console.WriteLine($"Общее количество непроданного товара: {unsoldProducts.Count}");

        // Пример запроса: Получить перечень и общее количество бракованного товара, пришедшего за определенный период
        List<Product> defectiveProducts = storeManager.GetDefectiveProducts(DateTime.Now.AddDays(-30), DateTime.Now);
        Console.WriteLine($"Общее количество бракованного товара: {defectiveProducts.Count}");

        // Пример запроса: Получить перечень, общее количество и стоимость товара, реализованного за конкретный день
        List<Product> soldProducts = storeManager.GetSoldProducts(DateTime.Now);
        Console.WriteLine($"Общее кол-во товаров на сегодняшний день: {soldProducts.Count}");

        // Пример запроса: Получить кассовый отчет за определенный период
        List<Order> cashReport = storeManager.GetCashReport(DateTime.Now.AddDays(-30), DateTime.Now);
        Console.WriteLine($"Общее кол-во заказов в кассе: {cashReport.Count}");

        // Пример запроса: Получить инвентаризационную ведомость
        List<Product> inventoryReport = storeManager.GetInventoryReport();
        Console.WriteLine($"Общее количество товара в отчете о запасах: {inventoryReport.Count}");

        // Пример запроса: Получить скорость оборота денежных средств, вложенных в товар
        decimal inventoryTurnoverRate = storeManager.GetInventoryTurnoverRate();
        Console.WriteLine($"Скорость оборачиваемости запасов: {inventoryTurnoverRate}");

        // Пример запроса: Подсчитать сколько пустых ячеек на складе и сколько он сможет вместить товара
        int emptyCellsCount = storeManager.GetEmptyCellsCount();
        int totalCapacity = storeManager.GetTotalCapacity();
        Console.WriteLine($"Количество пустых ячеек: {emptyCellsCount}, Общая вместимость: {totalCapacity}");

        // Пример запроса: Получить перечень и общее количество заявок от покупателей на ожидаемый товар
        List<Request> requests = storeManager.GetRequestsByProduct("Engine");
        Console.WriteLine($"Общее количество запросов на товар: {requests.Count}");

        // Пример запроса: Подсчитать на какую сумму даны заявки
        decimal totalRequestAmount = storeManager.GetTotalRequestAmount("Engine");
        Console.WriteLine($"Сумма: {totalRequestAmount}");
    }
}
