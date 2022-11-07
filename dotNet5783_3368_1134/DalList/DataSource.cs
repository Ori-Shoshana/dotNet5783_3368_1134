using DO;
using System;
using System.ComponentModel;
using static Dal.DataSource;

namespace Dal;

internal static class DataSource
{

    public static readonly Random Rnd = new Random();
    internal static Order[] _order = new Order[100];
    internal static OrderItem[] _orderItem = new OrderItem[200];
    internal static Product[] _product = new Product[50];
    internal static class config
    {
        internal static int Productcounter { get; set; }
        internal static int Ordercounter { get; set; }
        internal static int OrderItemtName { get; set; }

        private static int orderNumber = 0;
        private static int orderitemNumber = 0;

        public static int order_Number { get => ++orderNumber; }
        public static int runOrderitem_Number { get => ++orderitemNumber; }

        internal static int OrderIndex = 0;
        internal static int OrderItemIndex = 0;
        internal static int ProductIndex = 0;
        internal static int PrivateId = 0;
    }
    private static void s_Initialize()
    {
        s_order();
        s_orderItem();
        s_product();
    }

    public static void s_product()
    {
        //not done yet, more to do...
    }
    public static void s_order()
    {
        //not done yet, more to do...
    }
    public static void s_orderItem()
    {
        //not done yet, more to do...
    }

}

