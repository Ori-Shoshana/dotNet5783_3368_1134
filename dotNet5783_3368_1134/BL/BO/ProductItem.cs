
using System.ComponentModel;
using static BO.Enums;

namespace BO;
/// <summary>
/// properties of product item
/// </summary>
public class ProductItem
{
    /// <summary>
    /// product id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// product category
    /// </summary>
    public ProductCategory Category { get; set; }
    /// <summary>
    /// in stock
    /// </summary>
    public bool InStock { get; set; }
    /// <summary>
    /// amount (in the cart)
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// prints all the data about product item
    /// </summary>
    public override string ToString() => $@"
    private ID : {ID}
    Prodoct Name : {Name}
    Category : {Category}
    Price : {Price}
    In Stock : {InStock}
    Amount : {Amount}
    ";
}
