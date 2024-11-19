using System;


/// базовый класс для всех поставщиков.
public abstract class Supplier
{
    
    public int Id { get; set; }
 
    public string Name { get; set; }

    public string Country { get; set; }

    // метод для получения типа поставщика
    public abstract string GetSupplierType();
}



// производитель, который может предложить гарантию и скидки.
public class Manufacturer : Supplier
{
    // указывает, предоставляет ли производитель гарантию
    public bool HasGuarantee { get; set; }

    // указывает, предоставляет ли производитель скидки
    public bool HasDiscounts { get; set; }

    
    public override string GetSupplierType()
    {
        return "Manufacturer";
    }
}


/// дилер, который может предложить гарантию и скидки.

public class Dealer : Supplier
{
    public bool HasGuarantee { get; set; }

    public bool HasDiscounts { get; set; }

    public override string GetSupplierType()
    {
        return "Dealer";
    }
}


/// небольшое производство, которое не предлагает гарантию и скидки.

public class SmallProduction : Supplier
{
 
    public override string GetSupplierType()
    {
        return "SmallProduction";
    }
}


/// мелкий магазин, который не предлагает гарантию и скидки.

public class SmallShop : Supplier
{
 
    public override string GetSupplierType()
    {
        return "SmallShop";
    }
}

