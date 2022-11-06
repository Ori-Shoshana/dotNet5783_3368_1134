using DO;
using System.ComponentModel;

namespace Dal;

internal static class DataSource
{

    public static readonly Random Rnd = new Random();
    internal static Order[] _order = new Order[100];
    internal static OrderItem[] _orderItem = new OrderItem[200];
    internal static Product[] _product = new Product[50];
    internal static class config
    {
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

    public static void s_order()
    {

    }

    public static void s_orderItem()
    {

    }

    public static void s_product()
    {

    }

}

