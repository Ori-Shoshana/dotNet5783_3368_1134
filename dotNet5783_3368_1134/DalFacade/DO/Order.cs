
namespace DO;

/// <summary>
/// struct Order:
/// properties: private id, customer name, cusomer email, casomer adress, order date, ship date, delivery date
/// </summary>
public struct Order
{
    public int OrderID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public override string ToString() => $@"
    private Id : {OrderID}
    Customer Name : {CustomerName}
    Customer Email : {CustomerEmail}
    Customer Adress : {CustomerAdress}
    Order Date : {OrderDate}
    Ship Date : {ShipDate}
    Delivery Date : {DeliveryDate}
        ";
}
