namespace DO;

public struct Order
{
   
    public int PrivateId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdrss { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DelivoryDate { get; set; }
    public override string ToString() => $@"
    private Id : {PrivateId}
    Customer Name : {CustomerName}
    Customer Email : {CustomerEmail}
    Customer Adrss : {CustomerAdrss}
    Order Date : {OrderDate}
    Ship Date : {ShipDate}
    Delivory ate : {DelivoryDate}
        ";
}
