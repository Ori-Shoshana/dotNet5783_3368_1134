using System.ComponentModel;

namespace DO;

public struct Product
{
    public int PrivateId { get; set; }
    public string ProdoctName { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
    private Id : {PrivateId}
    Prodoct Name : {ProdoctName}
    Category : {Category}
    Price : {Price} 
    In Stock : {InStock}
    ";

}