
using DO;

namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public String? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double TotalPrice { get; set; }
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
