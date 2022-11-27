using BO;
namespace BlApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetProducts();
    public Product ProductDetailsM(int id);
    public ProductItem ProductDetailsC(int id,Cart cart);
    public void Add(Product product);
    public void Delete(int id);
    public void Update(Product product);

}
