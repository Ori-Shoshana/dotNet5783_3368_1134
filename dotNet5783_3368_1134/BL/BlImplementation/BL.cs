
using BlApi;
namespace BlImplementation;

public class BL : IBl
{
    public IOrder Order => new BoOrder();
    public ICart Cart => new BoCart();
    public IProduct Product => new BoProduct();
}
