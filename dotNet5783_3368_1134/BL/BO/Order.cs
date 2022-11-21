
using DO;
using System.Globalization;
using static BO.Enums;

namespace BO
{
    public class Order
    {
        int ID { get; set; }    
        string CustomerName { get; set; }
        String CustomerEmail { get; set; }  
        string CustomerAdress { get; set; } 
        DateTime OrderDate { get; set; }    
        DateTime ShipDate { get; set; }
        DateTime DeliveryDate { get; set; }
        OrderItem Items { get; set; }
        double TotalPrice { get; set; }
        OrderStatus Status {get; set;}
        public override string ToString() => $@"
    private Id : {ID}
    Customer Name : {CustomerName}
    Customer Email : {CustomerEmail}
    Customer Adress : {CustomerAdress}
    Order Date : {OrderDate}
    Ship Date : {ShipDate}
    Delivery Date : {DeliveryDate}
    Items : {Items}
    Total Price : {TotalPrice}
    Status : {Status}
        ";
    }
}
