using BlApi;
using BO;
using DalApi;
using System.Diagnostics;

namespace BlImplementation;

internal class BoProduct : IBoProduct
{
    private IDal dal = new Dal.DalList();

    public IEnumerable<ProductForList> GetProducts()
    {
        throw new NotImplementedException();
    }

    public void Add(BO.Product product)
    {
        if(product.ID)
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Product ProductDetailsC(int id, Cart cart)
    {
        throw new NotImplementedException();
    }

    public Product ProductDetailsM(int id)
    {
        if(id > 0)
        {

        }
        throw new NotImplementedException();
    }

    public void update(Product product)
    {
        throw new NotImplementedException();
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
