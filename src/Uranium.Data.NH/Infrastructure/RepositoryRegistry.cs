using StructureMap.Configuration.DSL;
using Uranium.Core.Data.Common;
using Uranium.Data.NH.Repositories;

namespace Uranium.Data.NH.Infrastructure
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For(typeof(IRepository<,>)).Use(typeof(NHibernateRepository<>));
        }
    }
}
