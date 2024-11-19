
// класс, представляющий заявку от покупателя на ожидаемый товар.

public class Request
{
    public int Id { get; set; }

    // покупатель, сделавший заявку
    public Customer Customer { get; set; }

    // товар, на который сделана заявка
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }
}
