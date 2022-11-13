﻿using DO;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;
using static Dal.DataSource;

public class DalOrderItem
{
    public static int AddOrderItem(OrderItem ordItem)
    {
        ordItem.PrivateId = DataSource.config.OrderItemIndex;
        DataSource._orderItem[DataSource.config.OrderItemIndex++] = ordItem;
        return ordItem.PrivateId;
    }

    public static void DeleteOrderItem(int ID)
    {
        int i;
        for (i = 0; i < DataSource.config.OrderItemIndex; i++)
        {
            if (_orderItem[i].PrivateId == ID)
                break;
        }
        if (i == DataSource.config.OrderItemIndex)
            throw new Exception("no order item found");
        else
        {
            for (; i < DataSource.config.OrderItemIndex; i++)
            {
                _orderItem[i].PrivateId = _orderItem[i++].PrivateId;
            }
            DataSource.config.OrderItemIndex--;
        }
    }

    public static void UpdateOrderItem(OrderItem orderItem)
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
    public static OrderItem Get(int orderItemId)
    {
        for (int i = 0; i < DataSource.config.OrderItemIndex; i++)
        {
            if (_orderItem[i].PrivateId == orderItemId)
            {
                return _orderItem[i];
            }
        }
        throw new Exception("no order item found");
    }

    public static OrderItem[] GetOrderItemArray()
    {
        OrderItem[] orderItemArrey = new OrderItem[200];
        for(int i=0; i < DataSource.config.OrderItemIndex; i++)
        {
            orderItemArrey[i].PrivateId = _orderItem[i].PrivateId;
            orderItemArrey[i].OrderId = _orderItem[i].OrderId;
            orderItemArrey[i].ProductId = _orderItem[i].ProductId;
            orderItemArrey[i].PriceItem = _orderItem[i].PriceItem;
            orderItemArrey[i].Amount = _orderItem[i].Amount;    
        }
        return orderItemArrey;
    }

}