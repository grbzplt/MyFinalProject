using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            //Sql Server, Oracle, MongoDb geliyormuş gibi simule ediyoruz.
            _products = new List<Product> {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
            
        }

        //public void Delete(Product product)
        //{
        //    //(1) 
        //    _products.Remove(product); //Bu şekilde product silinmez.Çünkü referans lar ile çalışıyoruz.

        //    //Ürün silerken onun primary key ini kullanırız.

        //    //(2) LINQ kullanmadan klasik yöntemle:
        //    //Product productToDelete=new Product() --> HATALI kullanım;
        //    Product productToDelete=null; //null --> referansı yok.
        //    foreach (var p in _products)
        //    {
        //        if (product.ProductId==p.ProductId)
        //        {
        //            productToDelete = p;
        //        }
        //    }
        //   _products.Remove(productToDelete);          
        //}

        public void Delete(Product product)
        {
            //(3) LINQ kullanarak:

            Product productToDelete;

            //SingleOrDefault --> tek bir eleman bulmaya yarar. (FirstOrDefault  da kullanılabilir.)
            //SingleOrDefault(p=>p.ProductId==product.ProductId)  kodu yukarıdaki foreach in yaptığını yapıyor.

            productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            _products.Remove(productToDelete);
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün Id sine sahip olan listedeki ürünü bul
            Product productToUpdate= _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;

        }
        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
}
