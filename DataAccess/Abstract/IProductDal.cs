using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //interface default olarak public değil fakat metodları default public tir..
    public interface IProductDal:IEntityRepository<Product>    
    {       

    }
}
