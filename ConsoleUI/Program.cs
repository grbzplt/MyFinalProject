using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("");
            //Console.WriteLine("----------- InMemoryProductDal -------");
            //ProductManager productManager = new ProductManager(new InMemoryProductDal());

            //foreach (var product in productManager.GetAll())
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            Console.WriteLine("");
            Console.WriteLine("----------- EfProductDal -------------");

            ProductManager productManager2 = new ProductManager(new EfProductDal());

            foreach (var product in productManager2.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }

        }
    }   
}
