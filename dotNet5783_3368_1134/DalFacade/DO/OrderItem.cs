namespace DO;
/// <summary>
/// struct OrderItem:
/// properties: private id, order id, product id, price, amount.
/// </summary>
public struct OrderItem
{
    public int PrivateId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public double PriceItem { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    private Id : {PrivateId}
    Order Id : {OrderId}
    Prodoct Id : {ProductId}
    Price : {PriceItem}
    Amount : {Amount}
    ";

}
