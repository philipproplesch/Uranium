using StructureMap.Configuration.DSL;
using Uranium.Core.Data;

namespace Uranium.Data.NH.Infrastructure
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For(typeof(IRepository<>)).Use(typeof(NHibernateRepository<>));
        }
    }
}
