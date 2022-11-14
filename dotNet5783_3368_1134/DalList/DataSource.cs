using DO;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
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
            config.ProductIndex++;
            _product[i].PrivateId = Rnd.Next(100000, 999999);
            _product[i].ProdoctName = Name[i];
            _product[i].Category = My_Category[Rnd.Next(0,4)];
            _product[i].Price = ProductPrice[i];
            if(i < 10)
            {
                _product[i].InStock = Rnd.Next(3, 50);
            }
            else
            {
                _product[i].InStock = 0;
            }
        }
    }
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
        for (i=0; i<12; i++)
        {
            config.OrderIndex++;
            _order[i].PrivateId = config.order_Number;
            _order[i].CustomerName = customer_Name[i];
            _order[i].CustomerEmail = customer_Email[i];
            _order[i].CustomerAdress = customer_Adress[i];
            _order[i].OrderDate = DateTime.Now;
            _order[i].ShipDate = DateTime.Now;///////80%
            _order[i].DelivoryDate = DateTime.Now;//////60%
        }
        for(;i<16;i++)
        {
            config.OrderIndex++;
            _order[i].PrivateId = config.order_Number;
            _order[i].CustomerName = customer_Name[i];
            _order[i].CustomerEmail = customer_Email[i];
            _order[i].CustomerAdress = customer_Adress[i];
            _order[i].OrderDate = DateTime.Now;
            _order[i].ShipDate = DateTime.Now ; //80%
            _order[i].DelivoryDate = DateTime.Now + new TimeSpan(Rnd.Next(0, 7), Rnd.Next(0, 59), Rnd.Next(0, 59));//40%
        }
        for(;i<20;i++)
        {
            config.OrderIndex++;
            _order[i].PrivateId = config.order_Number;
            _order[i].CustomerName = customer_Name[i];
            _order[i].CustomerEmail = customer_Email[i];
            _order[i].CustomerAdress = customer_Adress[i];
            _order[i].OrderDate = DateTime.Now;
            _order[i].ShipDate = DateTime.Now + new TimeSpan(Rnd.Next(0, 2), Rnd.Next(0, 59), Rnd.Next(0, 59));//20%
            _order[i].DelivoryDate = DateTime.Now + new TimeSpan(Rnd.Next(0, 7), Rnd.Next(0, 59), Rnd.Next(0, 59));//40%
        }
    }
    internal static void S_orderItem()
    {
        for (int i = 0; i < 10; i++)
        {
            config.MyPrivateId++;
            config.OrderItemIndex++;
         
            _orderItem[i].PrivateId = config.MyPrivateId;
            _orderItem[i].OrderId = _product[i].PrivateId;
            _orderItem[i].ProductId = _product[i].PrivateId;
            _orderItem[i].PriceItem = _product[i].Price;
            _orderItem[i].Amount = Rnd.Next(1,4);
            
        }
    }

}

