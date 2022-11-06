using DO;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;

public class DalOrderItem
{
    public int addOrderItem(OrderItem ordItem)
    {
        ordItem.PrivateId = DataSource.config.OrderItemIndex;
        DataSource._orderItem[DataSource.config.OrderItemIndex++] = ordItem;
        return ordItem.PrivateId;
    }

    public void deleteOrderItem(OrderItem orderItem)
    {
        int i;
        for (i = 0; i < DataSource.config.OrderItemIndex; i++)
        {
            if (_orderItem[i].PrivateId == orderItem.PrivateId)
                break;
        }
        if (i == DataSource.config.OrderItemIndex)
            throw new Exception("no order item found");
        else
        {
            _orderItem[i].PrivateId = _orderItem[i++].PrivateId;
            DataSource.config.OrderItemIndex--;
        }
    }

    public void updateOrderItem(OrderItem orderItem)
    {
        
        int i;
        int x = 0;
        for (i = 0; i < DataSource.config.OrderIndex; i++)
        {
            if (_orderItem[i].PrivateId == orderItem.PrivateId)
            {
                _orderItem[i] = orderItem;
                x = 1;
                break;
            }
        }
        if (x == 0)
        {
            throw new Exception("no order item found");
        }
    }


}