using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //Generic Constraint
    //class --> referans tip olmalı. (Referans tip olmayanlar (int gibi) elendi fakat herhangibir class olabilir.)
    //IEntity --> IEntity yada IEntity implemente eden bir nesne olmalı. (IEntity dışında class lar da elendi fakat IEntity olabilir.)
    //new() --> new ' lenebilir olmalı. (IEntity de elendi çünkü IEntity new lenemez.)
    public interface IEntityRepository<T> where T : class, IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>>filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
