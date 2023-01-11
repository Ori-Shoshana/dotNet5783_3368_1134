
using BO;
using System.Net.Http.Headers;
using static BO.Enums;

internal class BoOrder : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();    
    /// <summary>
    ///implemention of function get orders
    /// requests a list of orders from the data layer
    /// returns list of orders (from type order for list)
    /// </summary>
    public IEnumerable<BO.OrderForList?> GetOrders(Func<DO.Order?, bool>? func)
    {
        List<DO.Order?> DoOrders;
        List<DO.OrderItem?> DoOrderItems;

        DoOrders = dal.Order.GetAll().ToList();
        DoOrderItems = (List<DO.OrderItem?>)dal.OrderItem.GetAll();

        Random rnd = new Random();
        if (func != null)
        {

            var ordersForList = DoOrders.Where(Order => func(Order)).Select(doOrder => new BO.OrderForList
            {
                ID = (int)(doOrder?.OrderID)!,
                CustomerName = doOrder?.CustomerName,
                Status = doOrder?.DeliveryDate != null ? OrderStatus.Deliverd : (doOrder?.ShipDate != null ? OrderStatus.Sent : OrderStatus.Confirmed),
                TotalPrice = (double)DoOrderItems.Where(item => item?.OrderId == doOrder?.OrderID).Sum(item => item?.PriceItem * (int)item?.Amount!)!,
                AmountOfItems = DoOrderItems.Where(item => item?.OrderId == doOrder?.OrderID).Sum(item => (int)item?.Amount!)
            }).ToList();
            return ordersForList!;
        }
        else
        {
            var ordersForList = DoOrders.Select(doOrder => new BO.OrderForList
            {
                ID = (int)(doOrder?.OrderID)!,
                CustomerName = doOrder?.CustomerName,
                Status = doOrder?.DeliveryDate != null ? OrderStatus.Deliverd : (doOrder?.ShipDate != null ? OrderStatus.Sent : OrderStatus.Confirmed),
                TotalPrice = (double)DoOrderItems.Where(item => item?.OrderId == doOrder?.OrderID).Sum(item => item?.PriceItem * (int)item?.Amount!)!,
                AmountOfItems = DoOrderItems.Where(item => item?.OrderId == doOrder?.OrderID).Sum(item => (int)item?.Amount!)
            }).ToList();
            return ordersForList!;

        }
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
        List<DO.Order?> DoOrders;
        DO.Order DoOrder = new DO.Order();
        List<DO.OrderItem?> DoOrderItem;
        List<DO.Product?> DoProducts;
        List<BO.OrderItem> boOrderItems;
        BO.Order? BoOrder = new BO.Order();
        if (id > 0)
        {
            double finalTotalPrice = 0;
            DoOrder = dal.Order.GetById(id);
            DoOrderItem = dal.OrderItem.GetAll().ToList();
            DoProducts = dal.Product.GetAll().ToList();
            DoOrders = dal.Order.GetAll().ToList();

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
                        Name = DoProducts.FirstOrDefault(prod => prod?.ProductID == item?.ProductID)?.ProductName
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
                BoOrder.DeliveryDate = null;
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

        List<BO.OrderItem?> boOrderItems;
        List<DO.Product?> DoProducts;
        DoProducts = dal.Product.GetAll().ToList();
        double? finalTotalPrice = 0;
        List<DO.OrderItem?> orderItems;
        orderItems = dal.OrderItem.GetAll().ToList();
        List<DO.Order?> DoOrders;
        DoOrders = dal.Order.GetAll().ToList();
        BO.Order BoOrder = new BO.Order();
        DO.Order temp = new DO.Order();
        bool? check = false;




                    var boOrder = DoOrders.Where(x => x?.OrderID == id)
            .Select(x => new BO.Order
            {
                DeliveryDate = x?.DeliveryDate ?? DateTime.Now,
                ID = (int)(x?.OrderID)!,
                CustomerName = x?.CustomerName,
                CustomerAdress = x?.CustomerAdress,
                CustomerEmail = x?.CustomerEmail,
                OrderDate = x?.OrderDate,
                ShipDate = x?.ShipDate,
                Status = OrderStatus.Sent,
                TotalPrice = (double)orderItems.Where(y => y?.OrderId == id)
            .Select(y => y?.PriceItem * y?.Amount)
            .Sum()!,
                Items = orderItems.Where(y => y?.OrderId == id)
            .Select(y => new BO.OrderItem
            {
                ID = (int)(y?.OrderId)!,
                ProductID = (int)y?.ProductID!,
                Price = (double)y?.PriceItem!,
                Amount = (int)y?.Amount!,
                TotalPrice = (double)y?.PriceItem! * (double)y?.Amount!,
                Name = DoProducts.Where(z => z?.ProductID == y?.ProductID)
            .Select(z => z?.ProductName)
            .FirstOrDefault()
            }).ToList()!
            }).FirstOrDefault();

            if (boOrder != null)
            {
                dal.Order.Update(new DO.Order
                {
                    DeliveryDate = boOrder.DeliveryDate,
                    OrderID = boOrder.ID,
                    CustomerName = boOrder.CustomerName,
                    CustomerAdress = boOrder.CustomerAdress,
                    CustomerEmail = boOrder.CustomerEmail,
                    OrderDate = boOrder.OrderDate,
                    ShipDate = boOrder.ShipDate,
                    //Status = boOrder.Status
                });
                return boOrder;
            }

            throw new BO.VeriableNotExistException("No Id in list");
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
        List<BO.OrderItem?> boOrderItems = new List<BO.OrderItem?>();
        List<DO.Product?> DoProducts;
        DoProducts = dal.Product.GetAll().ToList();
        double finalTotalPrice = 0;
        List<DO.OrderItem?> orderItems = new List<DO.OrderItem?>();
        orderItems = (List<DO.OrderItem?>)dal.OrderItem.GetAll();

        List<DO.Order?> DoOrders;
        DoOrders = dal.Order.GetAll().ToList();

        BO.Order BoOrder = new BO.Order();
        DO.Order temp = new DO.Order();
        bool check = false;
        var doOrder = DoOrders.Where(o => o?.OrderID == id).FirstOrDefault();
        if (doOrder != null)
        {
            check = true;
            if (doOrder?.ShipDate == null)
                BoOrder.ShipDate = temp.ShipDate = DateTime.Now;
            else
                BoOrder.ShipDate = temp.ShipDate = doOrder?.ShipDate;
            BoOrder.ID = temp.OrderID = (int)(doOrder?.OrderID)!;
            BoOrder.CustomerName = temp.CustomerName = doOrder?.CustomerName;
            BoOrder.CustomerAdress = temp.CustomerAdress = doOrder?.CustomerAdress;
            BoOrder.CustomerEmail = temp.CustomerEmail = doOrder?.CustomerEmail;
            BoOrder.OrderDate = temp.OrderDate = doOrder?.OrderDate;

            if (doOrder?.DeliveryDate != null)
                BoOrder.DeliveryDate = temp.DeliveryDate = doOrder?.DeliveryDate;
            BoOrder.Status = OrderStatus.Sent;

            var orderItemsForOrder = orderItems.Where(i => i?.OrderId == id);
            foreach (var item in orderItemsForOrder)
            {
                BO.OrderItem boOrderItem = new BO.OrderItem();
                boOrderItem.ID = (int)(item?.OrderId)!;
                boOrderItem.ProductID = (int)(item?.ProductID)!;
                boOrderItem.Price = (double)(item?.PriceItem)!;
                boOrderItem.Amount = (int)(item?.Amount)!;
                boOrderItem.TotalPrice = (double)(item?.PriceItem * item?.Amount)!;
                finalTotalPrice += (double)item?.PriceItem! * (int)item?.Amount!;
                var product = DoProducts.Where(p => p?.ProductID == boOrderItem.ProductID).FirstOrDefault();
                if (product != null)
                    boOrderItem.Name = product?.ProductName;
                boOrderItems.Add(boOrderItem);
            }
        }
        if (check)
        {
            BoOrder.TotalPrice = finalTotalPrice;
            BoOrder.Items = boOrderItems;
            dal.Order.Update(temp);
            BoOrder.TotalPrice = finalTotalPrice;
            return BoOrder;
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
        List<DO.Order?> DoOrders;
        DoOrders = dal.Order.GetAll().ToList();
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

            if (matchingProduct.ProductName != null)
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

        order_items = updatedOrderItems?.Concat(ord?.Items.Where(i => i.ProductID != prodId)!).ToList()!;
        ord.Items = order_items!;
    }
}
