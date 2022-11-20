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
    public int AddProduct(Product prod)
    {
        foreach (Product product in DataSource._product)
        {
            if (prod.PrivateId == product.PrivateId)
                throw new Exception("they have the same product alrady");
        }
        DataSource._product.Add(prod);  
        return prod.PrivateId;
    }

    /// <summary>
    ///  The operation deletes a product from the array (finds him by id)
    /// </summary>
    public void DeleteProdoct(int ID)
    {
        {
            bool found = false;
            foreach(Product prod in DataSource._product)
            {
                if(prod.PrivateId == ID)
                {
                    _product.Remove(prod);
                    found = true;
                    break;
                }
            }
            if(found == false)
            {
                throw new Exception("no Prodoct found");
            }
        }
    }

    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void UpdateProduct(Product product)
    {
        int index =0;
        bool found = false; 
        foreach(Product prod in DataSource._product)
            {
             index++;   
                if(prod.PrivateId == product.PrivateId)
                {
                    found = true;
                DataSource._product[index] = product;
                }
            }
        if (found == false)
        {
            throw new Exception("no Prodoct found");
        }
    }

    /// <summary>
    ///  The operation finds the order (finds him by id) and returns his details
    /// </summary>
    public Product Get(int productId)
    {
        foreach (Product prod in DataSource._product)
        {
            if(prod.PrivateId == productId)
            {
                return prod;
            }
        }
        throw new Exception("no product found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public IEnumerable<Product> GetProductList()
    {
       List<Product> products = new List<Product>(DataSource._product);
       
       return products;
    }

    /// <summary>
    /// returns array length
    /// </summary>
    public int ProductLeangth()
    {
        return DataSource._product.Count;
    }
}