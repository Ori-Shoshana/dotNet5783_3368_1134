using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem entity)
    {

        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public OrderItem GetByDelegate(Func<OrderItem?, bool>? func)
    {
        throw new NotImplementedException();
    }

    public OrderItem GetById(int id)
    {
        throw new NotImplementedException();
    }

    public int ListLength()
    {
        throw new NotImplementedException();
    }

    public void Update(OrderItem entity)
    {
        throw new NotImplementedException();
    }
}
