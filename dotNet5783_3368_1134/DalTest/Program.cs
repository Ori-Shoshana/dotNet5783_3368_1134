using Dal;
using DalApi;
using DO;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static DO.Enums;

/// <summary>
/// Main - testing that the structs and the operations on them are actually works
/// </summary>
internal class Program
{


    public static IDal dal = new DalList();
    static void Main(string[] args)
    {
         
        Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 for product\n " +
                        "Enter 2 for order\n " +
                        "Enter 3 for order item:");


        bool check;
        int tempInt;
        double tempDouble;

        int option;
        check = int.TryParse(Console.ReadLine(), out option);
        while (option != 0) // 0 ends the program
        {
            
            switch (option)
            {
            //***************************** all the options for product *************************//
                case 1: //product
                   
                    Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 to add a prodoct\n " +
                        "Enter 2 to print product\n " +
                        "Enter 3 to see list of products\n " +
                        "Enter 4 to update product\n " +
                        "Enter 5 to delete product  ");

                    int option1;
                    check = int.TryParse(Console.ReadLine(), out option1);
                  
                    if (option1 == 0)
                        break;
                   
                    switch (option1)
                    {                 
                        case 1://add product
                         
                            Product TestAddProduct = new Product();
                            Console.WriteLine("Add a Id");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestAddProduct.ProductID = tempInt;
                            Console.WriteLine("Add Prodoct name");
                            TestAddProduct.ProductName = Console.ReadLine();
                            Console.WriteLine("Add Prodoct Category");
                            productCategory categoryPro;
                            productCategory.TryParse(Console.ReadLine(), out categoryPro);
                            TestAddProduct.Category = categoryPro;
                            //TestAddProduct.Category = Console.ReadLine();
                            Console.WriteLine("Add product price");
                            check = double.TryParse(Console.ReadLine(), out tempDouble);
                            TestAddProduct.Price = tempDouble;
                            Console.WriteLine("Add amount in stock");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestAddProduct.InStock = tempInt;
                            try
                            {
                                dal.Product.Add(TestAddProduct);
                            }
                            catch (IdAlreadyExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 2://print product

                            Product GetProduct = new Product(); 
                            Console.WriteLine("type the id to get");
                            int getId = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                GetProduct = dal.Product.GetById(getId);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            
                            Console.WriteLine(GetProduct);
                 
                    break;

                        case 3://print all prodocts


                            int counter = 0;
                            List<Product?> products = new List<Product?>(dal.Product.GetAll());
                            foreach (Product product in dal.Product.GetAll())
                            {
                                if (counter == dal.Product.ListLength())
                                {
                                    break;
                                }
                                counter++;
                                Console.WriteLine(product);
                            }
                            break;

                        case 4://update
                            Product TestUpdateProduct = new Product();
                            Console.WriteLine("Add a Id");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestUpdateProduct.ProductID = tempInt;
                            Console.WriteLine("Add Prodoct name");

                            TestUpdateProduct.ProductName = Console.ReadLine();


                            Console.WriteLine("Add Prodoct Category");
                            productCategory categoryPr;
                            productCategory.TryParse(Console.ReadLine(), out categoryPr);
                            TestUpdateProduct.Category = categoryPr;
                            //TestUpdateProduct.Category = Console.ReadLine();
                            Console.WriteLine("Add product price");
                            check = double.TryParse(Console.ReadLine(), out tempDouble);
                            TestUpdateProduct.Price = tempDouble;
                            Console.WriteLine("Add amount in stock");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestUpdateProduct.InStock = tempInt;
                            try
                            {
                                dal.Product.Update(TestUpdateProduct);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 5://delete

                            Console.WriteLine("Enter the ID of the prodoct to delete");
                            int ID = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                dal.Product.Delete(ID);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    break;
                //**************************** all the options for order ************************//
                case 2://Order

                    Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 to add a order\n " +
                        "Enter 2 to print order\n " +
                        "Enter 3 to see list of order\n " +
                        "Enter 4 to update order\n " +
                        "Enter 5 to delet order  ");

                    int option2;
                    check = int.TryParse(Console.ReadLine(), out option2);
                    
                    if (option2 == 0)
                        break;

                    switch (option2)
                    {
                        case 1://add

                            Order TestAddOrder = new Order();
                            Console.WriteLine("Add order Private ID");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestAddOrder.OrderID = tempInt;
                            Console.WriteLine("Add order customer name");
                            TestAddOrder.CustomerName = Console.ReadLine();
                            Console.WriteLine("Add order customer email");
                            TestAddOrder.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("Add order customer address");
                            TestAddOrder.CustomerAdress = Console.ReadLine();
                            Console.WriteLine("Add Order date");
                            TestAddOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Add ship date");
                            TestAddOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Add delivory date");
                            TestAddOrder.DeliveryDate = Convert.ToDateTime(Console.ReadLine());
                            try
                            {
                                dal.Order.Add(TestAddOrder);
                            }
                            catch (IdAlreadyExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 2://print

                            Order GetOrder = new Order();
                            Console.WriteLine("type the id to get");
                            int getId = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                GetOrder = dal.Order.GetById(getId);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            Console.WriteLine(GetOrder);
                            break;

                        case 3://print all orders

                            List<Order?> orders = new List<Order?>(dal.Order.GetAll());
                            int counter = 0;
                            foreach(Order order in orders)
                            { 
                                   
                                if(counter == dal.Order.ListLength())
                                {
                                    break;
                                }
                                counter++;
                                Console.WriteLine(order);
                            }
                            break;

                        case 4://update

                            Order TestupdateOrder = new Order();
                            Console.WriteLine("Add order Private ID");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestupdateOrder.OrderID = tempInt;
                            Console.WriteLine("Add order customer name");
                            TestupdateOrder.CustomerName = Console.ReadLine();
                            Console.WriteLine("Add order customer email");
                            TestupdateOrder.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("Add order customer address");
                            TestupdateOrder.CustomerAdress = Console.ReadLine();
                            Console.WriteLine("Add Order date");
                            TestupdateOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Add ship date");
                            TestupdateOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Add delivory date");
                            TestupdateOrder.DeliveryDate = Convert.ToDateTime(Console.ReadLine());
                            try
                            {
                                dal.Order.Update(TestupdateOrder);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 5://delete
                            Console.WriteLine("Enter the ID of the order to delete");
                            int ID = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                dal.Order.Delete(ID);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    break;
                //************************* all the options for order item **********************//
                case 3: //order Item

                    Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 to add a orderItem\n " +
                        "Enter 2 to print orderItem\n " +
                        "Enter 3 to see list of orderItem\n " +
                        "Enter 4 to update orderItem\n " +
                        "Enter 5 to delet orderItem  ");
                    
                    int option3;
                    check = int.TryParse(Console.ReadLine(), out option3);
                   
                    if (option3 == 0)
                        break;

                    switch (option3) //OrderItem
                    {
                        case 1: //add

                            DO.OrderItem TestAddOrderItem = new DO.OrderItem();
                            Console.WriteLine("Add Private ID");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestAddOrderItem.OrderItemID = tempInt;
                            Console.WriteLine("Add order id");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestAddOrderItem.OrderId = tempInt;
                            Console.WriteLine("Add product id");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestAddOrderItem.ProductID = tempInt;
                            Console.WriteLine("Add price item");
                            check = double.TryParse(Console.ReadLine(), out tempDouble);
                            TestAddOrderItem.PriceItem = tempDouble;
                            Console.WriteLine("Add amount");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestAddOrderItem.Amount = tempInt;
                            try
                            {
                                dal.OrderItem.Add(TestAddOrderItem);
                            }
                            catch(IdAlreadyExistException ex)
                            {
                                    Console.WriteLine(ex);
                            }
                            break;

                        case 2://print

                            OrderItem GetOrderItem = new OrderItem();
                            Console.WriteLine("type the id to get");
                            int getId = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                GetOrderItem = dal.OrderItem.GetById(getId);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            Console.WriteLine(GetOrderItem);
                            break;

                        case 3://print OrderItem

                            List<OrderItem?> orderItems = new List<OrderItem?>(dal.OrderItem.GetAll());
                            int counter = 0;
                            foreach (OrderItem orderItem in orderItems)
                            {
                               
                                if (counter == dal.OrderItem.ListLength())
                                {
                                    break;
                                }
                                counter++;
                                Console.WriteLine(orderItem);
                            }
                            break;

                        case 4://update

                            OrderItem TestUpdateOrderItem = new OrderItem();
                            Console.WriteLine("Add Private ID");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestUpdateOrderItem.OrderItemID = tempInt;
                            Console.WriteLine("Add order id");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestUpdateOrderItem.OrderId = tempInt;
                            Console.WriteLine("Add product id");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestUpdateOrderItem.ProductID = tempInt;
                            Console.WriteLine("Add price item");
                            check = double.TryParse(Console.ReadLine(), out tempDouble);
                            TestUpdateOrderItem.PriceItem = tempDouble;
                            Console.WriteLine("Add amount");
                            check = int.TryParse(Console.ReadLine(), out tempInt);
                            TestUpdateOrderItem.Amount = tempInt;
                            try
                            {
                                dal.OrderItem.Update(TestUpdateOrderItem);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;

                        case 5://delete
                            Console.WriteLine("Enter the ID of the order item to delete");
                            int ID = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                dal.OrderItem.Delete(ID);
                            }
                            catch (IdNotExistException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    break;
            }
        //******************************************************************************//
            Console.WriteLine("Enter 0 to end\n " +
                "Enter 1 for product\n " +
                "Enter 2 for order\n " +
                "Enter 3 for order item:");
            option = Convert.ToInt32(Console.ReadLine());
        }
    }
}