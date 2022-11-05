using System.ComponentModel;

namespace DO;

public struct Product
{
    public int IdPrivate { get; set; }
    public string ProdoctName { get; set; }
    public CategoryAttribute Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
    
}