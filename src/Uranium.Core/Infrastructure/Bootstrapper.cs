using Microsoft.Practices.ServiceLocation;
using StructureMap;
using Uranium.Core.Data;
using Uranium.Core.Extensibility;

namespace Uranium.Core.Infrastructure
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            var container = new Container();
            SetServiceLocator(container);
            ConfigureContainer(container);
        }

        private static void SetServiceLocator(IContainer container)
        {
            var serviceLocator = new StructureMapServiceLocator(container);
            container.Configure(x => x.For<IServiceLocator>().Singleton().Use(serviceLocator));

            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        private static void ConfigureContainer(IContainer container)
        {
            container.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.LookForRegistries();
                    scan.AddAllTypesOf<IMappedEntity>();
                    scan.AddAllTypesOf<IDatabaseInitializer>();
                    scan.AddAllTypesOf<IPlugin>();
                });
            });
        }
    }
}
