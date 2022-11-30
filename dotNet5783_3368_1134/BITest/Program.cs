using BlApi;
using BO;
using DalApi;
using Dal; 
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Metrics;
using BlImplementation;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Drawing;
using static DO.Enums;

namespace BITest;

internal class Program
{
  
    enum Option {Exit =0, Cart = 1, Order = 2, Product = 3 };
    public static IBl bl = new BlImplementation.BL();
    static BO.Cart cart = new Cart();


    static void Main(string[] args)
    {
        Console.WriteLine("Enter customer name");
        cart.CustomerName = Console.ReadLine();
        Console.WriteLine("Enter customer email");
        cart.CustomerEmail = Console.ReadLine();
        Console.WriteLine("Enter customer address");
        cart.CustomerAdress = Console.ReadLine();

        List<BO.ProductForList> products = new List<BO.ProductForList>();
        products = (List<ProductForList>)bl.Product.GetProducts();
        foreach (var product1 in products)
        {
            Console.WriteLine(product1.ToString());
        }


        Console.WriteLine("Enter 0 to exit \n" +
           "Enter 1 for Cart \n" +
           "Enter 2 for Order \n" +
           "Enter 3 for product.\n" +
           "please enter a choice\n");

        
        Option option;
        int option2;
        int tempInt=0;
        
        Option.TryParse(Console.ReadLine(), out option);

        
        do
        {
            switch (option)
            {
                case Option.Exit:
                return;
//********************** actions fo cart ******************************//
                case Option.Cart:
                    Console.WriteLine("Enter 0 to pick a different option\n " +
                        "Enter 1 to add a order\n " +
                        "Enter 2 to confirm order\n " +
                        "Enter 3 to update order\n ");
                    int.TryParse(Console.ReadLine(), out option2);
                    switch(option2)
                    {
                        case 0://return
                            break;

                        case 1://add order item to cart
                            int tempID = 0;
                            Console.WriteLine("Enter a ID");
                            int.TryParse(Console.ReadLine(), out tempID);
                            try
                            {
                                Console.WriteLine(bl.Cart.Add(cart, tempInt).ToString());
                            }
                            catch (VariableIsSmallerThanZeroExeption ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 2://confirm a order
                            int count1 = 0;
                            double total1 = 0;
                            Cart cart1 = new Cart();
                            List<BO.OrderItem> orderItems1 = new List<BO.OrderItem>();
                            BO.OrderItem item1 = new BO.OrderItem();

                            Console.WriteLine("Add customer name");
                            cart1.CustomerName = Console.ReadLine();
                            Console.WriteLine("Add customer email");
                            cart1.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("Add customer address");
                            cart1.CustomerAdress = Console.ReadLine();
                            Console.WriteLine("how many Items do you want to add to the Order?");
                            int.TryParse(Console.ReadLine(), out count1);
                            for (int i = 0; i < count1; i++)
                            {
                                Console.WriteLine("add a ID, Name, Product ID, Price, Amount");
                                int.TryParse(Console.ReadLine(), out tempInt);
                                item1.ID = tempInt;
                                item1.Name = Console.ReadLine();
                                int.TryParse(Console.ReadLine(), out tempInt);
                                item1.ProductID = tempInt;
                                int.TryParse(Console.ReadLine(), out tempInt);
                                item1.Price = tempInt;
                                int.TryParse(Console.ReadLine(), out tempInt);
                                item1.Amount = tempInt;
                                item1.TotalPrice = item1.Amount * item1.Price;
                                total1 += item1.Amount * item1.Price;
                                orderItems1.Add(item1);
                            }
                            cart1.Items = orderItems1;
                            cart1.TotalPrice = total1;
                            try
                            {
                                bl.Cart.Confirmation(cart1);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (VeriableNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (InvalidInputExeption ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 3://update order in cart
                            int count2 = 0;
                            int tempID2 = 0;
                            double total2 = 0;
                            int amount = 0;
                            Cart cart2 = new Cart();
                            List<BO.OrderItem> orderItems2 = new List<BO.OrderItem>();
                            BO.OrderItem item2 = new BO.OrderItem();

                            Console.WriteLine("type a ID");
                            int.TryParse(Console.ReadLine(), out tempID2);

                            Console.WriteLine("Add customer name");
                            cart2.CustomerName = Console.ReadLine();
                            Console.WriteLine("Add customer email");
                            cart2.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("Add customer address");
                            cart2.CustomerAdress = Console.ReadLine();
                            Console.WriteLine("how many Items do you want to add to the Order?");
                            int.TryParse(Console.ReadLine(), out count2);
                            for (int i = 0; i < count2; i++)
                            {
                                Console.WriteLine("add a ID, Name, Product ID, Price, Amount");
                                int.TryParse(Console.ReadLine(), out tempInt);
                                item2.ID = tempInt;
                                item2.Name = Console.ReadLine();
                                int.TryParse(Console.ReadLine(), out tempInt);
                                item2.ProductID = tempInt;
                                int.TryParse(Console.ReadLine(), out tempInt);
                                item2.Price = tempInt;
                                int.TryParse(Console.ReadLine(), out tempInt);
                                item2.Amount = tempInt;
                                item2.TotalPrice = item2.Amount * item2.Price;
                                amount += item2.Amount;
                                total2 += item2.Amount * item2.Price;
                                orderItems2.Add(item2);
                            }
                            cart2.Items = orderItems2;
                            cart2.TotalPrice = total2;
                            try
                            {
                                bl.Cart.Update(cart2, tempInt,amount);
                            }
                            catch (VariableIsSmallerThanZeroExeption ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    break;

//********************** actions fo order ******************************//
                case Option.Order:
                    Console.WriteLine("Enter 0 to pick a different option\n " +
                        "Enter 1 to see list of Orders\n " +
                        "Enter 2 to print order Details\n " +
                        "Enter 3 to update order delivrary\n " +
                        "Enter 4 to update order shiping\n " +
                        "Enter 5 to see the order tracking"
                        );
                    int.TryParse(Console.ReadLine(), out option2);
                    switch (option2)
                    {
                        case 0:
                            break;

                        case 1://prints the last product only
                            List<OrderForList> list = new List<OrderForList>();
                           
                                foreach (OrderForList item in (List<OrderForList>)bl.Order.GetOrders())
                                {
                                    Console.WriteLine(item.ToString());
                                }
                       
                            break;

                        case 2://print by id
                            Console.WriteLine("Enter the ID of the order Details to print");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            BO.Order order = new BO.Order();
                            try
                            {
                                order = bl.Order.OrderDetails(tempInt);
                                Console.WriteLine(order.ToString());
                            }
                            catch(IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (VariableIsSmallerThanZeroExeption ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 3://update the dilevery
                            Console.WriteLine("Enter the id to update for the delevary");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            try
                            {
                                bl.Order.UpdateDelivery(tempInt);
                            }
                            catch(VeriableNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 4://update the shiping
                            Console.WriteLine("Enter the id to update for the shiping");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            try
                            {
                                bl.Order.ShippingUpdate(tempInt);
                            }
                            catch (VeriableNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            } 
                            break;

                        case 5://reurns the trackng update of the order
                            Console.WriteLine("Enter the id to update for to see the order tracking");
                            BO.OrderTracking orderTracking = new BO.OrderTracking();
                            int.TryParse(Console.ReadLine(), out tempInt);
                            orderTracking = bl.Order.Track(tempInt);
                            Console.WriteLine("ID:" + orderTracking.ID);
                            Console.WriteLine("status:" + orderTracking.Status);
                            foreach(var t in orderTracking.Tracking)
                            {
                                Console.WriteLine("Tracking:" + t);
                            }
                            
                            break;
                    }
                    break;

//********************** actions fo product ******************************//
                case Option.Product:
                    Console.WriteLine("Enter 0 to pick a different option\n " +
                        "Enter 1 to see list of products\n " +
                        "Enter 2 to print product by the id\n " +
                        "Enter 3 to see the product by id and cart\n " +
                        "Enter 4 to add a prodoct\n " +
                        "Enter 5 to delete product\n  " +
                        "Enter 6 to update product" 
                        );
                    int.TryParse(Console.ReadLine(), out option2);
                    switch (option2)
                    {
                        case 0:
                            break;

                        case 1://prints all the products
                            
                            products = (List<ProductForList>)bl.Product.GetProducts();
                            foreach (var product1 in products)
                            {
                                Console.WriteLine(product1.ToString());
                            }
                            break;

                        case 2://prints the product by the id
                            Console.WriteLine("Enter the id of the product that you want to print");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            try
                            {
                                Console.WriteLine(bl.Product.ProductDetailsM(tempInt).ToString());
                            }
                            catch(IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (VariableIsSmallerThanZeroExeption ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 3://prints the product by the id and the givin cart
                            Console.WriteLine("Enter the id of the product item that you want to print");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            BO.ProductItem productItem = new ProductItem();
                            List<BO.OrderItem> orderItems = new List<BO.OrderItem>();
                            try
                            {
                                Console.WriteLine(bl.Product.ProductDetailsC(tempInt,cart).ToString());
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (VariableIsSmallerThanZeroExeption ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (IdAlreadyExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 4:
                            productCategory categoryPro;
                            BO.Product product = new BO.Product(); 
                            Console.WriteLine("Enter ID to add");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            product.ID = tempInt;
                            Console.WriteLine("Enter Name to add");
                            product.Name = Console.ReadLine();
                            Console.WriteLine("Enter the price of the item");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            product.Price = tempInt;
                            Console.WriteLine("Enter the category of the item");
                            productCategory.TryParse(Console.ReadLine(), out categoryPro);
                            product.Category = (BO.Enums.ProductCategory)categoryPro;
                            Console.WriteLine("Enter the amount in stock of the item");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            product.InStock = tempInt;
                            try
                            {
                                bl.Product.Add(product);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex);
                            }

                            break;

                        case 5:
                            Console.WriteLine("Enter ID of product to delete");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            try
                            {
                                bl.Product.Delete(tempInt);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 6:
                            productCategory categoryPro1;
                            BO.Product product2 = new BO.Product();
                            Console.WriteLine("Enter ID to update");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            product2.ID = tempInt;
                            Console.WriteLine("Enter Name to update");
                            product2.Name = Console.ReadLine();
                            Console.WriteLine("Enter the price of the item");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            product2.Price = tempInt;
                            Console.WriteLine("Enter the category of the item");
                            productCategory.TryParse(Console.ReadLine(), out categoryPro);
                            product2.Category = (BO.Enums.ProductCategory)categoryPro;
                            Console.WriteLine("Enter the amount in stock of the item");
                            int.TryParse(Console.ReadLine(), out tempInt);
                            product2.InStock = tempInt;
                            try
                            {
                                bl.Product.Update(product2);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    break;
            }

            Console.WriteLine("Enter 0 to exit \n" +
           "Enter 1 to Cart \n" +
           "Enter 2 to Order \n" +
           "Enter 3 to product.\n" +
           "please enter a choice\n");
            Option.TryParse(Console.ReadLine(), out option);
        } while (option != 0);
    }
}