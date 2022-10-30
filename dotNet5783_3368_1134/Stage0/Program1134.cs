using System;
//hello!!!!!!!!!!!!!!!!!!!!!!!!
namespace Targil0
{
    partial class Program
    {
        static void Main(String[] args)
        {
            Welcome1134();
            Welcome3368();
            Console.ReadKey();
        }
        static partial void Welcome3368();
        private static void Welcome1134()
        {
            Console.Write("Enter your name: ");
            string username = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", username);
        }
    }
}

