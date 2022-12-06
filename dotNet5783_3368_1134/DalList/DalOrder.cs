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
internal class DalOrder : IOrder
{
    /// <summary>
    /// The operation accepts an order and adds it in the array
    /// </summary>
    /// <returns> returns order id </returns>
    public int Add(Order ord)
    {
        foreach(Order order in DataSource.ListOrder)
        {
            if(ord.OrderID == order.OrderID)
            {
                throw new IdAlreadyExistException("Id already exist");
            }
        }
        DataSource.ListOrder.Add(ord);
        return ord.OrderID;
    }

    /// <summary>
    ///  The operation deletes an order from the array (finds him by id)
    /// </summary>
    public void Delete(int ID)
    {

        foreach (Order order in DataSource.ListOrder)
        {
            if (ID == order.OrderID)
            {
                ListOrder.Remove(order);
                return;
            }
        }

            throw new IdNotExistException("Id not found");
    }

    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void Update(Order order)
    {
        int index =0;
        foreach (Order ord in DataSource.ListOrder)
        {
            if(ord.OrderID == order.OrderID)
            {
                ListOrder[index] = order;
                return;
            }
            index++;
        }

            throw new IdNotExistException("Id not found");
    }
    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public Order GetById(int orderId)
    {
        foreach (Order order in DataSource.ListOrder)
        {
            if (order.OrderID == orderId)
                return order;
        }
        throw new IdNotExistException("Id not found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<Order> GetAll()
    {
        return (IEnumerable<Order>)DataSource.ListOrder;
    }
    /// <summary>
    /// returns array length
    /// </summary>
    public int ListLength()
    {
        return DataSource.ListOrder.Count();
    }
}
