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
    public DalProduct()
    {
        DataSource.S_Initialize();
    }

    /// <summary>
    /// The operation accepts a product and adds it in the array
    /// </summary>
    /// <returns> returns order id </returns>
    public int Add(Product prod)
    {
        foreach (Product product in DataSource.ListProduct)
        {
            if (prod.PrivateId == product.PrivateId)
                throw new IdAlreadyExistException("Product Id already exist");
        }
        DataSource.ListProduct.Add(prod);  
        return prod.PrivateId;
    }

    /// <summary>
    ///  The operation deletes a product from the array (finds him by id)
    /// </summary>
    public void Delete(int ID)
    {
        {
            bool found = false;
            foreach(Product prod in DataSource.ListProduct)
            {
                if(prod.PrivateId == ID)
                {
                    ListProduct.Remove(prod);
                    found = true;
                    break;
                }
            }
            if(found == false)
            {
                throw new IdNotExistException("Product Id not found");
            }
        }
    }

    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void Update(Product product)
    {
        int index = 0;
        bool found = false; 
        foreach(Product prod in DataSource.ListProduct)
            { 
                if(prod.PrivateId == product.PrivateId)
                {
                    found = true;
                    ListProduct[index] = product;
                    break;
                }
            index++;
        }
        if (found == false)
        {
            throw new IdNotExistException("Product Id not found");
        }
    }

    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public Product GetById(int productId)
    {
        foreach (Product prod in DataSource.ListProduct)
        {
            if(prod.PrivateId == productId)
            {
                return prod;
            }
        }
        throw new IdNotExistException("Product Id not found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<Product> GetAll()
    {
       List<Product> products = new List<Product>(DataSource.ListProduct);
       
       return products;
    }

    /// <summary>
    /// returns array length
    /// </summary>
    public int ListLeangth() 
    {
        return DataSource.ListProduct.Count;
    }
}