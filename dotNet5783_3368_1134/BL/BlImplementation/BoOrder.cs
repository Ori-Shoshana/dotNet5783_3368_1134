
internal class BoOrder : BlApi.IOrder
{
    private DalApi.IDal dal = new Dal.DalList();
    /// <summary>
    ///implemention of function get orders
    /// requests a list of orders from the data layer
    /// returns list of orders (from type order for list)
    /// </summary>
    public IEnumerable<BO.OrderForList> GetOrders()
    {
        List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
        List<DO.Order> DoOrders = new List<DO.Order>();
        List<DO.OrderItem> DoOrderItems = new List<DO.OrderItem>();

        DoOrders = (List<DO.Order>)dal.Order.GetAll();
        DoOrderItems = (List<DO.OrderItem>)dal.OrderItem.GetAll();
        int i = 0;

        foreach (DO.Order DoOrder in DoOrders)
        {
            BO.OrderForList orderForList1 = new BO.OrderForList();

            orderForList1.ID = DoOrder.OrderID;
            orderForList1.CustomerName = DoOrder.CustomerName;
            if (DoOrder.DeliveryDate <=DateTime.Now)
            {
                orderForList1.Status = BO.Enums.OrderStatus.Deliverd;
            }
            else  if (DoOrder.ShipDate <= DateTime.Now)
            {
                orderForList1.Status = BO.Enums.OrderStatus.Sent;
            }
            else
            { orderForList1.Status = BO.Enums.OrderStatus.Confirmed; }
            
            orderForList1.TotalPrice += DoOrderItems[i].PriceItem * DoOrderItems[i].Amount;
            orderForList1.AmountOfItems = DoOrderItems[i].Amount;
            
            orderForList.Add(orderForList1);
            i++;
        }
        return orderForList;
    }
    /// <summary>
    /// implemention of function order details
    /// receives id 
    /// checks if the order exists in the data layer
    /// the function receives order id , checking integrity
    /// Throws exceptions when needed
    /// returns order
    public BO.Order OrderDetails(int id)
    {
        List<DO.Order> DoOrders = new List<DO.Order>();
        DO.Order DoOrder = new DO.Order();
        List<DO.OrderItem> DoOrderItem = new List<DO.OrderItem>();
        List<DO.Product> DoProducts = new List<DO.Product>();
        List<BO.OrderItem> boOrderItems = new List<BO.OrderItem>();
        BO.Order? BoOrder = new BO.Order();
        if (id > 0)
        {
            int i = 0;
            double finalTotalPrice = 0;
            DoOrder = dal.Order.GetById(id);
            DoOrderItem = (List<DO.OrderItem>)dal.OrderItem.GetAll();
            DoProducts = (List<DO.Product>)dal.Product.GetAll();
            DoOrders = (List<DO.Order>)dal.Order.GetAll();

            BoOrder.ID = DoOrder.OrderID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAdress = DoOrder.CustomerAdress;
            BoOrder.OrderDate = (DateTime)DoOrder.OrderDate;
            if (DoOrder.ShipDate != null)
                BoOrder.ShipDate = (DateTime)DoOrder.ShipDate;
            if (DoOrder.DeliveryDate != null)
                BoOrder.DeliveryDate = (DateTime)DoOrder.DeliveryDate;

            foreach (DO.OrderItem item in DoOrderItem)
            {
                if (id == item.OrderId)
                {
                    BO.OrderItem BoOrderItem = new BO.OrderItem();
                    BoOrderItem.ID = item.OrderId;
                    BoOrderItem.ProductID = item.ProductID;
                    BoOrderItem.Price = item.PriceItem;
                    BoOrderItem.Amount = item.Amount;
                    BoOrderItem.TotalPrice = item.PriceItem * item.Amount;
                    finalTotalPrice += item.PriceItem * item.Amount;
                    foreach (var order in DoOrders)
                    {
                        if (id == order.OrderID)
                        {
                            BoOrderItem.Name = order.CustomerName;
                            break;
                        }
                    }

                    boOrderItems.Add(BoOrderItem);
                }

            }
            
            BoOrder.Items = boOrderItems;
            BoOrder.TotalPrice = finalTotalPrice;
            if (DoOrder.DeliveryDate <= DateTime.Now)
            {
                BoOrder.Status = BO.Enums.OrderStatus.Deliverd;
            }
            else if (DoOrder.ShipDate <= DateTime.Now)
            {
                BoOrder.Status = BO.Enums.OrderStatus.Sent;
            }
            else
            {
                BoOrder.Status = BO.Enums.OrderStatus.Confirmed;
            }
            return BoOrder;
        }
        throw new BO.VariableIsSmallerThanZeroExeption("id is smaller than 0");
    }
    /// <summary>
    /// implemention of function update delivery
    /// checks if the order exists in the data layer and updates ship date in the order
    /// the function receives order id , checking integrity , placing an order
    /// trying to update the data layer
    /// Throws exceptions when needed
    /// returns the order (after update)
    public BO.Order UpdateDelivery(int id)
    {
        List<DO.Order>? DoOrders = new List<DO.Order>();
        DoOrders = (List<DO.Order>)dal.Order.GetAll();
        BO.Order BoOrder = new BO.Order();

        foreach (DO.Order doOrder in DoOrders)
        {
            if (id == doOrder.OrderID)
            {
                if (BoOrder.DeliveryDate == null) // לבדוק אם ככה הוא כשהוא לא מאותחל
                {
                    BoOrder.DeliveryDate = DateTime.Now;
                    BoOrder.Status = BO.Enums.OrderStatus.Deliverd;
                    DO.Order order = new DO.Order();
                    order.OrderID = id;
                    order.CustomerName = BoOrder.CustomerName;
                    order.CustomerEmail = BoOrder.CustomerEmail;
                    order.CustomerAdress = BoOrder.CustomerAdress;
                    if(BoOrder.OrderDate != null)
                    {
                        order.OrderDate = (DateTime)BoOrder.OrderDate;
                    }
                    if(BoOrder.ShipDate != null)
                    {
                        order.ShipDate = (DateTime)BoOrder.ShipDate;
                    }
                    if(BoOrder.DeliveryDate != null)
                    {
                        order.DeliveryDate = (DateTime)BoOrder.DeliveryDate;
                    }
                    dal.Order.Update(order);
                    return BoOrder;
                }
            }
        }

        throw new BO.VeriableNotExistException("No Id in list");
    }
    /// <summary>
    /// implemention of function shipping update
    /// checks if the order exists in the data layer and updates ship date
    /// the function receives order id , checking integrity , placing an order
    /// Throws exceptions when needed
    /// trying tu update the data layer
    /// returns the order (after update)
    public BO.Order ShippingUpdate(int id)
    {
        List<DO.Order>? DoOrders = new List<DO.Order>();
        DoOrders = (List<DO.Order>)dal.Order.GetAll();
        BO.Order BoOrder = new BO.Order();

        foreach (DO.Order doOrder in DoOrders)
        {
            if (id == doOrder.OrderID)
            {
                if (BoOrder.ShipDate == null) // לבדוק אם ככה הוא כשהוא לא מאותחל
                {
                    BoOrder.ShipDate = DateTime.Now;
                    BoOrder.Status = BO.Enums.OrderStatus.Sent;
                    DO.Order order = new DO.Order();
                    order.OrderID = id;
                    order.CustomerName = BoOrder.CustomerName;
                    order.CustomerEmail = BoOrder.CustomerEmail;
                    order.CustomerAdress = BoOrder.CustomerAdress;
                    if (BoOrder.OrderDate != null)
                    {
                        order.OrderDate = (DateTime)BoOrder.OrderDate;
                    }
                    if (BoOrder.ShipDate != null)
                    {
                        order.ShipDate = (DateTime)BoOrder.ShipDate;
                    }
                    if (BoOrder.DeliveryDate != null)
                    {
                        order.DeliveryDate = (DateTime)BoOrder.DeliveryDate;
                    }
                    dal.Order.Update(order);
                    return BoOrder;
                }
            }
        }

        throw new BO.VeriableNotExistException("No Id in list");
    }
    /// <summary>
    /// implemention of function track
    /// receives order id
    /// checks if the order exists in the data layer 
    /// Throws exceptions when needed
    /// returns OrderTracking
    public BO.OrderTracking Track(int id)
    {
        string Item;
        List<DO.Order> DoOrders = new List<DO.Order>();
        DoOrders = (List<DO.Order>)dal.Order.GetAll();
        BO.OrderTracking BoOrderTracking = new BO.OrderTracking();
        bool check = false;
        foreach (DO.Order doOrder in DoOrders)
        {
            if (id == doOrder.OrderID)
            {
                check = true;
                BoOrderTracking.ID = (int)doOrder.OrderID;
                if (doOrder.DeliveryDate <= DateTime.Now)
                {
                    BoOrderTracking.Status = BO.Enums.OrderStatus.Deliverd;
                    Item = "the package was deliverd";
                }
                else if (doOrder.ShipDate <= DateTime.Now)
                {
                    BoOrderTracking.Status = BO.Enums.OrderStatus.Sent;
                    Item = "the package was sent";
                }
                else
                {
                    BoOrderTracking.Status = BO.Enums.OrderStatus.Confirmed;
                    Item = "the order is confermed in the system";
                }
                var tupleList = new List<(DateTime myTime, string Name)>
                {
                     (DateTime.Now, Item)
                };
                BoOrderTracking.Tracking = tupleList;
             }
        }
        if(check == false)
        {
            throw new BO.VariableIsNullExeption("There is no item to track");
        }
        return BoOrderTracking;

    }
}
