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
internal class DalOrderItem : IOrderItem
{

    /// <summary>
    /// The operation accepts an order item and adds it in the array
    /// </summary>
    /// <returns> returns order item id </returns>
    public int Add(OrderItem orderId)
    {

        if (DataSource.ListOrderItem.Any(orderItem => orderId.OrderItemID == orderItem?.OrderItemID))
        {
            throw new IdAlreadyExistException("Id already exist");
        }
        DataSource.ListOrderItem.Add(orderId);
        return orderId.OrderItemID;
    }

    /// <summary>
    ///  The operation deletes an order item from the array (finds him by id)
    /// </summary>
    public void Delete(int ID)
    {
       
        if (!DataSource.ListOrderItem.Any(orderItem => ID == orderItem?.OrderItemID))
        {
            throw new IdNotExistException("Order Id not found");
        }

        DataSource.ListOrderItem.RemoveAll(orderItem => ID == orderItem?.OrderItemID);
    }

    /// <summary>
    /// The operation updates an order item in the array (finds him by id)
    /// </summary>
    public void Update(OrderItem orderItem)
    {

        int index = 0;
        foreach (OrderItem ordIt in DataSource.ListOrderItem)
        {
            if (ordIt.OrderItemID == orderItem.OrderItemID)
            {
                DataSource.ListOrderItem[index] = orderItem;
                return;
            }
            index++;
        }
        throw new IdNotExistException("Order Id not found");
        
    }

    /// <summary>
    ///  The operation finds the order item (finds him by id) and returns his details
    /// </summary>
    public OrderItem GetById(int orderItemId)
    {
        foreach (OrderItem orderItem in DataSource.ListOrderItem)
        {
            if (orderItem.OrderItemID == orderItemId)
                return orderItem;
        }
        throw new IdNotExistException("Order Id not found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func)
    {
   
        List<OrderItem?> orderItem = new List<OrderItem?>();
        if (func != null)
        {
            foreach (OrderItem? item1 in ListOrderItem)
            {
                if (func(item1))
                {
                    orderItem.Add(item1);
                }
            }
            return orderItem;
        }
        else
        {
            foreach (OrderItem? item1 in ListOrderItem)
            {
                orderItem.Add(item1);
            }
            return orderItem;
        }
    }
    
    /// <summary>
    /// returns array length
    /// </summary>
    public int ListLength()
    {
        return DataSource.ListOrderItem.Count();
    }

    public OrderItem GetByDelegate(Func<OrderItem?, bool>? func)
    {
        if (func != null)
        {
            foreach (OrderItem orderItem in ListOrderItem)
            {
                if (func(orderItem))
                {
                    return orderItem;
                }
            }
        }
        throw new NoObjectFoundExeption("No object is of the delegate");
    }
}