
using System.Net.Http.Headers;

internal class BoOrder : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();    
    /// <summary>
                                                ///implemention of function get orders
                                                /// requests a list of orders from the data layer
                                                /// returns list of orders (from type order for list)
                                                /// </summary>
    public IEnumerable<BO.OrderForList?> GetOrders()
    {
        List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
        List<DO.Order?> DoOrders = new List<DO.Order?>();
        List<DO.OrderItem?> DoOrderItems = new List<DO.OrderItem?>();

        DoOrders = (List<DO.Order?>)dal.Order.GetAll();
        DoOrderItems = (List<DO.OrderItem?>)dal.OrderItem.GetAll();
        int i = 0;


     orderForList = DoOrders.Select((DoOrder, index) =>
        {
            BO.OrderForList orderForList1 = new BO.OrderForList();
            orderForList1.ID = (int)DoOrder?.OrderID!;
            orderForList1.CustomerName = DoOrder?.CustomerName;

            if (DoOrder?.DeliveryDate <= DateTime.Now)
            {
                orderForList1.Status = BO.Enums.OrderStatus.Deliverd;
            }
            else if (DoOrder?.ShipDate <= DateTime.Now)
            {
                orderForList1.Status = BO.Enums.OrderStatus.Sent;
            }
            else
            {
                orderForList1.Status = BO.Enums.OrderStatus.Confirmed;
            }

            orderForList1.TotalPrice += (DoOrderItems[index]?.PriceItem ?? 0) * (DoOrderItems[index]?.Amount ?? 0);
            orderForList1.AmountOfItems = DoOrderItems[index]?.Amount ?? 0;

            return orderForList1;
        })
        .ToList();

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
        List<DO.Order?> DoOrders = new List<DO.Order?>();
        DO.Order DoOrder = new DO.Order();
        List<DO.OrderItem?> DoOrderItem = new List<DO.OrderItem?>();
        List<DO.Product?> DoProducts = new List<DO.Product?>();
        List<BO.OrderItem> boOrderItems = new List<BO.OrderItem>();
        BO.Order? BoOrder = new BO.Order();
        if (id > 0)
        {
            double finalTotalPrice = 0;
            DoOrder = dal.Order.GetById(id);
            DoOrderItem = (List<DO.OrderItem?>)dal.OrderItem.GetAll();
            DoProducts = (List<DO.Product?>)dal.Product.GetAll();
            DoOrders = (List<DO.Order?>)dal.Order.GetAll();

            BoOrder.ID = DoOrder.OrderID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAdress = DoOrder.CustomerAdress;
            if (DoOrder.OrderDate != null)
                BoOrder.OrderDate = (DateTime)DoOrder.OrderDate;
            if (DoOrder.ShipDate != null)
                BoOrder.ShipDate = (DateTime)DoOrder.ShipDate;
            if (DoOrder.DeliveryDate != null)
                BoOrder.DeliveryDate = (DateTime)DoOrder.DeliveryDate;

            boOrderItems = DoOrderItem
                    .Where(item => id == item?.OrderId)
                    .Select(item => new BO.OrderItem
                    {
                        ID = (int)item?.OrderId!,
                        ProductID = (int)item?.ProductID!,
                        Price = (int)item?.PriceItem!,
                        Amount = (int)item?.Amount!,
                        TotalPrice = (int)item?.PriceItem! * (int)item?.Amount!,
                        Name = DoOrders.FirstOrDefault(order => id == order?.OrderID)?.CustomerName
                    }).ToList();

            finalTotalPrice = boOrderItems.Sum(item => item.TotalPrice);



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
        List<DO.Order?> DoOrders = new List<DO.Order?>();
        DoOrders = (List<DO.Order?>)dal.Order.GetAll();
        BO.Order BoOrder = new BO.Order();

        DO.Order? order1 = (DO.Order?)DoOrders.FirstOrDefault(doOrder => id == doOrder?.OrderID);
        if (order1 != null)
        {
            if (BoOrder.DeliveryDate == null) 
            {
                BoOrder.DeliveryDate = DateTime.Now;
                BoOrder.Status = BO.Enums.OrderStatus.Deliverd;
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
        List<DO.Order?> DoOrders = new List<DO.Order?>();
        DoOrders = (List<DO.Order?>)dal.Order.GetAll();
        BO.Order BoOrder = new BO.Order();

        DO.Order? order1 = (DO.Order?)DoOrders.FirstOrDefault(doOrder => id == doOrder?.OrderID);
        if (order1 != null)
        {
            if (BoOrder.ShipDate == null)
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
        List<DO.Order?> DoOrders = new List<DO.Order?>();
        DoOrders = (List<DO.Order?>)dal.Order.GetAll();
        BO.OrderTracking BoOrderTracking = new BO.OrderTracking();
        bool check = false;

        var doOrder = DoOrders.FirstOrDefault(order => id == order?.OrderID);
        if (doOrder != null)
        {
            check = true;
            BoOrderTracking.ID = (int)doOrder?.OrderID!;
            if (doOrder?.DeliveryDate <= DateTime.Now)
            {
                BoOrderTracking.Status = BO.Enums.OrderStatus.Deliverd;
                Item = "the package was deliverd";
            }
            else if (doOrder?.ShipDate <= DateTime.Now)
            {
                BoOrderTracking.Status = BO.Enums.OrderStatus.Sent;
                Item = "the package was sent";
            }
            else
            {
                BoOrderTracking.Status = BO.Enums.OrderStatus.Confirmed;
                Item = "the order is confermed in the system";
            }
            var tupleList = new List<(DateTime? myTime, string? Name)>
            {
                 (DateTime.Now, Item)
            };
            BoOrderTracking.Tracking = tupleList;
        }

        if (check == false)
        {
            throw new BO.VariableIsNullExeption("There is no item to track");
        }
        return BoOrderTracking;

    }
    public void updateOrederM(int amount, int orderId, int prodId)
    {

        BO.Order ord = OrderDetails(orderId);
        List<DO.Product> products = new List<DO.Product>();
        products = (List<DO.Product>)dal.Product.GetAll();
        DO.Product product = new DO.Product();
        BO.OrderItem order_item = new BO.OrderItem();
        List<BO.OrderItem> order_items = new List<BO.OrderItem>();
        bool flag = false;
        if (ord.Items == null)
        {
            throw new BO.VeriableNotExistException("can't find the list of items");
        }

            var updatedOrderItems = ord.Items
                    .Where(i => i.ProductID == prodId)
                    .Select(i => {
            if (i.Amount + amount < 0)
            {
                throw new BO.VariableIsSmallerThanZeroExeption("The quantity is less than the quantity to be reduced");
            }

            var matchingProduct = products
                    .Where(p => p.ProductID == prodId)
                    .FirstOrDefault();

            if (matchingProduct.ProductID != null)
            {
                if (amount > matchingProduct.InStock)
                {
                    throw new BO.VeriableNotExistException("not existing enough items in stock");
                }

                product.ProductID = matchingProduct.ProductID;
                product.ProductName = matchingProduct.ProductName;
                product.Price = matchingProduct.Price;
                product.InStock -= amount;
                product.Category = matchingProduct.Category;
                dal.Product.Update(product);
                flag = true;
            }

            if (flag == false)
            {
                throw new BO.VeriableNotExistException("product id not exists");
            }

            order_item.Price = i.Price;
            order_item.ProductID = i.ProductID;
            order_item.Amount = i.Amount + amount;
            order_item.ID = i.ID;
            order_item.Name = i.Name;
            order_item.TotalPrice = i.TotalPrice + amount * order_item.Price;
            ord.TotalPrice += amount * order_item.Price;
            return order_item;
        });

        order_items = updatedOrderItems?.Concat(ord?.Items.Where(i => i.ProductID != prodId)).ToList();
        ord.Items = order_items!;
    }
}
