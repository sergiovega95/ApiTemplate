﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync ();
        Task<IEnumerable<T>> FindAsync (Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync (IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);        
        void Update(T entity);
        Task SaveChangesAsync();
    }
}
