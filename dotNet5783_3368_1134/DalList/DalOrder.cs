using DO;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Dal;
using static Dal.DataSource;
using DalApi;


/// <summary>
/// class DalOrder: 
/// Implementation of add, delete, update and return operations
/// </summary>
//internal class DalOrder : IOrder
public class DalOrder //: IOrder

{
    /// <summary>
    /// The operation accepts an order and adds it in the array
    /// </summary>
    /// <returns> returns order id </returns>
    public int AddOrder(Order ord)
    {
        foreach(Order order in DataSource.ListOrder)
        {
            if(ord.PrivateId == order.PrivateId)
            {
                throw new IdAlreadyExistException("Id already exist");
            }
        }
        DataSource.ListOrder.Add(ord);
        return ord.PrivateId;
    }

    /// <summary>
    ///  The operation deletes an order from the array (finds him by id)
    /// </summary>
    public void DeleteOrder(int ID)
    {
        bool found = false;
        foreach (Order order in DataSource.ListOrder)
        {
            if (ID == order.PrivateId)
            {
                ListOrder.Remove(order);
                found = true;
                break;
            }
        }
        if(found == false)
        {
            throw new IdNotExistException("Id not found");
        }
    }

    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void UpdateOrder(Order order)
    {
        int index =0;
        bool found = false;
        foreach (Order ord in DataSource.ListOrder)
        {
            index++;
            if(ord.PrivateId == order.PrivateId)
            {
                found = true;
                ListOrder[index] = order;
                break;
            }
        }
        if(found == false)
        {
            throw new IdNotExistException("Id not found");
        }
    }
    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public Order Get(int orderId)
    {
        foreach (Order order in DataSource.ListOrder)
        {
            if (order.PrivateId == orderId)
                return order;
        }
        throw new IdNotExistException("Id not found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<Order> GetOrderList()
    {
       List<Order> orders = new List<Order>(DataSource.ListOrder);
        return orders;
    }
    /// <summary>
    /// returns array length
    /// </summary>
    public int OrderLeangth()
    {
        return DataSource.ListOrder.Count();
    }
}
