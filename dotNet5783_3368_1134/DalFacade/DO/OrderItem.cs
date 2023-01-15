
namespace DO;

/// <summary>
/// struct OrderItem:
/// properties: private id, order id, product id, price, amount.
/// </summary>
public struct OrderItem
{
    public int OrderItemID { get; set; }
    public int OrderId { get; set; }
    public int ProductID { get; set; }
    public double PriceItem { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    private Id : {OrderItemID}
    Order Id : {OrderId}
    Prodoct Id : {ProductID}
    Price : {PriceItem}
    Amount : {Amount}
    ";
}
