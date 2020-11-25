﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IAsyncRepository<T> where T: class //this means T is a reference type
    {
        //CRUD operations, which are common across all the repos.

        //Get and entity by Id => movieId => movie

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T,bool>> filter=null);
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
        //check if certain record exists in table
        Task<bool> GetExistAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
