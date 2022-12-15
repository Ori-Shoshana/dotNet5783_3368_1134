
using BlApi;
namespace BlImplementation;

internal class BL : IBl
{
    /// <summary>
    /// Allows creation of order
    /// </summary>
    public IOrder Order => new BoOrder();
    /// <summary>
    /// Allows creation of cart
    /// </summary>
    public ICart Cart => new BoCart();
    /// <summary>
    /// Allows creation of product
    /// </summary>
    public IProduct Product => new BoProduct();
}
