
using System.ComponentModel;
using static BO.Enums;

namespace BO;

public class Product
{
    public int? ID { get; set; }    
    public string? Name { get; set; }   
    public double Price { get; set; }
    public ProductCategory Category { get; set; }
    public int InStock { get; set; }

    
     public override string ToString() => $@"
    private ID : {ID}
    Prodoct Name : {Name}
    Category : {Category}
    Price : {Price}
    In Stock : {InStock}
    ";

}
