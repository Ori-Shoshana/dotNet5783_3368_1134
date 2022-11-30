using BlApi;
using BO;
using DalApi;
using DO;
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
    //The function adds an item to the shopping cart
    public BO.Cart Add(BO.Cart cart, int id)
    {

        List<DO.Product> Do_Products = new List<DO.Product>();
        Do_Products = (List<DO.Product>)dal.Product.GetAll();
        if (cart.Items == null)
        {
            foreach (var product in Do_Products)
            {
                if (product.ProductID == id)
                {
                    BO.OrderItem orderItem = new BO.OrderItem();
                    orderItem.ProductID = id;
                    orderItem.ID = dal.OrderItem.GetAll().ElementAt(dal.OrderItem.GetAll().Count() - 1).OrderId + 1;
                    orderItem.Price = product.Price;
                    orderItem.TotalPrice = product.Price;
                    if (product.InStock >= 1) //Check if the product is in stock
                    {
                        orderItem.Amount = 1;
                    }
                    else throw new VariableIsSmallerThanZeroExeption("Out of stock");
                    orderItem.Name = product.ProductName;
                    List<BO.OrderItem> boOrderItems = new List<BO.OrderItem>();
                    boOrderItems.Add(orderItem);
                    cart.Items = boOrderItems;
                    //cart.Items.Add(orderItem); // Add the order item to the list
                    cart.TotalPrice += orderItem.TotalPrice; //Add this to the final amount
                    return cart;
                }

            }
            throw new VeriableNotExistException("The Id Does Not Exist");    
        }
        //In case the member already exists
        foreach (var item in cart.Items)
        {
            if (item.ID == id)
            {
                foreach (var product in Do_Products)
                {
                    if (product.InStock > item.Amount)
                    {
                        item.TotalPrice = (++item.Amount) * (item.Price);
                        cart.TotalPrice += item.Price;
                    }
                    else throw new VariableIsSmallerThanZeroExeption("Out of stock");
                }
                return cart;
            }
        }
        // In case the item does not exist in the cart
        foreach (var product in Do_Products)
        {
            if (product.ProductID == id)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ProductID = id;
                orderItem.ID = dal.OrderItem.GetAll().ElementAt(dal.OrderItem.GetAll().Count() - 1).OrderItemID + 1;
                orderItem.Price = product.Price;
                orderItem.TotalPrice = product.Price;
                if (product.InStock >= 1) //Check if the product is in stock
                {
                    orderItem.Amount = 1;
                }
                else throw new VariableIsSmallerThanZeroExeption("Out of stock");
                orderItem.Name = product.ProductName;

                cart.Items.Add(orderItem); // Add the order item to the list
                cart.TotalPrice += orderItem.TotalPrice; //Add this to the final amount
            }

        }
        return cart;
    }

    public void Confirmation(BO.Cart C)
    {
        BO.Product? prod;
        foreach (var item in C.Items)
        {
            productDO = (dal.Product.GetById(item.ID));
            if (productDO.InStock < item.Amount)
                throw new VeriableNotExistException("{item.productName} out of stock");
            if (C.CustomerAdress == "")
                throw new VeriableNotExistException("the address is missing");
            if (C.CustomerName == "")
                throw new VeriableNotExistException("the name is missing");
            if (C.CustomerEmail == "")
                throw new VeriableNotExistException("the email is missing");
            if (!C.CustomerAdress.EndsWith("@gmail.com"))
                throw new VeriableNotExistException("email");
            if (item.Amount < 0)
                throw new InvalidInputExeption($"Error! quantity for {item.Name} is invalid");
        }
        int totalAmount = 0;
        foreach (var item in C.Items)
            totalAmount += item.Amount;
        if (totalAmount == 0)
            throw new VeriableNotExistException("can not confirm a cart without items");
        DO.Order NewOrderDO = new DO.Order();
        NewOrderDO.OrderDate = DateTime.Now;
        NewOrderDO.ShipDate = DateTime.MinValue;
        NewOrderDO.DeliveryDate = DateTime.MinValue;
        NewOrderDO.CustomerAdress = C.CustomerAdress;
        NewOrderDO.CustomerName = C.CustomerName;
        NewOrderDO.CustomerEmail = C.CustomerEmail;
        int IdOrder = dal.Order.Add(NewOrderDO);
        int i = 0;
        foreach (var item in C.Items)
        {
            DO.OrderItem item1 = new DO.OrderItem();
            List<DO.Order> order = new List<DO.Order>();
            item1.OrderItemID = item1.OrderItemID;
            item1.OrderId = order[i].OrderID;
            item1.ProductID = item.ProductID;
            item1.PriceItem = item.Price;
            item1.Amount = item.Amount;
            dal.OrderItem.Add(item1);
            i++;
        }
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

}
