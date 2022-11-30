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
                                Console.WriteLine(bl.Cart.Add(cart, tempID).ToString());
                            }
                            catch (VariableIsSmallerThanZeroExeption ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (VeriableNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            



                            break;

                        case 2://confirm a order
                            try
                            {
                                bl.Cart.Confirmation(cart);
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
                            int amount = 0;
                            Console.WriteLine("Entet id and the amount of the order to chsnge");
                            int.TryParse(Console.ReadLine(), out tempID);
                            int.TryParse(Console.ReadLine(), out amount);

                            try
                            {
                                bl.Cart.Update(cart, tempID, amount);
                            }
                            catch (VariableIsSmallerThanZeroExeption ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex.Message);
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