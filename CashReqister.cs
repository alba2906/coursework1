using System;
using System.Collections.Generic;
using System.Linq;


// класс, управляющий кассовыми операциями.

public class CashRegister
{
    // список заказов
    public List<Order> Orders { get; set; }

    // конструктор класса 
    public CashRegister()
    {
        Orders = new List<Order>();
    }

    // метод для добавления заказа в кассу
    public void AddOrder(Order order)
    {
        Orders.Add(order);
    }

    // метод для получения общей выручки за указанный период
    public decimal GetTotalRevenue(DateTime startDate, DateTime endDate)
    {
        return Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                     .Sum(o => o.Products.Sum(p => p.Price * p.Quantity));
    }

    // метод для получения кассового отчета за указанный период
    public List<Order> GetCashReport(DateTime startDate, DateTime endDate)
    {
        return Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
    }
}


