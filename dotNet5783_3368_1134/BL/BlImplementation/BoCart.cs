using BlApi;
using BO;
using DalApi;
using System.Collections.Immutable;
using System.Globalization;
using static BO.Enums;
namespace BlImplementation;

internal class BoCart : ICart
{
    private IDal dal = new Dal.DalList();
    DO.Product i;

    public BO.Cart Add(BO.Cart C, int id)
    {
        IEnumerable<DO.Product> DoProducts = dal.Product.GetAll();
        BO.Product? Product;
        BO.OrderItem? OrderItem;
        
        if (id == null)
        {

        }
            throw new NotImplementedException();
    }

    public void Confermation(BO.Cart C)
    {
        throw new NotImplementedException();
    }

    public BO.Cart Update(BO.Cart C, int ID, int amount)
    {
        throw new NotImplementedException();
    }
}
