using DO;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;


/// <summary>
/// class DalOrderItem: 
/// Implementation of add, delete, update and return operations
/// </summary>
public class DalOrderItem
{
    /// <summary>
    /// The operation accepts an order item and adds it in the array
    /// </summary>
    /// <returns> returns order item id </returns>
    public int AddOrderItem(OrderItem ordItem)
    {
        ordItem.PrivateId = DataSource.config.OrderItemIndex;
        DataSource._orderItem[DataSource.config.OrderItemIndex++] = ordItem;
        return ordItem.PrivateId;
    }

    /// <summary>
    ///  The operation deletes an order item from the array (finds him by id)
    /// </summary>
    public void DeleteOrderItem(int ID)
    {
        int i;
        for (i = 0; i < DataSource.config.OrderItemIndex; i++)
        {
            if (_orderItem[i].PrivateId == ID)
                break;
        }
        if (i == DataSource.config.OrderItemIndex)
            throw new Exception("no order item found");
        else
        {
            for (; i < DataSource.config.OrderItemIndex; i++)
            {
                _orderItem[i].PrivateId = _orderItem[i++].PrivateId;
            }
            DataSource.config.OrderItemIndex--;
        }
    }

    /// <summary>
    /// The operation updates an order item in the array (finds him by id)
    /// </summary>
    public void UpdateOrderItem(OrderItem orderItem)
    {
        
        int i;
        int x = 0;
        for (i = 0; i < DataSource.config.OrderIndex; i++)
        {
            if (_orderItem[i].PrivateId == orderItem.PrivateId)
            {
                _orderItem[i] = orderItem;
                x = 1;
                break;
            }
        }
        if (x == 0)
        {
            throw new Exception("no order item found");
        }
    }

    /// <summary>
    ///  The operation finds the order item (finds him by id) and returns his details
    /// </summary>
    public OrderItem Get(int orderItemId)
    {
        for (int i = 0; i < DataSource.config.OrderItemIndex; i++)
        {
            if (_orderItem[i].PrivateId == orderItemId)
            {
                return _orderItem[i];
            }
        }
        throw new Exception("no order item found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public static OrderItem[] GetOrderItemArray()
    {
        OrderItem[] orderItemArrey = new OrderItem[200];
        for(int i=0; i < DataSource.config.OrderItemIndex; i++)
        {
            orderItemArrey[i] = _orderItem[i];
        }
        return orderItemArrey;
    }

    /// <summary>
    /// returns array length
    /// </summary>
    public int OrderItemLeangth()
    {
        return DataSource.config.OrderItemIndex;
    }

}