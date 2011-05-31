using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Uranium.Mvc.Infrastructure
{
    class StructureMapDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            if(!serviceType.IsAbstract)
                return ServiceLocator.Current.GetService(serviceType);

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ServiceLocator.Current.GetAllInstances(serviceType);
        }
    }
}
