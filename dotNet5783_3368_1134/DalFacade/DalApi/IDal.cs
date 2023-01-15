
namespace DalApi;
/// <summary>
/// interface IDal (kind of father of the entities (order, order item and product) )
/// </summary>
public interface IDal
{
    IOrder Order { get; }
    IOrderItem OrderItem { get; }   
    IProduct Product { get; }   
}
