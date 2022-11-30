﻿using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Immutable;
using System.Globalization;
using System.Net.Mail;
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
        throw new VeriableNotExistException("The Id Does Not Exist");
    }

    public void Confirmation(BO.Cart cart)
    {
        if (cart.Items == null)
        {
            throw new VeriableNotExistException("The shopping cart is empty");
        }
        List<DO.Product> Do_Products = new List<DO.Product>();
        Do_Products = (List<DO.Product>)dal.Product.GetAll();
        foreach (var item in cart.Items) //Check that all data is correct
        {
            if (item.Amount <= 0 || cart.CustomerName == null || cart.CustomerEmail == null || cart.CustomerAdress == null || !IsValid(cart.CustomerEmail))
            {
                throw new VeriableNotExistException("Input error");
            }
            foreach (var product in Do_Products)
            {
                if (item.ProductID == product.ProductID)
                {
                    if (item.Amount > product.InStock)  //check that the quantity is less than the quantity in stock
                        throw new VariableIsSmallerThanZeroExeption("Out of stock");
                }
            }
        }

        DO.Order order = new DO.Order();
        order.OrderID = dal.Order.GetAll().ElementAt(dal.Order.GetAll().Count() - 1).OrderID + 1;
        order.CustomerName = cart.CustomerName;
        order.CustomerEmail = cart.CustomerEmail;
        order.CustomerAdress = cart.CustomerAdress;
        order.OrderDate = DateTime.Now;
        order.ShipDate = null;
        order.DeliveryDate = null;
        int orderId = dal.Order.Add(order); // Add an order to the data layer

        foreach (var item in cart.Items)
        {
            DO.OrderItem orderItem = new DO.OrderItem();
            orderItem.OrderItemID = dal.OrderItem.GetAll().ElementAt(dal.OrderItem.GetAll().Count() - 1).OrderItemID + 1;
            orderItem.OrderId = orderId;
            orderItem.ProductID = item.ProductID;
            orderItem.PriceItem = item.Price;
            orderItem.Amount = item.Amount;
            int k = dal.OrderItem.Add(orderItem); //We will add list details to the data layer
            foreach (var product in Do_Products)
            {
                if (product.ProductID == item.ProductID)
                {
                    DO.Product temp = new DO.Product();
                    temp.ProductID = product.ProductID;
                    temp.Price = product.Price;
                    temp.Category = product.Category;
                    temp.InStock = product.InStock - item.Amount;
                    temp.ProductName = product.ProductName;
                    dal.Product.Update(temp); //Updates the stock quantity in the data layer
                    break;
                }
            }
        }
    }

    public BO.Cart Update(BO.Cart cart, int id, int amount)
    {
        if (cart.Items == null)
        {
            throw new VariableIsSmallerThanZeroExeption("The shopping cart is empty");
        }
        if (amount < 0)  //check for correctness
        {
            throw new VariableIsSmallerThanZeroExeption("There is no such thing as a negative quantity");
        }
        List<DO.Product> Do_Products = new List<DO.Product>();
        Do_Products = (List<DO.Product>)dal.Product.GetAll();
        foreach (var item in cart.Items)
        {
            if (item.ProductID == id)
            {
                if (item.Amount < amount) //In case he wants to add
                {
                    cart.TotalPrice -= item.TotalPrice;
                    foreach (var product in Do_Products)
                    {
                        if (product.ProductID == id)
                        {
                            if (product.InStock >= amount)
                            {
                                item.Amount = amount;
                                item.TotalPrice = item.Price * amount;
                            }
                            else throw new VariableIsSmallerThanZeroExeption("Out of stock");
                        }
                    }
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
        }
        throw new VeriableNotExistException("The Id Does Not Exist");
    }

    private static bool IsValid(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }
}
