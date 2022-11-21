
using DO;

namespace BO;

public class Cart
{
    string CustomerName { get; set; }
    String CustomerEmail { get; set; }
    string CustomerAdress { get; set; }
    OrderItem Items { get; set; }
    double TotalPrice { get; set; }
    public override string ToString() => $@"
    Customer Name : {CustomerName}
    Customer Email : {CustomerEmail}
    Customer Adress : {CustomerAdress}
    Items : {Items}
    Total Price : {TotalPrice}
        ";
}
