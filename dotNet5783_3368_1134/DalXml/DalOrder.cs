using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;
/// <summary>
/// class DalOrder: 
/// Implementation of add, delete, update and return operations
/// </summary>
internal class DalOrder : IOrder
{
    const string orderPath = "Order";
    static XElement config = XmlTools.LoadConfig();
    /// <summary>
    /// The operation accepts an order and adds it in the array
    /// </summary>
    /// <returns> returns order id </returns>
    public int Add(Order entity)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (ListOrder.FirstOrDefault(orderItem => orderItem?.OrderID == entity.OrderID) != null)
            throw new Exception("id already exist");

        entity.OrderID = int.Parse(config.Element("OrderId")!.Value) + 1;
        ListOrder.Add(entity);

        XmlTools.SaveListToXMLSerializer(ListOrder, orderPath);

        return entity.OrderID;
    }
    /// <summary>
    ///  The operation deletes an order from the array (finds him by id)
    /// </summary>
    public void Delete(int id)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (ListOrder.Any(order => order?.OrderID == id ))
            ListOrder.Remove(GetById(id));
        else
            throw new DO.IdNotExistException("order does not exist");
        XmlTools.SaveListToXMLSerializer(ListOrder, orderPath);
    }
    /// <summary>
    /// The operation returns list of orders (maybe after sort)
    /// </summary>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (func == null)
            return ListOrder.Select(lec => lec).OrderBy(lec => lec?.OrderID);
        else
            return ListOrder.Where(func).OrderBy(lec => lec?.OrderID);
    }

    public Order GetByDelegate(Func<Order?, bool>? func)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        if (func != null)
        {
            var ord = ListOrder.FirstOrDefault(x => func(x));
            if (ord != null)
                return (DO.Order)ord;
        }
        throw new DO.NoObjectFoundExeption("No object is of the delegate");
    }
    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public Order GetById(int id)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        var ord = ListOrder.FirstOrDefault(x => x?.OrderID == id);

        if (ord == null)
            throw new DO.IdNotExistException("Order Id not found");
        else
            return (DO.Order)ord;
    }
    /// <summary>
    /// returns array length
    /// </summary>
    public int ListLength()
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        return ListOrder.Count();
    }
    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void Update(Order entity)
    {
        List<DO.Order?> ListOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        bool found = false;
        var foundOrder = ListOrder.FirstOrDefault(ord => ord?.OrderID == entity.OrderID);
        if (foundOrder != null)
        {
            found = true;
            int index = ListOrder.IndexOf(foundOrder);
            ListOrder[index] = entity;
        }
        if (found == false)
            throw new DO.IdNotExistException("Order Id not found");
        XmlTools.SaveListToXMLSerializer(ListOrder, orderPath);

    }
}
