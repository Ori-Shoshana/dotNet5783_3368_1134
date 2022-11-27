using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Immutable;
using System.Globalization;
using static BO.Enums;


namespace BlImplementation;

internal class BoOrder : BlApi.IOrder
{
    private IDal dal = new Dal.DalList();

    public IEnumerable<OrderForList> GetOrders()
    {
        int i = 0;
        List<BO.OrderForList> orderForList = new List<BO.OrderForList>(); 
        List<DO.Order> DoOrders = new List<DO.Order>();
        List<DO.OrderItem> DoOrderItems = new List<DO.OrderItem>();

        DoOrders = (List<DO.Order>)dal.Order.GetAll();
        DoOrderItems = (List<DO.OrderItem>)dal.OrderItem.GetAll();
        BO.OrderForList orderForList1 = new BO.OrderForList();


        foreach (DO.Order DoOrder in DoOrders)
        {
            orderForList1.ID = DoOrders[i].OrderID;
            orderForList1.CustomerName = DoOrders[i].CustomerName;
            if (DoOrders[i].DeliveryDate <=DateTime.Now)
            {
                orderForList1.Status = OrderStatus.Deliverd;
            }
            else  if (DoOrders[i].ShipDate <= DateTime.Now)
            {
                orderForList1.Status = OrderStatus.Sent;
            }
            else
            {
                orderForList1.Status = OrderStatus.Confirmed;
            }
            i++;
        }
        foreach (DO.OrderItem orderItem in DoOrderItems)
        {
            orderForList1.TotalPrice += DoOrderItems[i].PriceItem * DoOrderItems[i].Amount;
            orderForList1.AmountOfItems = DoOrderItems[i].Amount;
            i++;
        }

        orderForList.Add(orderForList1);
        return orderForList;
    }

    public BO.Order OrderDetails(int id)
    {
        DO.Order DoOrder = new DO.Order();
        List<DO.OrderItem> DoOrderItem = new List<DO.OrderItem>();
        List<DO.Product> DoProduct = new List<DO.Product>();

        BO.Order? BoOrder = new BO.Order();
        if (id > 0)
        {
            int i = 0;
            double finalTotalPrice = 0;
            DoOrder = dal.Order.GetById(id);
            DoOrderItem = (List<DO.OrderItem>)dal.OrderItem.GetAll();
            DoProduct = (List<DO.Product>)dal.Product.GetAll();

            BoOrder.ID = DoOrder.OrderID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAdress = DoOrder.CustomerAdress;
            BoOrder.OrderDate = (DateTime)DoOrder.OrderDate;
            BoOrder.ShipDate = (DateTime)DoOrder.ShipDate;
            BoOrder.DeliveryDate = (DateTime)DoOrder.DeliveryDate;

            foreach (DO.OrderItem item in DoOrderItem)
            {
                if(id == DoOrderItem[i].OrderId)
                {
                    BO.OrderItem orderItems = new BO.OrderItem();
                    orderItems.ID = DoOrderItem[i].OrderId;
                    orderItems.Name = DoProduct[i].ProductName;
                    orderItems.ProductID = DoOrderItem[i].ProductID;
                    orderItems.Price = DoOrderItem[i].PriceItem;
                    orderItems.Amount = DoOrderItem[i].Amount;
                    orderItems.TotalPrice = DoOrderItem[i].PriceItem * DoOrderItem[i].Amount;
                    finalTotalPrice = DoOrderItem[i].PriceItem * DoOrderItem[i].Amount;
                    BoOrder.Items.Add(orderItems);
                }
                i++;
            }
            BoOrder.TotalPrice = finalTotalPrice;
            if (DoOrder.DeliveryDate <= DateTime.Now)
            {
                BoOrder.Status = OrderStatus.Deliverd;
            }
            else if (DoOrder.ShipDate <= DateTime.Now)
            {
                BoOrder.Status = OrderStatus.Sent;
            }
            else
            {
                BoOrder.Status = OrderStatus.Confirmed;
            }

            return BoOrder;
        }
        throw new VariableIsSmallerThanZeroExeption("id is smaller than 0");
    }

    public BO.Order Track(int id)
    {      
        int i = 0;

        List<DO.Order>? DoOrders = new List<DO.Order>();
        DoOrders = (List<DO.Order>)dal.Order.GetAll();
        BO.Order BoOrder = new BO.Order();

        foreach (DO.Order Doorder in DoOrders)
        {
            if (id == DoOrders[i].OrderID)
            {
                if (BoOrder.ShipDate == null) // לבדוק אם ככה הוא כשהוא לא מאותחל
                {
                    BoOrder.ShipDate = DateTime.Now;
                    BoOrder.Status = OrderStatus.Sent;
                    DO.Order order = new DO.Order();
                    order.OrderID = id;
                    order.CustomerName = BoOrder.CustomerName;
                    order.CustomerEmail = BoOrder.CustomerEmail;
                    order.CustomerAdress = BoOrder.CustomerAdress;
                    order.OrderDate = (DateTime)BoOrder.OrderDate;
                    order.ShipDate = (DateTime)BoOrder.ShipDate;
                    order.DeliveryDate = (DateTime)BoOrder.DeliveryDate;
                    dal.Order.Update(order);
                    return BoOrder;
                }
            }
            i++;
        }
    
           throw new BO.IdNotExistException("No Id in list");
    }

    public BO.Order UpdateDelivery(int id)
    {
        int i = 0;

        List<DO.Order>? DoOrders = new List<DO.Order>();
        DoOrders = (List<DO.Order>)dal.Order.GetAll();
        BO.Order BoOrder = new BO.Order();

        foreach (DO.Order Doorder in DoOrders)
        {
            if (id == DoOrders[i].OrderID)
            {
                if (BoOrder.DeliveryDate == null) // לבדוק אם ככה הוא כשהוא לא מאותחל
                {
                    BoOrder.DeliveryDate = DateTime.Now;
                    BoOrder.Status = OrderStatus.Deliverd;
                    DO.Order order = new DO.Order();
                    order.OrderID = id;
                    order.CustomerName = BoOrder.CustomerName;
                    order.CustomerEmail = BoOrder.CustomerEmail;
                    order.CustomerAdress = BoOrder.CustomerAdress;
                    order.OrderDate = (DateTime)BoOrder.OrderDate;
                    order.ShipDate = (DateTime)BoOrder.ShipDate;
                    order.DeliveryDate = (DateTime)BoOrder.DeliveryDate;
                    dal.Order.Update(order);
                    return BoOrder;
                }
            }
            i++;
        }

        throw new BO.IdNotExistException("No Id in list");
    }

    public BO.Order UpdateShip(int id)
    {
        throw new NotImplementedException();
    }
}
