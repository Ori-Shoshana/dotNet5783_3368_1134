﻿using Dal;
using DO;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Linq.Expressions;




internal class Project
{
    static void Main(string[] args)
    {
         
        Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 for product\n " +
                        "Enter 2 for order\n " +
                        "Enter 3 for order item:");

        DalProduct TestProduct = new DalProduct();
        DalOrder TestOrder = new DalOrder();
        DalOrderItem TestOrderItem = new DalOrderItem();

        bool check;

        int option;
        check = int.TryParse(Console.ReadLine(), out option);
        while (option != 0)
        {
            
            switch (option)
            {
 //************************************************************************************************//
                case 1: //product
                    Console.WriteLine("Enter 0 to end\n " +
                        "Enter 1 to add a prodoct\n " +
                        "Enter 2 to print product\n " +
                        "Enter 3 to see list of products\n " +
                        "Enter 4 to update product\n " +
                        "Enter 5 to delet product  ");
                    int option1;
                    check = int.TryParse(Console.ReadLine(), out option1);
                    if (option1 == 0)
                        break;
                    switch (option1)
                    {

                        case 1://add product
                            Product TestAddProduct = new Product();
                            Console.WriteLine("Add a Id");
                            TestAddProduct.PrivateId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Add Prodoct name");
                            TestAddProduct.ProdoctName = Console.ReadLine();
                            Console.WriteLine("Add Prodoct Category");
                            TestAddProduct.Category = Console.ReadLine();
                            Console.WriteLine("Add product price");
                            TestAddProduct.Price = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Add amount in stock");
                            TestAddProduct.InStock = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                TestProduct.AddProduct(TestAddProduct);
                            }
                            catch (Exception ex)
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
                                    GetProduct = TestProduct.Get(getId);
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            Console.WriteLine(GetProduct.PrivateId);
                            Console.WriteLine(GetProduct.ProdoctName);
                            Console.WriteLine(GetProduct.Category);
                            Console.WriteLine(GetProduct.Price);
                            Console.WriteLine(GetProduct.InStock);
                            break;
                        case 3://print all prodocts
                     
                            int counter = 0;
                            Product[] products = DalProduct.GetProductArray();
                            foreach (Product product in products)
                            {
                                if (counter == TestProduct.ProductLeangth())
                                {
                                    break;
                                }
                                    counter++; 
                                    Console.WriteLine(product);
                                
                            }
                            break;
                        case 4://update
                            Product TestUpdateProduct = new Product();
                            Console.WriteLine("Update a Id");
                            TestUpdateProduct.PrivateId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update Prodoct name");
                            TestUpdateProduct.ProdoctName = Console.ReadLine();
                            Console.WriteLine("Update Prodoct Category");
                            TestUpdateProduct.Category = Console.ReadLine();
                            Console.WriteLine("Update product price");
                            TestUpdateProduct.Price = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Update amount in stock");
                            TestUpdateProduct.InStock = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                TestProduct.UpdateProduct(TestUpdateProduct);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                        case 5://delete
                            Console.WriteLine("Enter the ID of the prodoct to delete");
                            int ID = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                TestProduct.DeleteProdoct(ID);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    break;
 //************************************************************************************************//
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
                            TestAddOrder.PrivateId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Add order customer name");
                            TestAddOrder.CustomerName = Console.ReadLine();
                            Console.WriteLine("Add order customer email");
                            TestAddOrder.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("Add Order date");
                            TestAddOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Add ship date");
                            TestAddOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Add delivory date");
                            TestAddOrder.DelivoryDate = Convert.ToDateTime(Console.ReadLine());
                                try
                                {
                                TestOrder.AddOrder(TestAddOrder);
                                }
                                catch (Exception ex)
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
                                GetOrder = TestOrder.Get(getId);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            Console.WriteLine(GetOrder.PrivateId);
                            Console.WriteLine(GetOrder.CustomerName);   
                            Console.WriteLine(GetOrder.CustomerEmail); 
                            Console.WriteLine(GetOrder.CustomerAdress);
                            Console.WriteLine(GetOrder.OrderDate);
                            Console.WriteLine(GetOrder.ShipDate);
                            Console.WriteLine(GetOrder.DelivoryDate);
                            break;
                        case 3://print all orders
                            Order[] orders = DalOrder.GetOrderArray();
                            int counter = 0;
                            foreach(Order order in orders)
                            { 
                                    counter++;
                                    Console.WriteLine(order);
                                if(counter == TestOrder.OrderLeangth())
                                {
                                    break;
                                }
                            }
                            break;
                        case 4://update
                            Order TestUpdateOrder = new Order();
                            Console.WriteLine("Update order Private ID");
                            TestUpdateOrder.PrivateId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update order customer name");
                            TestUpdateOrder.CustomerName = Console.ReadLine();
                            Console.WriteLine("Update order customer email");
                            TestUpdateOrder.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("Update Order date");
                            TestUpdateOrder.OrderDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Update ship date");
                            TestUpdateOrder.ShipDate = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Update delivory date");
                            TestUpdateOrder.DelivoryDate = Convert.ToDateTime(Console.ReadLine());
                            try
                            {
                                TestOrder.UpdateOrder(TestUpdateOrder);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                        case 5://delete
                            Console.WriteLine("Enter the ID of the order to delete");
                            int ID = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                TestOrder.DeleteOrder(ID);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    break;
//************************************************************************************************//
                case 3:
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
                        case 1:
                            OrderItem TestAddOrderItem = new OrderItem();
                            Console.WriteLine("Add Private ID");
                            
                            TestAddOrderItem.PrivateId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Add order id");
                            TestAddOrderItem.OrderId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Add product id");
                            TestAddOrderItem.ProductId = Convert.ToInt32(Console.ReadLine());   
                            Console.WriteLine("Add price item");
                            TestAddOrderItem.PriceItem = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Add amount");
                            TestAddOrderItem.Amount = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                TestOrderItem.AddOrderItem(TestAddOrderItem);
                                }
                                catch(Exception ex)
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
                                GetOrderItem = TestOrderItem.Get(getId);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            Console.WriteLine(GetOrderItem.PrivateId);
                            Console.WriteLine(GetOrderItem.OrderId);
                            Console.WriteLine(GetOrderItem.ProductId);
                            Console.WriteLine(GetOrderItem.PriceItem);
                            Console.WriteLine(GetOrderItem.Amount);
                            break;
                        case 3://print OrderItem

                            OrderItem[] orderItems = DalOrderItem.GetOrderItemArray();
                            int counter = 0;
                            foreach (OrderItem orderItem in orderItems)
                            {
                                counter++;
                                Console.WriteLine(orderItem);
                                if (counter == TestOrderItem.OrderItemLeangth())
                                {
                                    break;
                                }
                            }
                            break;
                        case 4://update
                            OrderItem TestUpdateOrderItem = new OrderItem();
                            Console.WriteLine("Update Private ID");
                            TestUpdateOrderItem.PrivateId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update order id");
                            TestUpdateOrderItem.OrderId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update product id");
                            TestUpdateOrderItem.ProductId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Update price item");
                            TestUpdateOrderItem.PriceItem = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Update amount");
                            TestUpdateOrderItem.Amount = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                TestOrderItem.UpdateOrderItem(TestUpdateOrderItem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                        case 5://delete
                            Console.WriteLine("Enter the ID of the order item to delete");
                            int ID = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                TestOrderItem.DeleteOrderItem(ID);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                    }
                    break;
            }
//***********************************************************************************************//
            Console.WriteLine("Enter 0 to end\n " +
                "Enter 1 for product\n " +
                "Enter 2 for order\n " +
                "Enter 3 for order item:");
            option = Convert.ToInt32(Console.ReadLine());
        }
    }
}


