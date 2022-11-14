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

        DataSource._product[DataSource.config.ProductIndex++] = prod;
        return prod.PrivateId;
    }

    /// <summary>
    ///  The operation deletes a product from the array (finds him by id)
    /// </summary>
    public void DeleteProdoct(int ID)
    {
        {
            int i;
            for (i = 0; i < DataSource.config.ProductIndex; i++)
            {
                if (_product[i].PrivateId == ID)
                    break;
            }
            if (i == DataSource.config.ProductIndex)
                throw new Exception("no Prodoct found");
            else
            {
                for (; i < DataSource.config.ProductIndex; i++)
                {
                    _product[i].PrivateId = _product[i++].PrivateId;
                }
                DataSource.config.ProductIndex--;
            }
        }
    }

    /// <summary>
    /// The operation updates an order in the array (finds him by id)
    /// </summary>
    public void UpdateProduct(Product product)
    {
        int i;
        int x = 0;
        for (i = 0; i < DataSource.config.ProductIndex; i++)
        {
            if (_product[i].PrivateId == product.PrivateId)
            {
                _product[i] = product;
                x = 1;
                break;
            }
        }
        if (x == 0)
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