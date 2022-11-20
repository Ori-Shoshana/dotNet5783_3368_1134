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
public class DalOrderItem //: IOrderItem
{
    /// <summary>
    /// The operation accepts an order item and adds it in the array
    /// </summary>
    /// <returns> returns order item id </returns>
    public int AddOrderItem(OrderItem orderIt)
    {
        foreach (OrderItem orderItem in DataSource.ListOrderItem)
        {
            if (orderIt.PrivateId == orderItem.PrivateId)
            {
                throw new Exception("they have the same Order Item alrady");
            }
        }
        DataSource.ListOrderItem.Add(orderIt);
        return orderIt.PrivateId;
    }

    /// <summary>
    ///  The operation deletes an order item from the array (finds him by id)
    /// </summary>
    public void DeleteOrderItem(int ID)
    {
        bool found = false;
        foreach (OrderItem orderItem in DataSource.ListOrderItem)
        {
            if (ID == orderItem.PrivateId)
            {
                ListOrderItem.Remove(orderItem);
                found = true;
                break;
            }
        }
        if (found == false)
        {
            throw new Exception("no order item found");
        }
    }

    /// <summary>
    /// The operation updates an order item in the array (finds him by id)
    /// </summary>
    public void UpdateOrderItem(OrderItem orderItem)
    {

        int index = 0;
        bool found = false;
        foreach(OrderItem ordIt in DataSource.ListOrderItem)
        {
            index++;
            if (ordIt.PrivateId == orderItem.PrivateId)
            {
                found = true;
                DataSource.ListOrderItem[index] = orderItem;
                break;
            }
        }
        if (found == false)
        {
            throw new Exception("no order item found");
        }
    }

    /// <summary>
    ///  The operation finds the order item (finds him by id) and returns his details
    /// </summary>
    public OrderItem Get(int orderItemId)
    {
        foreach (OrderItem orderItem in DataSource.ListOrderItem)
        {
            if (orderItem.PrivateId == orderItemId)
                return orderItem;
        }
        throw new Exception("no order item found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<OrderItem> GetOrderItemList()
    {
        List<OrderItem> orderItems = new List<OrderItem>(DataSource.ListOrderItem);
        return orderItems;
    }

    /// <summary>
    /// returns array length
    /// </summary>
    public int OrderItemLeangth()
    {
        return DataSource.ListOrderItem.Count();
    }

}