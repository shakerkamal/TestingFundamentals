namespace TestingFundamentals.Mocking;

public class OrderService
{
    private readonly IStorage _storage;

    public OrderService(IStorage storage)
    {
        _storage = storage;
    }

    public int PlaceOrder(Order order)
    {
        var orderId = _storage.Store(order);

        return orderId;
    }
}

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public interface IStorage
{
    int Store(Order order);
}

public class Storage : IStorage
{
    private static List<Order> _orders = new List<Order>();
    public int Store(Order order)
    {
        _orders.Add(order);
        return order.Id;
    }
}
