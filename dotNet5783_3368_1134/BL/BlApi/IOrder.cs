using BO;
namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetOrders();
    public Order OrderDetails(int id);
    public Order ShippingUpdate(int id);
    public Order UpdateDelivery(int id);
    public OrderTracking Track(int id);
}
