using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using Uranium.Core.Data;

namespace Uranium.Data.NH
{
    internal class NHibernateRepository<T> : IRepository<T> where T : class
    {
        private readonly ISession _session;
        public NHibernateRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<T> Fetch()
        {
            return _session.Query<T>();
        }

        public IEnumerable<T> Fetch(int skip, int take)
        {
            return _session.Query<T>().Skip(skip).Take(take);
        }

        public IEnumerable<T> FetchOrderBy(Expression<Func<T, object>> orderPredicate)
        {
            return _session.Query<T>().OrderBy(orderPredicate);
        }

        public IEnumerable<T> FetchOrderByDescending(Expression<Func<T, object>> orderPredicate)
        {
            return _session.Query<T>().OrderByDescending(orderPredicate);
        }

        public IEnumerable<T> FetchOrderBy(Expression<Func<T, object>> orderPredicate, int skip, int take)
        {
            return _session.Query<T>().OrderBy(orderPredicate).Skip(skip).Take(take);
        }

        public IEnumerable<T> FetchOrderByDescending(Expression<Func<T, object>> orderPredicate, int skip, int take)
        {
            return _session.Query<T>().OrderByDescending(orderPredicate).Skip(skip).Take(take);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _session.Query<T>().Where(predicate);
        }

        public IEnumerable<T> FindOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderPredicate)
        {
            return _session.Query<T>().OrderBy(orderPredicate).Where(predicate);
        }

        public IEnumerable<T> FindOrderByDescending(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderPredicate)
        {
            return _session.Query<T>().OrderByDescending(orderPredicate).Where(predicate);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return _session.Query<T>().Where(predicate).Skip(skip).Take(take);
        }

        public IEnumerable<T> FindOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderPredicate, int skip, int take)
        {
            return _session.Query<T>().OrderBy(orderPredicate).Where(predicate).Skip(skip).Take(take);
        }

        public IEnumerable<T> FindOrderByDescending(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderPredicate, int skip, int take)
        {
            return _session.Query<T>().OrderByDescending(orderPredicate).Where(predicate).Skip(skip).Take(take);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _session.Query<T>().Single(predicate);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _session.Query<T>().SingleOrDefault(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return _session.Query<T>().First(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _session.Query<T>().FirstOrDefault(predicate);
        }

        public T Get(Guid id)
        {
            return _session.Get<T>(id);
        }

        public T Insert(T entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                // This is to prevent a 'NonUniqueObjectException'.
                _session.Clear();

                _session.Save(entity);
                tx.Commit();

                if (!tx.WasCommitted)
                {
                    Trace.TraceError("An error occured while inserting a new entity of type '{0}'.", entity.GetType().Name);

                    tx.Rollback();
                    return default(T);
                }

                Trace.TraceInformation("A new entity of type '{0}' was inserted successfully.", entity.GetType().Name);

                return entity;
            }
        }

        public T Update(T entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                // This is to prevent a 'NonUniqueObjectException'.
                _session.Clear();

                _session.Update(entity);
                tx.Commit();

                if (!tx.WasCommitted)
                {
                    Trace.TraceError("An error occured while updating an entity of type '{0}'.", entity.GetType().Name);

                    tx.Rollback();
                    return default(T);
                }

                Trace.TraceInformation("An entity of type '{0}' was updated successfully.", entity.GetType().Name);

                return entity;
            }
        }

        public bool Delete(Guid id)
        {
            var entity = Get(id);

            if (entity == null)
                return false;

            return Delete(entity);
        }

        public bool Delete(T entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.Delete(entity);
                tx.Commit();

                if (!tx.WasCommitted)
                {
                    Trace.TraceError("An error occured while deleting an entity of type '{0}'.", entity.GetType().Name);

                    tx.Rollback();
                    return false;
                }

                Trace.TraceInformation("An entity of type '{0}' was deleted successfully.", entity.GetType().Name);

                return true;
            }
        }
    }
}
