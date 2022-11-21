using BlApi;
using DalApi;
using BO;

namespace BlImplementation;

internal class BoCart : IBoCart
{
    private IDal dal = new Dal.DalList();

    public BO.Cart Add(Cart C, int id)
    {
        //IEnumerable<DO.Product> DoProducts = dal.Product.GetAll();
        //BO.Product BoProduct;
        //BO.OrderItem BoOrderItem;
        
        //if(id == dal.Product.ID)
        throw new NotImplementedException();
    }

    public void Confermation(Cart C)
    {
        throw new NotImplementedException();
    }

    public BO.Cart Update(Cart C, int ID, int amount)
    {
        throw new NotImplementedException();
    }
}
