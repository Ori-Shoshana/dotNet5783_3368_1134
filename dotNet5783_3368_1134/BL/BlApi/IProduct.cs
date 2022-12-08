using BO;
using System.Collections;

namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// decleration of function get products
    /// request products list from the data layer
    /// Build a product list based on the data
    /// returns the list
    public IEnumerable<ProductForList?> GetProducts(Func<DO.Product?, bool>? func = null);
    /// <summary>
    /// decleration of function product details (for manager)
    /// receives product id
    /// trying to request the product from the data layer 
    /// builds the product
    /// Throws exceptions when needed
    /// returns the product
    public Product ProductDetailsM(int id);
    /// <summary>
    /// decleration of function product details (for customer)
    /// receives product id and cart
    /// trying to request the product from the data layer 
    /// builds  productItem
    /// Throws exceptions when needed
    /// returns productItem
    public ProductItem ProductDetailsC(int id,Cart cart);
    /// <summary>
    /// decleration of function add
    /// receives product
    /// checks data integrity
    /// adds the product to the data layer
    /// Throws exceptions when needed
    public void Add(Product product);
    /// <summary>
    /// decleration of function delete 
    /// receives the product id
    /// checks if the product exits
    /// trying to delete him from the data layer
    /// Throws exceptions when needed
    public void Delete(int id);
    /// <summary>
    /// decleration of function update
    /// receives product 
    /// checks integrity in the data layer
    /// updates the product
    /// Throws exceptions when needed
    public void Update(Product product);
}
