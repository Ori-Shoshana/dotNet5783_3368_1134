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
        if (DataSource.ListProduct.Any(product => prod.ProductID == product?.ProductID))
        {
            throw new DO.IdAlreadyExistException("Product Id already exists");
        }
        DataSource.ListProduct.Add(prod);  
        return prod.ProductID;
    }

    /// <summary>
    ///  The operation deletes a product from the array (finds him by id)
    /// </summary>
    public void Delete(int ID)
    {
        var found = (from p in DataSource.ListProduct
                     select
                     p?.ProductID).Where(temp => temp == ID);
        if (found.Count() == 1)
            DataSource.ListProduct.Remove(GetById(ID));
        else
            throw new DO.IdNotExistException("Product not exist");
    }

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
        foreach (DO.Product prod in DataSource.ListProduct)
        {
            if(prod.ProductID == productId)
            {
                return prod;
            }
        }
        throw new DO.IdNotExistException("Product Id not found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func)
    {
        List<DO.Product?> products = new List<DO.Product?>();
        if (func != null)
        {
            foreach (DO.Product? prod in ListProduct)
            {
                if (func(prod))
                {
                    products.Add(prod);
                }
            }
            return products;
        }
        else 
        {
            foreach (DO.Product? prod in ListProduct)
            {              
                    products.Add(prod);
            }
            return products;
        }
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
            foreach (DO.Product prod in ListProduct)
            {
                if (func(prod))
                {
                    return prod;
                }
            }
        }
        throw new DO.NoObjectFoundExeption("No object is of the delegate");
    }
}