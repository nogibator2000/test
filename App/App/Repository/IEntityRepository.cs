    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repository
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        public T? GetSingle(int id);
        public T? GetSingle(Expression<Func<T, bool>> predicate);
        public T? GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
    }
}