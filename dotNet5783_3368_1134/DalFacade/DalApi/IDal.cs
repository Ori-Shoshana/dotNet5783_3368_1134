
using DO;
namespace DalApi;

public interface IDal
{
    IOrder order { get; }
    IOrderItem OrderItem { get; }   
    IProduct Product { get; }   
}
