using DO;
using System;
using static Dal.DataSource;


namespace Dal;

/// <summary>
/// class Dal product: 
/// Implementation of add, delete, update and return operations
/// </summary>
public class DalProduct
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
            bool? found = false;
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
        for (int i = 0; i < DataSource.config.ProductIndex; i++)
        {
            if (_product[i].PrivateId == productId)
                return _product[i];
        }
        throw new Exception("no product found");
    }

    /// <summary>
    /// The operation updates the array and returns him
    /// </summary>
    public static Product[] GetProductArray()
    {
        Product[] productArr = new Product[50];
        for (int i = 0; i < DataSource.config.ProductIndex; i++)
        {
            productArr[i] = _product[i];
        }
        return productArr;
    }

    /// <summary>
    /// returns array length
    /// </summary>
    public int ProductLeangth()
    {
        return DataSource.config.ProductIndex;
    }
}