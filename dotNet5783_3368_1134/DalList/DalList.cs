
using DalApi;
namespace Dal;
/// <summary>
/// class DalList - Contains constructors to initialize an order/product/order item
/// </summary>
internal sealed class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();
    private DalList() { }
}
