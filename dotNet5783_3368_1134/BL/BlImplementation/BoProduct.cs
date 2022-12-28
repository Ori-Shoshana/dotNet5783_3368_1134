
internal class BoProduct : BlApi.IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>                                
    /// implemention of function get products
    /// request products list from the data layer
    /// Build a product list based on the data
    /// returns the list
    public IEnumerable<BO.ProductForList?> GetProducts(Func<DO.Product?, bool>? func)
    {
        //List<BO.ProductForList?> productsForList = new List<BO.ProductForList?>();
        List<DO.Product?> products = new List<DO.Product?>();
        products = (List<DO.Product?>)dal.Product.GetAll();
        if (func != null)
        {
            List<BO.ProductForList> productsForList = (List<BO.ProductForList>)products
                  .Where(product => func(product))
                  .Select(product => new BO.ProductForList
                  {
                      ID = (int)product?.ProductID!,
                      Category = (BO.Enums.ProductCategory?)product?.Category,
                      Name = product?.ProductName,
                      Price = (int)product?.Price!
                  }).ToList();

            return productsForList;
        }
        else
        {
            List<BO.ProductForList> productsForList = (List<BO.ProductForList>)products
                .Select(product => new BO.ProductForList
                {
                    ID = (int)product?.ProductID!,
                    Category = (BO.Enums.ProductCategory?)product?.Category,
                    Name = product?.ProductName,
                    Price = (int)product?.Price!
                }).ToList();

            return productsForList;
        }
        
    }
    /// <summary>
    /// implemention of function product details (for manager)
    /// receives product id
    /// trying to request the product from the data layer 
    /// builds the product
    /// Throws exceptions when needed
    /// returns the product
    public BO.Product ProductDetailsM(int id)
    {
        DO.Product DoProduct = new DO.Product();
        BO.Product BoProduct = new BO.Product();
        if (id >= 0)
        {
            DoProduct = dal.Product.GetById(id);          
            BoProduct.ID = DoProduct.ProductID;
            BoProduct.Name = DoProduct.ProductName;
            BoProduct.Price = DoProduct.Price;
            BoProduct.Category = (BO.Enums.ProductCategory)DoProduct.Category!;
            BoProduct.InStock = DoProduct.InStock;
            return BoProduct;
        }
        throw new BO.VariableIsSmallerThanZeroExeption("Id is les than 0");
    }
    /// <summary>
    /// implemention of function product details (for customer)
    /// receives product id and cart
    /// trying to request the product from the data layer 
    /// builds  productItem
    /// Throws exceptions when needed
    /// returns productItem
    public BO.ProductItem ProductDetailsC(int id, BO.Cart cart)
    {
        if(cart.Items == null)
        {
            throw new BO.VariableIsNullExeption("The cart is empty");
        }
        if(id >= 0)
        {
            DO.Product product = new  DO.Product();
            product = dal.Product.GetById(id);
            BO.ProductItem productItem = new BO.ProductItem();
            productItem.ID = product.ProductID;
            productItem.Name = product.ProductName;
            productItem.Price = product.Price;
            productItem.Category = (BO.Enums.ProductCategory?)product.Category;
            if(product.InStock >0)
                productItem.InStock = true;
            else 
                productItem.InStock = false;
            productItem.Amount = 0;
      
            int totalAmount = cart.Items.Where(item => id == productItem.ID).Sum(item => item.Amount);
            productItem.Amount += totalAmount;

            return productItem;
        }
        throw new BO.VariableIsSmallerThanZeroExeption("the id is less than 0");
    }
    /// <summary>
    /// implemention of function add
    /// receives product
    /// checks data integrity
    /// adds the product to the data layer
    /// Throws exceptions when needed
    public void Add(BO.Product product)     
    {
        if (product.ID < 0) throw new BO.VariableIsSmallerThanZeroExeption("Id is less than 0");
        if (product.Name == null) throw new BO.VariableIsNullExeption("The name is null");
        if (product.Price <= 0) throw new BO.VariableIsSmallerThanZeroExeption("the price is less than 0");
        if (product.InStock < 0) throw new BO.VariableIsSmallerThanZeroExeption("the stock is less than 0");

        DO.Product DoProduct = new DO.Product();
        //if(product.ID !=null)
        DoProduct.ProductID = (int)product.ID;
        DoProduct.ProductName = product.Name;
        DoProduct.Price = product.Price;
        DoProduct.Category = (DO.Enums.productCategory?)product.Category;
        DoProduct.InStock = product.InStock;

        dal.Product.Add(DoProduct);
    }
    /// <summary>
    /// implemention of function delete 
    /// receives the product id
    /// checks if the product exits
    /// trying to delete him from the data layer
    /// Throws exceptions when needed
    public void Delete(int id)
    {
        List<DO.Product?> products = new List<DO.Product?>();
        products = (List<DO.Product?>)dal.Product.GetAll();

        //Go through all the existing products in the data layer
        if (products.Any(prod => prod?.ProductID==id))
        {
            dal.Product.Delete(id); //Delete the product in the data layer
        }
        else//If we did not delete = the member did not exist, we will throw an exception
            throw new BO.VeriableNotExistException("The product does not exist");
    }
    /// <summary>
    /// implemention of function update
    /// receives product 
    /// checks integrity
    /// updates the product in the data layer
    /// Throws exceptions when needed
    public void Update(BO.Product product)
    {
        if (product.ID < 0) throw new BO.VariableIsSmallerThanZeroExeption("Id is less than 0");
        if (product.Name == null) throw new BO.VariableIsNullExeption("The name is null");
        if (product.Price < 0) throw new BO.VariableIsSmallerThanZeroExeption("the price is less than 0");
        if (product.InStock < 0) throw new BO.VariableIsSmallerThanZeroExeption("the stock is less than 0");

        DO.Product DoProduct = new DO.Product();
        //if(product.ID != null)
        DoProduct.ProductID = (int)product.ID;
        DoProduct.ProductName = product.Name;
        DoProduct.Price = product.Price;
        DoProduct.Category = (DO.Enums.productCategory?)product.Category;
        DoProduct.InStock = product.InStock;

        try
        {
            dal.Product.Update(DoProduct);
        }
        catch(BO.VeriableNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }
}
