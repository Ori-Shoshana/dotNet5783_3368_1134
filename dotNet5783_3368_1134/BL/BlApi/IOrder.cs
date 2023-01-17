using BO;
using DalApi;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    ///decleration of function get orders
    /// requests a list of orders from the data layer
    /// returns list of orders (from type order for list)
    /// </summary>
    public IEnumerable<OrderForList?> GetOrders(Func<DO.Order?, bool>? func = null);
    /// <summary>
    /// decleration of function order details
    /// receives id 
    /// checks if the order exists in the data layer
    /// the function receives order id , checking integrity
    /// Throws exceptions when needed
    /// returns order
    public Order OrderDetails(int id);
    /// <summary>
    /// decleration of function shipping update
    /// checks if the order exists in the data layer and updates ship date
    /// the function receives order id , checking integrity , placing an order
    /// Throws exceptions when needed
    /// trying tu update the data layer
    /// returns the order (after update)
    public Order ShippingUpdate(int id);
    /// <summary>
    /// decleration of function update delivery
    /// checks if the order exists in the data layer and updates ship date in the order
    /// the function receives order id , checking integrity , placing an order
    /// trying to update the data layer
    /// Throws exceptions when needed
    /// returns the order (after update)
    public Order UpdateDelivery(int id);
    /// <summary>
    /// decleration of function track
    /// recieves order id
    /// checks if the order exists in the data layer 
    /// Throws exceptions when needed
    /// returns OrderTracking
    public OrderTracking Track(int id);
    public void updateOrederM(int amount, int id, int prodId);
    public void Delete(int id);
}
