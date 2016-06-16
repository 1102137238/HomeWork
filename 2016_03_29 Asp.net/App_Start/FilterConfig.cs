using System.Web;
using System.Web.Mvc;

namespace _2016_03_29_Asp.net
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
