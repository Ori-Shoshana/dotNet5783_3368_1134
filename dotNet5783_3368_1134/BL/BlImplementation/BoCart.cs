using BlApi;
using DalApi;
using BO;

namespace BlImplementation;

internal class BoCart : ICart
{
    private IDal dal = new Dal.DalList();

    public BO.Cart Add(BO.Cart C, int id)
    {

        IEnumerable<DO.Product> DoProducts = dal.Product.GetAll();
        BO.Product BoProduct;
        BO.OrderItem BoOrderItem;

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
