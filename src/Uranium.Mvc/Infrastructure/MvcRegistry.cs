using System.Web.Mvc;
using StructureMap.Configuration.DSL;

namespace Uranium.Mvc.Infrastructure
{
    public class MvcRegistry : Registry
    {
        public MvcRegistry()
        {
            Scan(scan => scan.With(new ControllerRegistryConvention()));
            DependencyResolver.SetResolver(new StructureMapDependencyResolver());
        }
    }
}
