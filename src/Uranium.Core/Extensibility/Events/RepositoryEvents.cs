using System;

namespace Uranium.Core.Extensibility.Events
{
    public class RepositoryEvents<T>
    {
        private static object s_lock = new object();

        #region OnInserting
        private static EventHandler<RepositoryEventArgs<T>> s_inserting;
        public static event EventHandler<RepositoryEventArgs<T>> Inserting
        {
            add
            {
                lock (s_lock)
                {
                    s_inserting += value;
                }
            }
            remove
            {
                lock (s_lock)
                {
                    s_inserting -= value;
                }
            }
        }

        public static void OnInserting(T entity)
        {
            if (s_inserting == null)
                return;

            s_inserting(
                null,
                new RepositoryEventArgs<T> { Entity = entity });
        }
        #endregion

        #region OnInserted
        private static EventHandler<RepositoryEventArgs<T>> s_inserted;
        public static event EventHandler<RepositoryEventArgs<T>> Inserted
        {
            add
            {
                lock (s_lock)
                {
                    s_inserted += value;
                }
            }
            remove
            {
                lock (s_lock)
                {
                    s_inserted -= value;
                }
            }
        }

        public static void OnInserted(T entity)
        {
            if (s_inserted == null)
                return;

            s_inserted(
                null,
                new RepositoryEventArgs<T> { Entity = entity });
        }
        #endregion

        #region OnUpdating
        private static EventHandler<RepositoryEventArgs<T>> s_updating;
        public static event EventHandler<RepositoryEventArgs<T>> Updating
        {
            add
            {
                lock (s_lock)
                {
                    s_updating += value;
                }
            }
            remove
            {
                lock (s_lock)
                {
                    s_updating -= value;
                }
            }
        }

        public static void OnUpdating(T entity)
        {
            if (s_updating == null)
                return;

            s_updating(
                null,
                new RepositoryEventArgs<T> { Entity = entity });
        }
        #endregion

        #region OnUpdated
        private static EventHandler<RepositoryEventArgs<T>> s_updated;
        public static event EventHandler<RepositoryEventArgs<T>> Updated
        {
            add
            {
                lock (s_lock)
                {
                    s_updated += value;
                }
            }
            remove
            {
                lock (s_lock)
                {
                    s_updated -= value;
                }
            }
        }

        public static void OnUpdated(T entity)
        {
            if (s_updated == null)
                return;

            s_updated(
                null,
                new RepositoryEventArgs<T> { Entity = entity });
        }
        #endregion

        #region OnDeleting
        private static EventHandler<RepositoryEventArgs<T>> s_deleting;
        public static event EventHandler<RepositoryEventArgs<T>> Deleting
        {
            add
            {
                lock (s_lock)
                {
                    s_deleting += value;
                }
            }
            remove
            {
                lock (s_lock)
                {
                    s_deleting -= value;
                }
            }
        }

        public static void OnDeleting(T entity)
        {
            if (s_deleting == null)
                return;

            s_deleting(
                null,
                new RepositoryEventArgs<T> { Entity = entity });
        }
        #endregion

        #region OnDeleted
        private static EventHandler<RepositoryEventArgs<T>> s_deleted;
        public static event EventHandler<RepositoryEventArgs<T>> Deleted
        {
            add
            {
                lock (s_lock)
                {
                    s_deleted += value;
                }
            }
            remove
            {
                lock (s_lock)
                {
                    s_deleted -= value;
                }
            }
        }

        public static void OnDeleted(T entity)
        {
            if (s_deleted == null)
                return;

            s_deleted(
                null,
                new RepositoryEventArgs<T> { Entity = entity });
        }
        #endregion
    }
}