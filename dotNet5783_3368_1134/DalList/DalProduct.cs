using DO;
using System.ComponentModel;
using System.Diagnostics;

namespace Dal;

public class DalProduct
{
    public static addDalProduct(int privateId, string prodoctName, CategoryAttribute category, double price, int inStock)
    {
        Product newProdoct = new Product();
        newProdoct.PrivateId = privateId;
        newProdoct.ProdoctName = prodoctName;
        newProdoct.Category = category;
        newProdoct.Price = price;
        newProdoct.InStock = inStock;
        //add to arrayS
    }
}
