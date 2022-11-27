
using DO;

namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public String? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
    Customer Name : {CustomerName}
    Customer Email : {CustomerEmail}
    Customer Adress : {CustomerAdress}
    Items : {Items}
    Total Price : {TotalPrice}
        ";
}
