
namespace BlImplementation;
/// <summary>
/// implementoin of all the functions of cart
/// </summary>
internal class BoCart : BlApi.ICart
{

    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// implemention of function Add
    /// Adds a product to the cart and updates the quntity and the prices
    /// the function receives cart and id of a product
    /// returns cart
    public BO.Cart Add(BO.Cart cart, int id)
    {
        List<DO.Product?> Do_Products = new List<DO.Product?>();
        Do_Products = (List<DO.Product?>)dal.Product.GetAll();

        DO.Product prod = new DO.Product();
        prod = dal.Product.GetById(id);

        if (prod.InStock >= 1)
        {
            prod.InStock--;
            dal.Product.Update(prod);
        }
        else
            throw new BO.VariableIsSmallerThanZeroExeption("Out of stock");
        if (cart.Items == null)
        {
            BO.OrderItem orderItem = new BO.OrderItem
            {
                ProductID = id,
                ID = dal.OrderItem.GetAll().Last()?.OrderId + 1 ?? 0,
                Price = prod.Price,
                TotalPrice = prod.Price,
                Amount = prod.InStock =1,
                Name = prod.ProductName
            };
            List<BO.OrderItem> boOrderItems = new List<BO.OrderItem>();
            boOrderItems.Add(orderItem);
            cart.Items = boOrderItems!;
            cart.TotalPrice += prod.Price;
            return cart;
        }
        else 
        {
            //In case the member already exists
            if (cart.Items.Any(x => x?.ProductID == id))
            {
                var item = cart.Items.FirstOrDefault(item => item?.ProductID == id);
                int index = cart.Items.IndexOf(item);
                cart.Items[index]!.Amount++;
                cart.Items[index]!.TotalPrice += prod.Price;
                cart.TotalPrice += prod.Price;
                return cart;
            }
            else   // In case the item does not exist in the cart
            {
                BO.OrderItem ordItem = new BO.OrderItem
                {
                    Name = prod.ProductName,
                    ProductID = id,
                    ID = dal.OrderItem.GetAll().Last()?.OrderId + 1 ?? 0,
                    Price = prod.Price,
                    TotalPrice = prod.Price,
                    Amount = 1
                };
                cart.Items.Add(ordItem); // Add the order item to the list
                cart.TotalPrice += prod.Price; //Add this to the final amount
                return cart;
            }
        }
    }
    /// <summary>
    /// implemention of function confirmation
    /// the function receives cart
    /// Adds an order items updates the quntity and the prices
    /// checks the integrity of the data
    /// place an order and update the inventory
    /// </summary>
    public void Confirmation(BO.Cart cart)
    {
        bool check = false;
        if (cart.Items == null)
            throw new BO.VariableIsSmallerThanZeroExeption("the cart is empty");
        if (cart.CustomerName == null || cart.CustomerEmail == null || cart.CustomerAdress == null)
            throw new BO.VeriableNotExistException("one of the cart detaile is invalid (name/email/adress)");
        if (cart.Items == null)
            throw new BO.VeriableNotExistException("The shopping cart is empty");
        for (int i = 0; i < cart.CustomerEmail?.Length; i++)
        {
            if (cart.CustomerEmail[i] == '@')
            {
                check = true;
                break;
            }
        }
        if (check == false)
        {
            throw new BO.InvalidInputExeption("The input is not an email");
        }
        
        var Do_Products = (List<DO.Product?>)dal.Product.GetAll();

        var order = new DO.Order
        {
            OrderID = dal.Order.GetAll().LastOrDefault()?.OrderID + 1 ?? 0,
            CustomerName = cart.CustomerName,
            CustomerEmail = cart.CustomerEmail,
            CustomerAdress = cart.CustomerAdress,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryDate = null
        };
        int orderId = dal.Order.Add(order);

        foreach (var item in cart.Items)
        {
            var orderItem = new DO.OrderItem
            {
                OrderItemID = dal.OrderItem.GetAll().LastOrDefault()?.OrderItemID + 1 ?? 0,
                OrderId = orderId,
                ProductID = (int)item?.ProductID!,
                PriceItem = item.Price,
                Amount = item.Amount
            };
            int a = dal.OrderItem.Add(orderItem);
            var product = Do_Products.FirstOrDefault(p => p?.ProductID == item.ProductID);
            if (product != null)
            {
                var temp = new DO.Product
                {
                    ProductID = (int)product?.ProductID!,
                    Price = (double)product?.Price!,
                    Category = product?.Category,
                    InStock = (int)product?.InStock! - item.Amount,
                    ProductName = product?.ProductName
                };
                dal.Product.Update(temp);
            }
        }
    }
    /// <summary>
    /// implemention of function update
    /// the function updates the product amount 
    /// and the total price of the order item and the total price of the cart
    /// </summary>
    /// the function receives cart and id of a product and the product amount
    /// returns cart
    public BO.Cart Update(BO.Cart cart, int id, int amount)
    {
        if (cart.Items == null)
            throw new BO.VariableIsSmallerThanZeroExeption("The shopping cart is empty");

        if (amount < 0)  
            throw new BO.VariableIsSmallerThanZeroExeption("There quantity can't be negative");

        List<DO.Product?> Do_Products = new List<DO.Product?>();
        Do_Products = (List<DO.Product?>)dal.Product.GetAll();

        BO.OrderItem? item = cart.Items.Where(x => x?.ProductID == id).FirstOrDefault();
        if (item != null)
        {
            if (item.Amount < amount) //In case he wants to add
            {
                cart.TotalPrice -= item.TotalPrice;
                DO.Product product = dal.Product.GetById(id);
                if (product.InStock >= amount)
                {
                    item.Amount = amount;
                    item.TotalPrice = item.Price * amount;
                }
                else 
                    throw new BO.VariableIsSmallerThanZeroExeption("Out of stock");
                cart.TotalPrice += item.TotalPrice;
                return cart;
            }
            else
            {
                if (item.Amount > amount) //In case he wants to subtract
                {
                    if (amount == 0)
                    {
                        cart.TotalPrice -= item.TotalPrice;
                        cart.Items.Remove(item);
                    }
                    else
                    {
                        cart.TotalPrice -= item.TotalPrice;
                        item.Amount = amount;
                        item.TotalPrice = (item.Price * amount);
                        cart.TotalPrice += item.TotalPrice;
                    }
                }
                return cart;
            }
        }
        throw new BO.IdNotExistException("The Id Does Not Exist");
    }
}
