using BO;
namespace BlApi;

public interface IProduct
{
    // public ProductForList ()
    public Product ProductDetailsM(int id);
    public Product ProductDetailsC(int id,Cart cart);
    public void Add(Product product);
    public void Delete(int id);
    public void update(Product product);




}
