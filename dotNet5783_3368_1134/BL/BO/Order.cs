
using System.Globalization;
using static BO.Enums;

namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public String CustomerEmail { get; set; }
        public string CustomerAdress { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus? Status {get; set;}
        public override string ToString() 
        { 
            string st = $@"
            private Id : {ID}
            Customer Name : {CustomerName}
            Customer Email : {CustomerEmail}
            Customer Adress : {CustomerAdress}
            Order Date : {OrderDate}
            Ship Date : {ShipDate}
            Delivery Date : {DeliveryDate}
            Status : {Status}
            Items:
            ";
            int i = 1;
            if (Items != null)
            {
                foreach(var item in Items)
                {
                    st+= $@" {i++}:
                    ID:{item.ID}
                    Name:{item.Name}
                    Price:{item.Price}
                    ProductID:{item.ProductID}
                    Amount:{item.Amount}
                    TotalProce:{item.TotalPrice}
                    ";
                }
            }
            return st;
        }
        
    }
}
