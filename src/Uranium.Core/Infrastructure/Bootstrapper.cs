using System;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using Uranium.Core.Data;
using Uranium.Core.Data.Common;
using Uranium.Core.Extensibility;

namespace Uranium.Core.Infrastructure
{
    public class Bootstrapper
    {
        private static Predicate<Assembly> _assemblyFilter;
        public static void Bootstrap(Predicate<Assembly> assemblyFilter)
        {
            _assemblyFilter = assemblyFilter;

            Bootstrap();
        }

        public static void Bootstrap()
        {
            var container = new Container();

            ConfigureContainer(container);
            SetServiceLocator(container);

            InitializeBootstrapMembers();
        }

        private static void InitializeBootstrapMembers()
        {
            var bootstrapItems = 
                ServiceLocator.Current.GetAllInstances<IBootstrapItem>();

            foreach (var bootstrapMember in bootstrapItems)
            {
                bootstrapMember.Execute();
            }
        }

        private static void SetServiceLocator(IContainer container)
        {
            var serviceLocator = new StructureMapServiceLocator(container);
            container.Configure(x => x.For<IServiceLocator>().Singleton().Use(serviceLocator));

            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        private static void ConfigureContainer(IContainer container)
        {
            container.Configure(
                x =>
                x.Scan(
                    scan =>
                    {
                        scan.TheCallingAssembly();
                        scan.Assembly(Assembly.GetEntryAssembly());

                        if (_assemblyFilter != null)
                            scan.AssembliesFromApplicationBaseDirectory(_assemblyFilter);

                        scan.AssembliesFromApplicationBaseDirectory(GetFilteredAssemblies);
                        scan.WithDefaultConventions();
                        scan.AddAllTypesOf<IEntity>();
                        scan.AddAllTypesOf<IMappedEntity>();
                        scan.AddAllTypesOf<IDatabaseSeeder>();
                        scan.AddAllTypesOf<IPlugin>();
                        scan.AddAllTypesOf<IBootstrapItem>();
                        scan.LookForRegistries();
                    }));
        }

        private static bool GetFilteredAssemblies(Assembly obj)
        {
            return obj.FullName.StartsWith("Uranium.");
        }
    }
}
