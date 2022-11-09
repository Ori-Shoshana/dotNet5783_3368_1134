

namespace DO;

public struct OrderItem
{
    public int PrivateId { get; set; }
    public int OrderId { get; set; }
    public int ProdoctId { get; set; }
    public int Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    private Id : {PrivateId}
    Order Id : {OrderId}
    Prodoct Id : {ProdoctId}
    Price : {Price}
    Amount : {Amount}
    ";

}
