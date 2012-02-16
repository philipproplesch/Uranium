using System.Web.Mvc;
using Uranium.Core.Infrastructure;

namespace Uranium.Mvc.Infrastructure
{
    public class GlobalFiltersInitialization : IBootstrapItem
    {
        public void Execute()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}