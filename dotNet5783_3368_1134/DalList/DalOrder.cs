using DO;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Diagnostics;
namespace Dal;
using static Dal.DataSource;

public class DalOrder
{
    public int addOrder(Order ord)
    {
        ord.OrderId = DataSource.config.OrderIndex;
        DataSource._order[DataSource.config.OrderIndex++] = ord;
        return ord.OrderId;
    }

    public void deleteOrder(Order order)
    {
        int i;
        for (i=0; i<DataSource.config.OrderIndex; i++)
        {
            if (_order[i].OrderId == order.OrderId)
                break;
        }
        if (i == DataSource.config.OrderIndex)
            throw new Exception("no order found");
        else
        {
           _order[i].OrderId = _order[i++].OrderId;
           DataSource.config.OrderIndex--;
        }
    }

    public void updateOrder(Order order)
    {
        int i;
        int x = 0;
        for (i = 0; i < DataSource.config.OrderIndex; i++)
        {
            if (_order[i].OrderId == order.OrderId)
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
}
