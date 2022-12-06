namespace BO;
/// <summary>
/// properties of cart
/// </summary>
public class Cart
{ 
    /// <summary>
    /// customer name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// CustomerEmail
    /// </summary>
    public String? CustomerEmail { get; set; }
    /// <summary>
    /// CustomerAdress
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// list of order items
    /// </summary>
    public List<OrderItem>? Items { get; set; }
    /// <summary>
    /// TotalPrice
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// prints all the cart data
    /// </summary>
    public override string ToString()
    {
    string st = $@"
    Customer Name : {CustomerName}
    Customer Email : {CustomerEmail}
    Customer Adress : {CustomerAdress}
    ";
             int i = 1;
            if (Items != null)
            {
                foreach(var item in Items)
                {
                    st += $@" {i++}:
                    ID:{item.ID}
                    Name:{item.Name}
                    Price:{item.Price}
                    ProductID:{item.ProductID}
                    Amount:{item.Amount}
                    TotalProce:{item.TotalPrice}
                    ";
                }
            }
            return st;
    }
}
