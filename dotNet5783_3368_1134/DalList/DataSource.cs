using DalApi;
using DO;
using static DO.Enums;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using static Dal.DataSource;

namespace Dal;

/// <summary>
/// class data source (mainly initialize properties with random values)
/// </summary>
internal static class DataSource
{
    static DataSource()
    {
        S_Initialize();
    }

    public static readonly Random Rnd = new Random();
    internal static List<Order?> listOrder = new List<Order?>();
    internal static List<OrderItem?> ListOrderItem = new List<OrderItem?>();
    internal static List<Product?> ListProduct = new List<Product?>();

    internal static List<Order?> ListOrder { get => listOrder; set => listOrder = value; }

    internal static class config
    {
        private static int orderNumber = 10000;
        private static int orderitemNumber = 10000;
        internal static int order_Number { get => ++orderNumber; }
        internal static int runOrderitem_Number { get => ++orderitemNumber; }
    }
    internal static void S_Initialize()
    {
        S_product();
        S_order();
        S_orderItem();
    }

    /// <summary>
    /// initializing product
    /// contains: Names of categories + names of products for sale + different prices
    /// </summary>
    internal static void S_product()
    {
        string[] Name =
        {
            "Iphone14","GalaxyS22",
            "Lenovo PC" ,"Dell PC",
            "Mouse Logitech", "Mouse Razer",
            "Keyboard Logiteck","Keyboard Razer",
            "airpods pro2", "galaxy buds2"
        };
        int[] ProductPrice = new int[10] 
        { 4000, 3900, 3000, 4000, 200, 150, 250, 270, 850, 50 };
        for (int i = 0; i < 10; i++)
        {
            Product p = new Product();
            p.ProductID = Rnd.Next(100000, 999999);
            p.ProductName = Name[i];
            p.Price = ProductPrice[i];
            if (i < 2) { p.Category = productCategory.Phone; }
            if (i < 4 && i >= 2) { p.Category = productCategory.Laptop; }
            if (i < 6 && i >= 4) { p.Category = productCategory.Mouse; }
            if (i < 8 && i >= 6) { p.Category = productCategory.Keybord; }
            if (i < 10 && i >= 8) { p.Category = productCategory.Hadphones; }
            p.InStock = Rnd.Next(0, 10); 
         
            ListProduct.Add(p);
        }
    }

    /// <summary>
    /// initializing prder
    /// contains: Email addresses + street names + customer names
    /// </summary>
    internal static void S_order()
    {
        string[] customer_Email =
        {
        "avi22@gmail.com", "yossi349@gmail.com", "avraam563@gmail.com", "shira3492@gmail.com", "shalom349@gmail.com",
        "michal349@gmail.com", "sapir459@gmail.com", "yaakov483@gmail.com", "david548@gmail.com", "netanel3834@gmail.com",
        "shani584@gmail.com", "nati4939@gmail.com", "tova349@gmail.com", "noa3885@gmail.com", "orel684@gmail.com",
         "yair688@gmail.com", "sara684@gmail.com", "daniel584@gmail.com", "dan584@gmail.com", "shilat584@gmail.com"
        };
        string[] customer_Adress =
        {
        "Orchard Court","Heath Close","Moorland Road","Second Avenue",
        "Oakfield Road","West Close","Talbot Road","Commercial Road",
        "Kingsley Road","Brunswick Street","Shakespeare Road","York Street",
        "Hardy Close","Derwent Avenue","Walton Road","The Willows",
        "Highfield","Stanley Close","Coppice Close","Nursery Lane"
        };
        string[] customer_Name =
        {
        "Bethany Dalton", "Alaina Ingram", "Hope Hawkins","Charlize Cline",
        "Deborah Sullivan", "Jacoby Hartman", "Melody Keith", "Gilberto Neal",
        "Mattie Valencia", "Joyce Wolf", "Esteban Byrd", "Amelia Salinas",
        "Shirley Stafford", "Catalina Riggs", "Lilah Stone", "Tyree Merritt",
        "Kaelyn Allison", "Jacey Johns", "Lillianna Curtis","Kirsten Howe",
        "Kendall Dodson",
        };
        int i;
        Order O = new Order();
        for (i=0; i<12; i++)
        {
            O.OrderID = config.order_Number;
            O.CustomerName = customer_Name[i];
            O.CustomerEmail = customer_Email[i];
            O.CustomerAdress = customer_Adress[i];
            O.OrderDate = DateTime.Now.AddDays(-5).AddHours(-4);
            O.ShipDate = DateTime.Now;///////80%
            O.DeliveryDate = DateTime.Now;//////60%
            ListOrder?.Add(O);
        }
        for(;i<16;i++)
        {
            O.OrderID = config.order_Number;
            O.CustomerName = customer_Name[i];
            O.CustomerEmail = customer_Email[i];
            O.CustomerAdress = customer_Adress[i];
            O.OrderDate = DateTime.Now.AddDays(-5).AddHours(-4);
            O.ShipDate = DateTime.Now + new TimeSpan(Rnd.Next(0, 2), Rnd.Next(0, 59), Rnd.Next(0, 59)); //80%
            //O.DeliveryDate = null;
            ListOrder?.Add(O);
        }
        for (;i<20;i++)
        {
            O.OrderID = config.order_Number;
            O.CustomerName = customer_Name[i];
            O.CustomerEmail = customer_Email[i];
            O.CustomerAdress = customer_Adress[i];
            O.OrderDate = DateTime.Now.AddDays(-5).AddHours(-4);
            O.ShipDate = DateTime.Now;//20%
            //O.DeliveryDate = DateTime.Now;//40%
            ListOrder?.Add(O);
        }
    }

    /// <summary>
    /// initializing order item
    /// </summary>
    internal static void S_orderItem()
    {
        OrderItem OI = new OrderItem();
        foreach (Order orders in ListOrder)
        {
            foreach (Product products in ListProduct)
            {
                for (int i = 0; i < 4; i++)
                {
                    OI.OrderItemID = config.runOrderitem_Number;
                    OI.OrderId = orders.OrderID;
                    OI.ProductID = products.ProductID;
                    OI.PriceItem = products.Price;
                    OI.Amount = Rnd.Next(1, 5);
                    ListOrderItem.Add(OI);
                }
                break;
            }
        }
    }
}

