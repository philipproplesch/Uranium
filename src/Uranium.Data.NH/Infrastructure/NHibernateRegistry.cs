using System.Web;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Uranium.Data.NH.Infrastructure
{
    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry()
        {
            For<ISessionFactory>()
                .Singleton()
                .Use(SessionFactoryBuilder.CreateSessionFactory);

            if (HttpContext.Current != null)
            {
                For<ISession>()
                .HttpContextScoped()
                .Use(x => x.GetInstance<ISessionFactory>().OpenSession());
            }
            else
            {
                For<ISession>()
                .Use(x => x.GetInstance<ISessionFactory>().OpenSession());
            }
        }
    }
}
