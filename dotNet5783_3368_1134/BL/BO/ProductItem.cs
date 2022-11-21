
using System.ComponentModel;
using static BO.Enums;

namespace BO;

public class ProductItem
{
    int ID { get; set; }
    string? Name { get; set; }
    double Price { get; set; }
    ProductCategory Category { get; set; }
    bool InStock { get; set; }
    int Amount { get; set; }   



    public override string ToString() => $@"
    private ID : {ID}
    Prodoct Name : {Name}
    Category : {Category}
    Price : {Price}
    In Stock : {InStock}
    Amount : {Amount}
    ";
}
