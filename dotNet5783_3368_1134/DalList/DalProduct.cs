using DO;
using System;
using static Dal.DataSource;


namespace Dal;

public class DalProduct
{
    public int addProduct (Product prod)
    {
        foreach(Product product in DataSource._product)
        {
            if (prod.PrivateId == product.PrivateId)
                throw new Exception("they have the same product alrady");
        }

        DataSource._product[DataSource.config.ProductIndex++] = prod;
        return prod.PrivateId;
    }

    public void deleteProdoct (Product prod)
    {
        {
            int i;
            for (i = 0; i < DataSource.config.ProductIndex; i++)
            {
                if (_product[i].PrivateId == prod.PrivateId) 
                break;
            }
            if (i == DataSource.config.ProductIndex)
                throw new Exception("no Prodoct found");
            else
            {
                _product[i].PrivateId = _product[i++].PrivateId;
                DataSource.config.ProductIndex--;
            }
        }
    }
    
    public void updateProduct(Product product)
    {
        int i;
        int x = 0;
        for(i=0; i < DataSource.config.ProductIndex; i++)
        {
            if(_product[i].PrivateId == product.PrivateId)
            {
                _product[i] = product;
                x = 1;
                break;
            }
        }
        if(x == 0)
        {
            throw new Exception("no Prodoct found");
        }
    }
    
}
