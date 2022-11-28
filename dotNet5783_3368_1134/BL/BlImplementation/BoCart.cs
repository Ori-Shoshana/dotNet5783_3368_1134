using BlApi;
using BO;
using DalApi;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using static BO.Enums;
namespace BlImplementation;

internal class BoCart : ICart
{
    private IDal dal = new Dal.DalList();
    BO.OrderItem ordItemBO = new BO.OrderItem();
    DO.Product productDO = new DO.Product();
    public BO.Cart Add(BO.Cart C, int id)
    {
        IEnumerable<DO.Product> DoProducts = dal.Product.GetAll();
        BO.OrderItem? OrderItem;
        foreach (var x in C.Items)
        {
            if (x.ProductID == id)
            {
                productDO = dal.Product.GetById(x.ProductID);
                if (productDO.InStock > 0)
                {
                    x.Amount++;
                    x.TotalPrice += productDO.Price;
                    C.TotalPrice += productDO.Price;
                    return C;
                }
                else
                    throw new SoldOutExeption("product sold out");
            }
        }
        productDO = dal.Product.GetById(id);
        if (productDO.InStock > 0)
        {
            OrderItem = new BO.OrderItem()
            {
                Price = (double)productDO.Price,
                Name = productDO.ProductName,
                ProductID = id,
                Amount = 1,
                TotalPrice = productDO.Price
            };
            List<BO.OrderItem> Items = C.Items?.ToList() ?? new List<BO.OrderItem>();
            Items.Add(OrderItem);
            C.Items = Items;
            C.TotalPrice += OrderItem.Price;
            return C;
        }
        else
            throw new SoldOutExeption("product sold out");
    }

    public void Confirmation(BO.Cart C)
    {
        BO.Product? prod;
        foreach (var item in C.Items)
        {
            productDO = (dal.Product.GetById(item.ID));
            if (productDO.InStock < item.Amount)
                throw new ItemMissingException("{item.productName} out of stock");
            if (C.CustomerAdress == "")
                throw new DataMissingException("the address is missing");
            if (C.CustomerName == "")
                throw new DataMissingException("the name is missing");
            if (C.CustomerEmail == "")
                throw new DataMissingException("the email is missing");
            if (!C.CustomerAdress.EndsWith("@gmail.com"))
                throw new DataMissingException("email");
            if (item.Amount < 0)
                throw new InvalidInputBO($"Error! quantity for {item.Name} is invalid");
        }
        int totalAmount = 0;
        foreach (var item in C.Items)
            totalAmount += item.Amount;
        if (totalAmount == 0)
            throw new noItemsInTheCart("can not confirm a cart without items");
        DO.Order NewOrderDO = new DO.Order();
        NewOrderDO.OrderDate = DateTime.Now;
        NewOrderDO.ShipDate = DateTime.MinValue;
        NewOrderDO.DeliveryDate = DateTime.MinValue;
        NewOrderDO.CustomerAdress = C.CustomerAdress;
        NewOrderDO.CustomerName = C.CustomerName;
        NewOrderDO.CustomerEmail = C.CustomerEmail;
        int IdOrder = dal.Order.Add(NewOrderDO);
        foreach (var item in C.Items)
            dal.OrderItem.Add(item);////////////אין לי שמץ
    }

    public BO.Cart Update(BO.Cart C, int id, int amount)
    {
        BO.OrderItem? ordItem = C.Items?.FirstOrDefault(Item => Item.ID == id);
        if (ordItem?.Amount < amount && amount > 0)
        {
            for (int i = ordItem.Amount; i < amount; i++)
                C = Add(C, id);
        }
        else if (ordItem?.Amount > amount && amount > 0)
        {
            List<BO.OrderItem> ordItems = C.Items?.ToList() ?? new List<BO.OrderItem>();
            ordItem = ordItems.Find(Item => Item.ID == id);
            ordItem.Amount = amount;
            ordItem.TotalPrice = ordItem.Price * amount;
            C.Items = ordItems;
            C.TotalPrice -= (ordItem.Amount - amount) * ordItem.Price;
        }
        else if (amount == 0)
        {
            List<BO.OrderItem> ordItems = C.Items?.ToList() ?? new List<BO.OrderItem>();
            C.TotalPrice -= ordItem.Amount * ordItem.Price;
            ordItems.Remove(ordItem);
            C.Items = ordItems;
        }
        return C;
    }

    //public static DO.Product  BO_TO_DO(BO.Product prod)
    //{
    //    DO.Product product = new DO.Product()
    //    {
    //        ProductID = (int)prod.ID,
    //        ProductName = prod.Name,
    //        Category = (DO.Enums.productCategory)prod.Category,
    //        Price = prod.Price,
    //        InStock = prod.InStock
    //    };
    //    return product;
    //}
}
