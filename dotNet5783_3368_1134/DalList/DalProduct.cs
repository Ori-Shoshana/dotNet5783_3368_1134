using DO;
using System;
using static Dal.DataSource;


namespace Dal;

public class DalProduct
{
    public DalProduct()
    {
        DataSource.S_Initialize();
    }
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

    public Product Get(int productId)
    {
        for (int i = 0; i < DataSource.config.ProductIndex; i++)
        {
            if (_product[i].PrivateId == productId)
                return _product[i];
        }
        throw new Exception("no product found");
    }

    public static Product[] GetProductArray()
    {
        Product[] productArr = new Product[50];
        for (int i = 0; i < DataSource.config.ProductIndex; i++)
        {
            productArr[i].PrivateId = _product[i].PrivateId;
            productArr[i].Price = _product[i].Price;
            productArr[i].InStock = _product[i].InStock;
            productArr[i].ProdoctName = _product[i].ProdoctName;
            productArr[i].Category= _product[i].Category;  
        }
        return productArr;
    }
    public int ProductLeangth()
    {
        return DataSource.config.ProductIndex;
    }
}

