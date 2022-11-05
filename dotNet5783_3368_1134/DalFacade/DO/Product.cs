using System.ComponentModel;

namespace DO;

public struct Product
{
    public int IdPrivate { get; set; }
    public string ProdoctName { get; set; }
    public CategoryAttribute Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
    private Id : {IdPrivate}
    Prodoct Name : {ProdoctName}
    Category : {Category}
    Price : {Price} 
    In Stock : {InStock}
    ";

}