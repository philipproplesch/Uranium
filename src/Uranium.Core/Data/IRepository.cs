using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Uranium.Core.Data
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Fetches all entites.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Fetch();
        IEnumerable<T> Fetch(int skip, int take);
        IEnumerable<T> FetchOrderBy(Expression<Func<T, object>> orderPredicate);
        IEnumerable<T> FetchOrderByDescending(Expression<Func<T, object>> orderPredicate);
        IEnumerable<T> FetchOrderBy(Expression<Func<T, object>> orderPredicate, int skip, int take);
        IEnumerable<T> FetchOrderByDescending(Expression<Func<T, object>> orderPredicate, int skip, int take);

        /// <summary>
        /// Finds all entites that matches the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns> 
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderPredicate);
        IEnumerable<T> FindOrderByDescending(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderPredicate);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, int skip, int take);
        IEnumerable<T> FindOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderPredicate, int skip, int take);
        IEnumerable<T> FindOrderByDescending(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderPredicate, int skip, int take);

        /// <summary>
        /// Gets a single entity that matches the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the first entity that matches the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets a stored entity by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(Guid id);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(Guid id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);
    }
}
