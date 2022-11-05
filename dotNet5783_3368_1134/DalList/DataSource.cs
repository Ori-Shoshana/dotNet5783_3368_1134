
using DO;
using System.ComponentModel;

namespace Dal;

internal static class DataSource
{
    
    public readonly static Random Rnd = new Random();
    internal static Order[] _order = new Order[100];
    internal static OrderItem[] _orderItem = new OrderItem[200];
    internal static Product[] _product = new Product[50];

    private static void addOrder(int idPrivate, int idOrder, int idProdoct, int price, int amount )
    {
    }
    private static void addOrderItem(int idPrivate, string customerName, string customerEmail, string customerAdrss, DateTime orderDate, DateTime shipDate, DateTime delivoryDate )
    { 
    }
    private static void addProdoct(int idPrivate, string prodoctName, CategoryAttribute category, double price, int inStock)
    { 
    }
    private static void s_Initialize()
    { 
    }


    

}

