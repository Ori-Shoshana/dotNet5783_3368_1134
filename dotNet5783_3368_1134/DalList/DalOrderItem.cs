using DalApi;
using DO;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;
using DalApi;



/// <summary>
/// class DalOrderItem: 
/// Implementation of add, delete, update and return operations
/// </summary>
public class DalOrderItem : IOrderItem
{
    /// <summary>
    /// The operation accepts an order item and adds it in the array
    /// </summary>
    /// <returns> returns order item id </returns>
    public int AddOrderItem(OrderItem orderIt)
    {
        foreach (OrderItem orderItem in DataSource._orderItem)
        {
            if (orderIt.PrivateId == orderItem.PrivateId)
            {
                throw new IdAlreadyExistException("Id already exist");
            }
        }
        DataSource._orderItem.Add(orderIt);
        return orderIt.PrivateId;
    }

    /// <summary>
    ///  The operation deletes an order item from the array (finds him by id)
    /// </summary>
    public void DeleteOrderItem(int ID)
    {
        bool found = false;
        foreach (OrderItem orderItem in DataSource._orderItem)
        {
            if (ID == orderItem.PrivateId)
            {
                _orderItem.Remove(orderItem);
                found = true;
                break;
            }
        }
        if (found == false)
        {
            throw new IdNotExistException("Order Id not found");
        }
    }

    /// <summary>
    /// The operation updates an order item in the array (finds him by id)
    /// </summary>
    public void UpdateOrderItem(OrderItem orderItem)
    {

        int index = 0;
        bool found = false;
        foreach(OrderItem ordIt in DataSource._orderItem)
        {
            index++;
            if (ordIt.PrivateId == orderItem.PrivateId)
            {
                found = true;
                DataSource._orderItem[index] = orderItem;
                break;
            }
        }
        if (found == false)
        {
            throw new IdNotExistException("Order Id not found");
        }
    }

    /// <summary>
    ///  The operation finds the order item (finds him by id) and returns his details
    /// </summary>
    public OrderItem Get(int orderItemId)
    {
        foreach (OrderItem orderItem in DataSource._orderItem)
        {
            if (orderItem.PrivateId == orderItemId)
                return orderItem;
        }
        throw new IdNotExistException("Order Id not found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<OrderItem> GetOrderItemList()
    {
        List<OrderItem> orderItems = new List<OrderItem>(DataSource._orderItem);
        return orderItems;
    }

    /// <summary>
    /// returns array length
    /// </summary>
    public int OrderItemLeangth()
    {
        return DataSource._orderItem.Count();
    }

}