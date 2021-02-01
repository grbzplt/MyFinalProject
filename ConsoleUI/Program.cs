using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("----------- InMemoryProductDal -------");
            ProductManager productManager = new ProductManager(new InMemoryProductDal());

            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("");
            Console.WriteLine("----------- EfProductDal -------------");

            ProductManager productManager2 = new ProductManager(new EfProductDal());

            foreach (var product in productManager2.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("");
            Console.WriteLine("************************** Ödev3 Video Çalışma *******************************");

            List<Category> categories = new List<Category>
            {
                new Category{CategoryId=1,CategoryName="Kategori 1"},
                new Category{CategoryId=2,CategoryName="Kategori 2"},
            };

            List<Product> products = new List<Product>
            {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };

            //Test1(products);

            //Test2(products);

            //Test3(products);

            //Test4(products);

            //Test5(products);

            //Test6(products);

            //Test7(products);

            //Test8(products);

            //Test9(products);

            //class ProductDto eklendi.

            //Test10(categories, products); 

            //Test11(categories, products);

            Console.WriteLine("*************************** Video Çalışma Sonu *******************************");

        }

        private static void Test11(List<Category> categories, List<Product> products)
        {
            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.CategoryId
                         where p.UnitPrice > 150
                         orderby p.UnitPrice descending, p.ProductName ascending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice, CategoryName = c.CategoryName };

            foreach (var productDto in result)
            {
                Console.WriteLine($"{productDto.ProductName} --- {productDto.CategoryName}");
            }
        }

        private static void Test10(List<Category> categories, List<Product> products)
        {
            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.CategoryId
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice, CategoryName = c.CategoryName };

            foreach (var productDto in result)
            {
                Console.WriteLine($"{productDto.ProductName} --- {productDto.CategoryName}");
            }
        }

        private static void Test9(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 150
                         orderby p.UnitPrice descending, p.ProductName ascending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice };

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void Test8(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 150
                         orderby p.UnitPrice descending, p.ProductName ascending
                         select p;

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void Test7(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 150
                         select p;

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void Test6(List<Product> products)
        {
            var result = from p in products
                         select p;

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void Test5(List<Product> products)
        {
            var result = products.Where(p => p.ProductName.Contains("ar")).OrderByDescending(p => p.UnitPrice);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void Test4(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("ar"));
            Console.WriteLine(result.Count); //2-->Bardak ve Fare
        }

        private static void Test3(List<Product> products)
        {
            var result = products.Find(p => p.ProductId == 3);
            Console.WriteLine(result.ProductName);
        }

        private static void Test2(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "Bardak");
            Console.WriteLine(result);
        }

        private static void Test1(List<Product> products)
        {
            var result = products.Where(p => p.UnitPrice > 150 && p.UnitsInStock <= 3);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }

    class ProductDto
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }    //Eklenirse Test9 da CategoryName = c.CategoryName!!! -->Test10-11
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
