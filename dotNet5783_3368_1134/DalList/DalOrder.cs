using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Dal;
using static Dal.DataSource;
using DalApi;
using DO;


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
    public int Add(DO.Order ord)
    {
        var check = (from order in listOrder select order?.OrderID).Where(temp => temp == ord.OrderID);
        if (check.Count() == 0)
        {
            ListOrder.Add(ord);
            return ord.OrderID;
        }
        throw new DO.IdAlreadyExistException("order Id already exists");
    }
    /*if (DataSource.ListOrder.Any(order => order?.OrderID == ord.OrderID))
       throw new DO.IdAlreadyExistException("Order Id already exists");
       DataSource.ListOrder.Add(ord);return ord.OrderID;*/
    /// <summary>
    ///  The operation deletes an order from the array (finds him by id)
    /// </summary>
    public void Delete(int OrderId)
    {
        if (DataSource.ListOrder.Any(order => order?.OrderID == OrderId))
            DataSource.listOrder.Remove(GetById(OrderId));
        else
            throw new DO.IdNotExistException("order does not exist");
    }

    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void Update(DO.Order order)
    {
        bool found = false;
        var foundOrder = DataSource.listOrder.FirstOrDefault(ord => ord?.OrderID == order.OrderID);
        if (foundOrder != null)
        {
            found = true;
            int index = DataSource.listOrder.IndexOf(foundOrder);
            DataSource.listOrder[index] = order;
        }
        if (found == false)
            throw new DO.IdNotExistException("Order Id not found");
    }
    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public DO.Order GetById(int orderId)
    {
        var ord = DataSource.listOrder.FirstOrDefault(x => x?.OrderID == orderId);

        if (ord == null)
            throw new DO.IdNotExistException("Order Id not found");
        else
            return (DO.Order) ord;
    }

    /// <summary>
    /// The operation returns list of orders (maybe after sort)
    /// </summary>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func)
    {
        var orders = listOrder.Where(ord => func == null || func(ord)).ToList();
        return orders;
    }
    
    /// <summary>
    /// returns array length
    /// </summary>
    public int ListLength()
    {
        return DataSource.ListOrder.Count();
    }

    public DO.Order GetByDelegate(Func<DO.Order?, bool>? func)
    {
        if (func != null)
        {
            var ord = listOrder.FirstOrDefault(x => func(x));
            if (ord != null)
                return (DO.Order) ord;
        }
        throw new DO.NoObjectFoundExeption("No object is of the delegate");
    }
}
