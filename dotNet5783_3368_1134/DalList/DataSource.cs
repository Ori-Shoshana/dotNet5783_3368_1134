﻿using DalApi;
using DO;
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
    public static readonly Random Rnd = new Random();
    internal static List<Order> _order = new List<Order>();
    internal static List<OrderItem> _orderItem = new List<OrderItem>();
    internal static List<Product> _product = new List<Product>();

    internal static class config
    {
        private static int orderNumber = 10000;
        private static int orderitemNumber = 10000;
        internal static int order_Number { get => ++orderNumber; }
        internal static int runOrderitem_Number { get => ++orderitemNumber; }

        internal static int OrderIndex = 0;
        internal static int OrderItemIndex = 0;
        internal static int ProductIndex = 0;
        internal static int MyPrivateId = 0;
    }
    internal static void S_Initialize()
    {
        S_order();
        S_product();
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
            "Iphone12", "Iphone13", "GalaxyS20", "GalaxyS21",
            "GalaxyS22", "Lenovo Pc" ,"Dell PC",
            "Mouse Logiteck", "Keyboard Logiteck", "Headphones5s"
        };
        string[] My_Category =
        {
            "Phone", "PC", "Mouse", "Keybord", "Hadphones" 
        };
        int[] ProductPrice = new int[10]
        {4000, 3500, 3500, 4000, 50000, 6000, 4000, 70, 100, 350};
        for(int i=0; i < 10; i++)
        {
            Product p = new Product();
            p.PrivateId = Rnd.Next(100000, 999999);
            p.ProdoctName = Name[i];
            p.Category = My_Category[Rnd.Next(0,4)];
            p.Price = ProductPrice[i];
            if(i < 10)
            {
                p.InStock = Rnd.Next(3, 50);
            }
            else
            {
                p.InStock = 0;
            }
            _product.Add(p);
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

            O.PrivateId = config.order_Number;
            O.CustomerName = customer_Name[i];
            O.CustomerEmail = customer_Email[i];
            O.CustomerAdress = customer_Adress[i];
            O.OrderDate = DateTime.Now;
            O.ShipDate = DateTime.Now;///////80%
            O.DeliveryDate = DateTime.Now;//////60%
            _order.Add(O);
        }
        for(;i<16;i++)
        {
            config.OrderIndex++;
            O.PrivateId = config.order_Number;
            O.CustomerName = customer_Name[i];
            O.CustomerEmail = customer_Email[i];
            O.CustomerAdress = customer_Adress[i];
            O.OrderDate = DateTime.Now;
            O.ShipDate = DateTime.Now ; //80%
            O.DeliveryDate = DateTime.Now + new TimeSpan(Rnd.Next(0, 7), Rnd.Next(0, 59), Rnd.Next(0, 59));//40%
            _order.Add(O);
        }
        for (;i<20;i++)
        {
            config.OrderIndex++;
            O.PrivateId = config.order_Number;
            O.CustomerName = customer_Name[i];
            O.CustomerEmail = customer_Email[i];
            O.CustomerAdress = customer_Adress[i];
            O.OrderDate = DateTime.Now;
            O.ShipDate = DateTime.Now + new TimeSpan(Rnd.Next(0, 2), Rnd.Next(0, 59), Rnd.Next(0, 59));//20%
            O.DeliveryDate = DateTime.Now + new TimeSpan(Rnd.Next(0, 7), Rnd.Next(0, 59), Rnd.Next(0, 59));//40%
            _order.Add(O);
        }
    }

    /// <summary>
    /// initializing order item
    /// </summary>
    internal static void S_orderItem()
    {
        for (int i = 0; i < 10; i++)
        {
            OrderItem OI = new OrderItem();
            OI.PrivateId = config.runOrderitem_Number;
            OI.OrderId = _product[i].PrivateId;
            OI.ProductId = _product[i].PrivateId;
            OI.PriceItem = _product[i].Price;
            OI.Amount = Rnd.Next(1,4);
            _orderItem.Add(OI);
        }
    }
}

