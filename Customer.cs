﻿using System.Collections.Generic;

/// класс, представляющий покупателя.

public class Customer
{
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Order> Orders { get; set; }
}