using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Uranium.Core.Data;
using Uranium.Data.NH.Conventions;
using Configuration = NHibernate.Cfg.Configuration;

namespace Uranium.Data.NH
{
    internal class SessionFactoryBuilder
    {
        internal static ISessionFactory CreateSessionFactory()
        {
            string connectionStringName = GetConnectionStringName();

            Action<ConnectionStringBuilder> connectionStringBuilder =
                x => x.FromConnectionStringWithKey(connectionStringName);

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionString == null || String.IsNullOrEmpty(connectionString.ProviderName))
                throw new ConfigurationErrorsException();

            IPersistenceConfigurer persistenceConfigurer = null;
            switch (connectionString.ProviderName)
            {
                case "System.Data.SqlClient":
                    persistenceConfigurer = MsSqlConfiguration.MsSql2008.ConnectionString(connectionStringBuilder);
                    break;
                case "System.Data.SQLite":
                case "Mono.Data.SqliteClient":
                    persistenceConfigurer = SQLiteConfiguration.Standard.ConnectionString(connectionStringBuilder);
                    break;
            }

            return Fluently.Configure()
                .Database(persistenceConfigurer)
                .Mappings(AddImportedFluentMappings)
                .Mappings(AddMappings)
                .ExposeConfiguration(CreateOrUpdateSchema)
                .BuildSessionFactory();
        }

        internal static string GetConnectionStringName()
        {
            string connectionStringName =
                ConfigurationManager.AppSettings["Uranium.Data.ConnectionStringName"];

            if (!String.IsNullOrWhiteSpace(connectionStringName))
            {
                return connectionStringName;
            }

            return "ConnectionString";
        }

        private static void AddMappings(MappingConfiguration cfg)
        {
            var mappings = ServiceLocator.Current.GetAllInstances<IEntity>();
            foreach (var mapping in mappings)
            {
                cfg.AutoMappings.Add(AutoMap.Assembly(mapping.GetType().Assembly));
            }
        }

        private static void AddImportedFluentMappings(MappingConfiguration cfg)
        {
            cfg.FluentMappings.Conventions.Setup(
                x =>
                {
                    x.Add(new CustomForeignKeyConvention());
                });

            var mappings = ServiceLocator.Current.GetAllInstances<IMappedEntity>();
            foreach (IMappedEntity mapping in mappings)
            {
                cfg.FluentMappings.Add(mapping.GetType());
            }
        }

        private static void AddFluentMappings(MappingConfiguration cfg)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                cfg.FluentMappings.AddFromAssembly(assembly);
            }
        }

        private static void CreateOrUpdateSchema(Configuration cfg)
        {
            // TODO: Determine when the schema should be updated.
            // TODO: Update the schema if necessary.

            try
            {
                new SchemaValidator(cfg).Validate();
            }
            catch (Exception)
            {
                Trace.TraceInformation("The database schema will be recreated.");

                new SchemaExport(cfg).Drop(false, true);
                new SchemaExport(cfg).Create(false, true);

                var dataInitializer =
                    ServiceLocator.Current
                        .GetAllInstances<IDatabaseInitializer>()
                        .OrderBy(x => x.Order);

                foreach (var initializer in dataInitializer)
                {
                    initializer.Execute();
                }
            }
        }
    }
}