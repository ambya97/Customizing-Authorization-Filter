using System.Web;
using System.Web.Mvc;

namespace Customizing_Authorization_Filter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
