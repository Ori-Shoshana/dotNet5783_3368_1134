using BO;
namespace BlApi;

public interface IOrder
{
    //public OrderForList()
    public Order OrderDetails(int id);
    public Order UpdateShip(int id);
    public Order UpdateDelivery(int id);
    public Order Track(int id);
}
