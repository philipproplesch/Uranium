using System;

namespace Uranium.Core.Extensibility.Events
{
    public class RepositoryEventArgs<TEntity> : EventArgs
    {
        public TEntity Entity { get; set; }
    }
}
