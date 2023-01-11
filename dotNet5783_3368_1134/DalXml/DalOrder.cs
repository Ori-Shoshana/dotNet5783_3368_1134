using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalOrder : IOrder
{
    public int Add(Order entity)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (listOrder.FirstOrDefault(orderItem => orderItem?.OrderID == entity.OrderID) != null)
            throw new Exception("id already exist");

        entity.OrderID = int.Parse(config.Element("OrderId")!.Value) + 1;
        listOrder.Add(entity);

        XmlTools.SaveListToXMLSerializer(listOrder, orderPath);

        return entity.OrderID;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public Order GetByDelegate(Func<Order?, bool>? func)
    {
        throw new NotImplementedException();
    }

    public Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public int ListLength()
    {
        throw new NotImplementedException();
    }

    public void Update(Order entity)
    {
        throw new NotImplementedException();
    }
}
