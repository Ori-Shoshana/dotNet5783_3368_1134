
using System.Globalization;
using static BO.Enums;
/// <summary>
/// properties of order
/// </summary>
namespace BO
{
    public class Order
    {
        /// <summary>
        /// order id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Customer Name
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// Customer Email
        /// </summary>
        public String? CustomerEmail { get; set; }
        /// <summary>
        /// Customer Adress
        /// </summary>
        public string? CustomerAdress { get; set; }
        /// <summary>
        /// Order Date
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// ShipDate
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// DeliveryDate
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// List of Items (type order item)
        /// </summary>
        public List<OrderItem?>? Items { get; set; }
        /// <summary>
        /// total price
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// order status
        /// </summary>
        public OrderStatus? Status {get; set;}
        /// <summary>
        /// prints all the order data
        /// </summary>
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
                    ID:{item?.ID}
                    Name:{item?.Name}
                    Price:{item?.Price}
                    ProductID:{item?.ProductID}
                    Amount:{item?.Amount}
                    TotalProce:{item?.TotalPrice}
                    ";
                }
            }
            return st;
        }
        
    }
}
