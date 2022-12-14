
using System.ComponentModel;
using static BO.Enums;

namespace BO;
/// <summary>
/// properties of product for list
/// </summary>
public class ProductForList
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
    public ProductCategory? Category { get; set; }

    /// <summary>
    /// prints all the data of product for list
    /// </summary>
    public override string ToString() => $@"
    private ID : {ID}
    Prodoct Name : {Name}
    Category : {Category}
    Price : {Price}
    ";

}
