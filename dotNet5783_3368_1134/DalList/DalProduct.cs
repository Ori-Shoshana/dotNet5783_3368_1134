using DO;
using System;
using static Dal.DataSource;
using DalApi;



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
    public int Add(Product prod)
    {
        foreach (Product product in DataSource.ListProduct)
        {
            if (prod.ProductID == product.ProductID)
                throw new IdAlreadyExistException("Product Id already exist's");
        }
        DataSource.ListProduct.Add(prod);  
        return prod.ProductID;
    }

    /// <summary>
    ///  The operation deletes a product from the array (finds him by id)
    /// </summary>
    public void Delete(int ID)
    {
        
            bool found = false;
            foreach(Product prod in DataSource.ListProduct)
            {
                if(prod.ProductID == ID)
                {
                    ListProduct.Remove(prod);
                    found = true;
                    break;
                }
            }
            if(found == false)
            {
                throw new  IdNotExistException("Product Id not found");
        }

    }

    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void Update(Product product)
    {
        Product? itemToUpdate = DataSource.ListProduct
            .FirstOrDefault(prod => prod?.ProductID == product.ProductID);
        if (itemToUpdate == null)
        {
            throw new IdNotExistException("Product Id not found");
        }

        int index = DataSource.ListProduct.IndexOf(itemToUpdate);
        DataSource.ListProduct[index] = product;
    }

    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public Product GetById(int productId)
    {
        foreach (Product prod in DataSource.ListProduct)
        {
            if(prod.ProductID == productId)
            {
                return prod;
            }
        }
        throw new IdNotExistException("Product Id not found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func)
    {
        List<Product?> products = new List<Product?>();
        if (func != null)
        {
            foreach (Product? prod in ListProduct)
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
            foreach (Product? prod in ListProduct)
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

    public Product GetByDelegate(Func<Product?, bool>? func)
    {
        if (func != null)
        {
            foreach (Product prod in ListProduct)
            {
                if (func(prod))
                {
                    return prod;
                }
            }
        }
        throw new NoObjectFoundExeption("No object is of the delegate");
    }
}