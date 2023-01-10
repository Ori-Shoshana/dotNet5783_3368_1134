using System;
using static Dal.DataSource;
using DalApi;
using System.Security.Cryptography;
namespace Dal;

/// <summary>
/// class Dal product: 
/// Implementation of add, delete, update and return operations
/// </summary>
internal class DalProduct : IProduct
{
    /// <summary>
    /// The operation accepts a product and adds it in the array
    /// </summary>
    /// <returns> returns order id </returns>
    public int Add(DO.Product prod)
    {
        var check = (from p in ListProduct select p?.ProductID).Where(temp => temp == prod.ProductID);
        if (check.Count()==0)
        {
            ListProduct.Add(prod);
            return prod.ProductID;
        }
        throw new DO.IdAlreadyExistException("Product Id already exists");
    }
    //if (DataSource.ListProduct.Any(product => prod.ProductID == product?.ProductID))
    //    throw new DO.IdAlreadyExistException("Product Id already exists");
    //DataSource.ListProduct.Add(prod);   return prod.ProductID;*/
    /// <summary>
    ///  The operation deletes a product from the array (finds him by id)
    /// </summary>
    public void Delete(int prodId)
    {
        var check = (from p in ListProduct let temp = p?.ProductID select p?.ProductID).Where(temp => temp == prodId);
        if(check.Count()==1)
            ListProduct.Remove(GetById(prodId));
        else
            throw new DO.IdNotExistException("product does not exist");
    }
    //if (DataSource.ListProduct.Any(product => product?.ProductID == prodId))
    //    DataSource.ListProduct.Remove(GetById(prodId));
    //else
    //    throw new DO.IdNotExistException("product does not exist");
    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void Update(DO.Product product)
    { 
        bool found = false; 
        var foundProduct = DataSource.ListProduct.FirstOrDefault(p => p?.ProductID == product.ProductID);
        if (foundProduct != null)
        {
            found = true;
            int index = DataSource.ListProduct.IndexOf(foundProduct);
            DataSource.ListProduct[index] = product;
        }
        if (found == false)
            throw new DO.IdNotExistException("Product Id not found");
    }

    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public DO.Product GetById(int productId)
    {
        var product = DataSource.ListProduct.FirstOrDefault(x => x?.ProductID == productId);

        if (product == null)
            throw new DO.IdNotExistException("Product Id not found");
        else
            return (DO.Product)product;
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func)
    {
        var products = ListProduct.Where(prod => func == null || func(prod)).OrderBy(prod => prod?.ProductID).ToList();
        return products;
    }
    public IEnumerable<IGrouping<int, DO.Product?>> GetAllGroupedBy(Func<DO.Product?, bool>? func)
    {
        var products = ListProduct.Where(prod => func == null || func(prod)).GroupBy(prod => prod?.ProductID).OrderBy(group => group.Key);
        return (IEnumerable<IGrouping<int, DO.Product?>>)products;
    }
    /// <summary>
    /// returns array length
    /// </summary>
    public int ListLength() 
    {
        return DataSource.ListProduct.Count;
    }

    public DO.Product GetByDelegate(Func<DO.Product?, bool>? func)
    {
        if (func != null)
        {
            var p = ListProduct.FirstOrDefault(p => func(p));
            if (p != null)
                return (DO.Product) p;
        }
        throw new DO.NoObjectFoundExeption("No object is of the delegate");
    }
}