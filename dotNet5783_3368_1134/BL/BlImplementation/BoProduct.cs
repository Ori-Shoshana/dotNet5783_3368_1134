using BlApi;
using BO;
using DalApi;
using System.Collections.Immutable;
using System.Diagnostics;
using static BO.Enums;
namespace BlImplementation;

internal class BoProduct : BlApi.IProduct
{
    private IDal dal = new Dal.DalList();

    public IEnumerable<ProductForList> GetProducts()
    {
        int i = 0;
        List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
        List<DO.Product> products = new List<DO.Product>();
        products = (List<DO.Product>)dal.Product.GetAll();
        foreach (DO.Product product in products)
        {
            productsForList.Add(new ProductForList
            {
                ID = products[i].ProductID,
                Category = (Enums.ProductCategory)products[i].Category,
                Name = products[i].ProductName,
                Price = products[i].Price
            });
            i++;
        }
        return productsForList;
    }

    public BO.Product ProductDetailsM(int id)
    {
        DO.Product DoProduct = new DO.Product();
        BO.Product? BoProduct = new BO.Product();
        if (id > 0)
        {
        
            DoProduct = dal.Product.GetById(id);          
            BoProduct.ID = DoProduct.ProductID;
            BoProduct.Name = DoProduct.ProductName;
            BoProduct.Price = DoProduct.Price;
            BoProduct.Category = (ProductCategory)DoProduct.Category;
            BoProduct.InStock = DoProduct.InStock;
            return BoProduct;
        }
        throw new VariableIsSmallerThanZeroExeption("Id is les than 0");
    }
    
//********************************************************************//
    public BO.Product ProductDetailsC(int id, BO.Cart cart)
    {
        throw new NotImplementedException();
    }
//*******************************************************************//
    public void Add(BO.Product product)
    {
        if (product.ID < 0) throw new VariableIsSmallerThanZeroExeption("Id is less than 0");
        if (product.Name == null) throw new VariableIsNullExeption("The name is null");
        if (product.Price < 0) throw new VariableIsSmallerThanZeroExeption("the price is less than 0");
        if (product.InStock < 0) throw new VariableIsSmallerThanZeroExeption("the stock is less than 0");

        DO.Product DoProduct = new DO.Product();
        DoProduct.ProductID = (int)product.ID;
        DoProduct.ProductName = product.Name;
        DoProduct.Price = product.Price;
        DoProduct.Category = (DO.Enums.productCategory)product.Category;
        DoProduct.InStock = product.InStock;

        dal.Product.Add(DoProduct);
    }
    public void Delete(int id)
    {
        int i = 0;
        List<DO.Product> products = new List<DO.Product>();
        foreach(DO.Product product in (List<DO.Product>)dal.Product.GetAll())
        {
            if (id == products[i].ProductID)
            {
               break;
            }
            i++;
        }
        if(i == products.Count)
        {
            throw new IdNotExistException("there is no ptoduct to delete");
        }    
        dal.Product.Delete(id);
    }
    public void update(BO.Product product)
    {
        if (product.ID < 0) throw new VariableIsSmallerThanZeroExeption("Id is less than 0");
        if (product.Name == null) throw new VariableIsNullExeption("The name is null");
        if (product.Price < 0) throw new VariableIsSmallerThanZeroExeption("the price is less than 0");
        if (product.InStock < 0) throw new VariableIsSmallerThanZeroExeption("the stock is less than 0");

        DO.Product DoProduct = new DO.Product();

        DoProduct.ProductID = (int)product.ID;
        DoProduct.ProductName = product.Name;
        DoProduct.Price = product.Price;
        DoProduct.Category = (DO.Enums.productCategory)product.Category;
        DoProduct.InStock = product.InStock;

        try
        {
            dal.Product.Update(DoProduct);
        }
        catch(IdNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }
}
