using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class BoOrder : IBoOrder
{
    private IDal dal = new Dal.DalList();

    public IEnumerable<OrderForList> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Order OrderDetails(int id)
    {
        throw new NotImplementedException();
    }

    public Order Track(int id)
    {
        throw new NotImplementedException();
    }

    public Order UpdateDelivery(int id)
    {
        throw new NotImplementedException();
    }

    public Order UpdateShip(int id)
    {
        throw new NotImplementedException();
    }
}
