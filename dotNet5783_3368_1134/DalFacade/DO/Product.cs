using System.ComponentModel;
using static DO.Enums;

namespace DO;

/// <summary>
/// struct product:
/// properties: private id, Prodoct Name, Category, price, In Stock.
/// </summary>
public struct Product
{
    public int PrivateId { get; set; }
    public string ProdoctName { get; set; }
    public productCategory Category { get; set; }
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