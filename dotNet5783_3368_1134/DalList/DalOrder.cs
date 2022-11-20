using DO;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
namespace Dal;
using static Dal.DataSource;

/// <summary>
/// class DalOrder: 
/// Implementation of add, delete, update and return operations
/// </summary>
public class DalOrder
{
    /// <summary>
    /// The operation accepts an order and adds it in the array
    /// </summary>
    /// <returns> returns order id </returns>
    public int AddOrder(Order ord)
    {
        ord.PrivateId = DataSource.config.OrderIndex;
        DataSource._order[DataSource.config.OrderIndex++] = ord;
        return ord.PrivateId;
    }

    /// <summary>
    ///  The operation deletes an order from the array (finds him by id)
    /// </summary>
    public void DeleteOrder(int ID)
    {
        int i;
        for (i=0; i<DataSource.config.OrderIndex; i++)
        {
            if (_order[i].PrivateId == ID)
                break;
        }
        if (i == DataSource.config.OrderIndex)
            throw new Exception("no order found");
        else
        {
            for (; i < DataSource.config.OrderIndex; i++)
            {
                _order[i] = _order[i++];
            }
            DataSource.config.OrderIndex--;
        }
    }

    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void UpdateOrder(Order order)
    {
        int i;
        int x = 0;
        for (i = 0; i < DataSource.config.OrderItemIndex; i++)
        {
            if (_order[i].PrivateId == order.PrivateId)
            {
                _order[i] = order;
                x = 1;
                break;
            }
        }
        if(x == 0)
        {
            throw new Exception("no order found");
        }
    }
    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public Order Get(int orderId)
    {
        for(int i=0; i<DataSource.config.OrderIndex; i++)
        {
            if (_order[i].PrivateId == orderId)
                return _order[i];
        }
        throw new Exception("no ordeer found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public static IEnumerable<Order> GetOrderArray()
    {
        List<Order> orderArrey = new Order[100];
        for (int i = 0; i < DataSource.config.OrderIndex; i++)
        {
            orderArrey[i] = _order[i];
        }
        return orderArrey;
    }
    /// <summary>
    /// returns array length
    /// </summary>
    public int OrderLeangth()
    {
        return DataSource.config.OrderIndex;
    }
}
