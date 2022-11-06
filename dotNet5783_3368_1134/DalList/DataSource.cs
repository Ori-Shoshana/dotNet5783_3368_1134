using DO;
using System.ComponentModel;

namespace Dal;

internal static class DataSource
{

    public static readonly Random Rnd = new Random();
    internal static Order[] _order = new Order[100];
    internal static OrderItem[] _orderItem = new OrderItem[200];
    internal static Product[] _product = new Product[50];

    private static void addOrder(int privateId, int orderId, int prodoctId, int price, int amount)
    {
        Order newOrder = new Order();
        newOrder.PrivateId = privateId;
        newOrder.OrderId = orderId;
        newOrder.ProdoctId = prodoctId;
        newOrder.Price = price;
        newOrder.Amount = amount;
    }
    private static void addDalProduct(int privateId, string prodoctName, CategoryAttribute category, double price, int inStock)
    {
        Product newProdoct = new Product();
        newProdoct.PrivateId = privateId;
        newProdoct.ProdoctName = prodoctName;
        newProdoct.Category = category;
        newProdoct.Price = price;
        newProdoct.InStock = inStock;
    }
    private static void addOrderItem(int privateId, string customerName, string customerEmail, string customerAdrss, DateTime orderDate, DateTime shipDate, DateTime delivoryDate)
    {
        OrderItem newOrderItem = new OrderItem();
        newOrderItem.PrivateId = privateId;
        newOrderItem.CustomerName = customerName;
        newOrderItem.CustomerEmail = customerEmail;
        newOrderItem.CustomerAdrss = customerAdrss;
        newOrderItem.OrderDate = orderDate;
        newOrderItem.ShipDate = shipDate;
        newOrderItem.DelivoryDate = delivoryDate;
    }
    internal static class config
    {
        internal static int OrderIndex = 0;
        internal static int OrderItemIndex = 0;
        internal static int ProductIndex = 0;
        internal static int PrivateId = 0;
    }
    private static void s_Initialize()
    {
    }
}

