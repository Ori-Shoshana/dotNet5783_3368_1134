
namespace BO;
/// <summary>
/// properties of order item
/// </summary>
public class OrderItem
{
    /// <summary>
    /// order item id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// order name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product id
    /// </summary>
    public int? ProductID { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// products amount (in the order)
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// total price of product
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// prints all the data about order item
    /// </summary>
    public override string ToString() => $@"
    Id : {ID}
    Name :{Name}
    Product ID : {ProductID}
    Price : {Price}
    Amount : {Amount}
    Total Price : {TotalPrice}
        ";
}


