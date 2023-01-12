using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Dal;

internal class DalOrderItem : IOrderItem
{
    const string orderItemPath = "OrderItem";
    static XElement config = XmlTools.LoadConfig();

    /// <summary>
    /// The operation accepts an order item and adds it in the array
    /// </summary>
    /// <returns> returns order item id </returns>
    public int Add(OrderItem ordItem)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        if (listOrderItem.FirstOrDefault(orderItem => orderItem?.OrderItemID == ordItem.OrderItemID) != null)
            throw new DO.IdAlreadyExistException("order item Id already exists");

        ordItem.OrderItemID = int.Parse(config.Element("OrderItemID")!.Value) + 1;
        XmlTools.SaveConfigXElement("OrderItemID", ordItem.OrderItemID);
        listOrderItem.Add(ordItem);

        XmlTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);

        return ordItem.OrderItemID;
    }
    /// <summary>
    ///  The operation deletes an order item from the array (finds him by id)
    /// </summary>
    public void Delete(int ordItemId)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

         if (ListOrderItem.Any(orderItem => orderItem?.OrderItemID == ordItemId))
            ListOrderItem.Remove(GetById(ordItemId));
        else
            throw new DO.IdNotExistException("Order item not exist");

        XmlTools.SaveListToXMLSerializer(ListOrderItem, orderItemPath);
    }
    /// <summary>
    /// The operation returns the full array
    /// </summary>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        var orderItems = ListOrderItem.Where(ordItem => func == null || func(ordItem)).ToList();
        return orderItems;
    }
    public OrderItem GetByDelegate(Func<OrderItem?, bool>? func)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        if (func != null)
        {
            var ordItem = ListOrderItem.FirstOrDefault(item => func(item));
            if (ordItem != null)
                return (DO.OrderItem)ordItem;
        }
        throw new DO.NoObjectFoundExeption("No object is of the delegate");
    }
    /// <summary>
    ///  The operation finds the order item (finds him by id) and returns his details
    /// </summary>
    public OrderItem GetById(int ordItemId)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        var orderItem = ListOrderItem.FirstOrDefault(x => x?.OrderItemID == ordItemId);
        if (orderItem == null)
            throw new DO.IdNotExistException("order item id not found");
        else
            return (DO.OrderItem)orderItem;
    }
    /// <summary>
    /// returns array length
    /// </summary>
    public int ListLength()
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        return ListOrderItem.Count();
    }
    /// <summary>
    /// The operation updates an order item in the array (finds him by id)
    /// </summary>
    public void Update(OrderItem ordItem)
    {
        List<DO.OrderItem?> ListOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        bool found = false;
        var foundOrderItem = ListOrderItem.FirstOrDefault(Item => Item?.OrderItemID == ordItem.OrderItemID);
        if (foundOrderItem != null)
        {
            found = true;
            int index = ListOrderItem.IndexOf(foundOrderItem);
            ListOrderItem[index] = ordItem;
        }
        if (found == false)
            throw new DO.IdNotExistException("order item id not found");
        XmlTools.SaveListToXMLSerializer(ListOrderItem, orderItemPath);
    }
}
