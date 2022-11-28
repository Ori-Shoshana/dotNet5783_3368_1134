
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    void Add(global::BO.OrderItem item);
}
