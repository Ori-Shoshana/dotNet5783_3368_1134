

namespace DO;

public struct OrderItem
{
    public int IdPrivate { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdrss { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DelivoryDate { get; set; }
}
