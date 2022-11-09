using DO;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
namespace Dal;
using static Dal.DataSource;

public class DalOrder
{
    public int AddOrder(Order ord)
    {
        ord.PrivateId = DataSource.config.OrderIndex;
        DataSource._order[DataSource.config.OrderIndex++] = ord;
        return ord.PrivateId;
    }

    public void DeleteOrder(Order order)
    {
        int i;
        for (i=0; i<DataSource.config.OrderIndex; i++)
        {
            if (_order[i].PrivateId == order.PrivateId)
                break;
        }
        if (i == DataSource.config.OrderIndex)
            throw new Exception("no order found");
        else
        {
            for (; i < DataSource.config.OrderIndex; i++)
            {
                _order[i].PrivateId = _order[i++].PrivateId;
            }
            DataSource.config.OrderIndex--;
        }
    }

    public void UpdateOrder(Order order)
    {
        int i;
        int x = 0;
        for (i = 0; i < DataSource.config.OrderIndex; i++)
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

    public Order Get(int orderId)
    {
        for(int i=0; i<DataSource.config.OrderIndex; i++)
        {
            if (_order[i].PrivateId == orderId)
                return _order[i];
        }
        throw new Exception("no ordeer found");
    }
    public Order[] GetOrderArray()
    {
        Order[] orderArrey = new Order[100];
        for (int i = 0; i < DataSource.config.OrderItemIndex; i++)
        {
            orderArrey[i].PrivateId = _order[i].PrivateId;
            orderArrey[i].CustomerName = _order[i].CustomerName;
            orderArrey[i].CustomerEmail = _order[i].CustomerEmail;
            orderArrey[i].CustomerAdrss = _order[i].CustomerAdrss;
            orderArrey[i].OrderDate = _order[i].OrderDate;
            orderArrey[i].ShipDate = _order[i].ShipDate;
            orderArrey[i].DelivoryDate = _order[i].DelivoryDate;

        }
        return orderArrey;
    }

}
