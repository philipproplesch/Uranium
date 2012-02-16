using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using Uranium.Core.Data.Common;
using Uranium.Core.Extensibility.Events;
using Uranium.Core.Models.Common;

namespace Uranium.Data.NH.Repositories
{
    internal class NHibernateRepository<T>
        : IRepository<T, Guid> where T : class, IEntityBase<Guid>
    {
        private readonly ISession _session;
        public NHibernateRepository(ISession session)
        {
            _session = session;
        }

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _session.Query<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        public Expression Expression
        {
            get { return _session.Query<T>().Expression; }
        }

        public Type ElementType
        {
            get { return _session.Query<T>().ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return _session.Query<T>().Provider; }
        }

        #endregion

        #region Implementation of IRepository<T,in Guid>

        public T Get(Guid id)
        {
            return _session.Get<T>(id);
        }

        public bool Insert(T entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                entity.Inserted = DateTime.Now;

                try
                {
                    RepositoryEvents<T>.OnInserting(entity);

                    _session.Save(entity);

                    RepositoryEvents<T>.OnInserted(entity);

                    tx.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    Trace.TraceError(
                        "An error occured while inserting an instance of type \"{0}\": {1}",
                        typeof(T).Name,
                        ex);
                }
            }

            return false;
        }

        public bool Update(T entity)
        {
            using (var tx = _session.BeginTransaction())
            {
                entity.Modified = DateTime.Now;

                try
                {
                    RepositoryEvents<T>.OnUpdating(entity);

                    _session.Update(entity);

                    RepositoryEvents<T>.OnUpdated(entity);

                    tx.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    Trace.TraceError(
                        "An error occured while updating an instance of type \"{0}\": {1}",
                        typeof(T).Name,
                        ex);
                }
            }

            return false;
        }

        public bool Delete(Guid id)
        {
            var entity = Get(id);
            return entity != null && Delete(entity);
        }

        public bool Delete(T entity)
        {
            if (entity == null)
                return false;

            entity.Deleted = true;
            var response = Update(entity);

            return response;
        }

        public bool DeleteFromDatabase(Guid id)
        {
            var entity = Get(id);
            return entity != null && DeleteFromDatabase(entity);
        }

        public bool DeleteFromDatabase(T entity)
        {
            if (entity == null)
                return false;

            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    RepositoryEvents<T>.OnDeleting(entity);

                    _session.Delete(entity);

                    RepositoryEvents<T>.OnDeleted(entity);

                    tx.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    Trace.TraceError(
                        "An error occured while deleting an instance of type \"{0}\" with Id \"{1}\": {2}",
                        typeof(T).Name,
                        entity.Id,
                        ex);

                    return false;
                }
            }
        }

        #endregion
    }
}
