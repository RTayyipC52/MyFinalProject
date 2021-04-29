using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    // generic constraint, generic kısım
    // class : referans tip olabilir
    // IEntity : IEntitiy veya IEntity implemente eden bir nesne olabilir
    // new(): new'lenebilir olmalı, IEntity interface olduğundan newlenemez, IEntity yazılmaması için
    public interface IEntityRepository<T> where T:class, IEntity, new()
    {
        // Filtre koyma, expression verebilme syntaxı, Linq ile geliyor
        // filter = null, filtre vermeyedebilirsin, tüm datayı istiyor demek 
        List<T> GetAll(Expression<Func<T,bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
