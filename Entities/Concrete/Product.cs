using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product :IEntity   //Product class ı default olarak:internal  Sadece Entity  de geçerli.
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }        
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
