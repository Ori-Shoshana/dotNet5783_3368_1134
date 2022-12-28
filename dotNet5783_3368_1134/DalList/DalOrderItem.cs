using DalApi;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;
using DalApi;
using DO;



/// <summary>
/// class DalOrderItem: 
/// Implementation of add, delete, update and return operations
/// </summary>
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// The operation accepts an order item and adds it in the array
    /// </summary>
    /// <returns> returns order item id </returns>
    public int Add(DO.OrderItem ordItem)
    {
        var check = (from ord in ListOrderItem select ord?.OrderItemID).Where(temp => temp == ordItem.OrderItemID);
        if (check.Count() == 0)
        {
            ListOrderItem.Add(ordItem);
            return ordItem.OrderItemID;
        }
        throw new DO.IdAlreadyExistException("order item Id already exists");
    }
        /*if (DataSource.ListOrderItem.Any(orderItem => orderItem?.OrderItemID == ordItem.OrderItemID))
        throw new DO.IdAlreadyExistException("Id already exist");
        DataSource.ListOrderItem.Add(ordItem);
        return ordItem.OrderItemID;*/
    /// <summary>
    ///  The operation deletes an order item from the array (finds him by id)
    /// </summary>
    public void Delete(int ordItemId)
    {
        if (DataSource.ListOrderItem.Any(orderItem => orderItem?.OrderItemID == ordItemId))
            DataSource.ListOrderItem.Remove(GetById(ordItemId));
        else
            throw new DO.IdNotExistException("Order item not exist");
    }

    /// <summary>
    /// The operation updates an order item in the array (finds him by id)
    /// </summary>
    public void Update(DO.OrderItem orderItem)
    {
        bool found = false;
        var foundOrderItem = ListOrderItem.FirstOrDefault(ordItem => ordItem?.OrderItemID == orderItem.OrderItemID);
        if (foundOrderItem != null)
        {
            found = true;
            int index = ListOrderItem.IndexOf(foundOrderItem);
            ListOrderItem[index] = orderItem;
        }
        if (found == false)
            throw new DO.IdNotExistException("order item id not found");
    }

    /// <summary>
    ///  The operation finds the order item (finds him by id) and returns his details
    /// </summary>
    public DO.OrderItem GetById(int orderItemId)
    {
        var orderItem = DataSource.ListOrderItem.FirstOrDefault(x => x?.OrderItemID == orderItemId);
        if (orderItem == null)
            throw new DO.IdNotExistException("order item id not found");
        else
            return (DO.OrderItem) orderItem;
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func)
    {
        var orderItems = ListOrderItem.Where(ordItem => func == null || func(ordItem)).ToList();
        return orderItems;
    }
    
    /// <summary>
    /// returns array length
    /// </summary>
    public int ListLength()
    {
        return DataSource.ListOrderItem.Count();
    }

    public DO.OrderItem GetByDelegate(Func<DO.OrderItem?, bool>? func)
    {
        if (func != null)
        {
            var ordItem = ListOrderItem.FirstOrDefault(item => func(item));
            if (ordItem != null)
                return (DO.OrderItem) ordItem;
        }
        throw new DO.NoObjectFoundExeption("No object is of the delegate");
    }
}