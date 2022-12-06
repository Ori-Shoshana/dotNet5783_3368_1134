
using static BO.Enums;

namespace BO;

/// <summary>
/// properties of order for list
/// </summary>
public class OrderForList
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// customer name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public OrderStatus? Status {get; set;}
    /// <summary>
    /// amount of items
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    /// total price
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// prints all the data about order for list
    /// </summary>
    public override string ToString() => $@"
    ID : {ID}
    Customer Name : {CustomerName}
    Amount Of Items : {AmountOfItems}
    Total Price : {TotalPrice}
    Status : {Status}
        ";

}
