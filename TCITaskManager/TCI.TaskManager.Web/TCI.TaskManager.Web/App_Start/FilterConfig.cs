using System.Web;
using System.Web.Mvc;

namespace TCI.TaskManager.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //Controllers are secure-by-default. 
            filters.Add(new AuthorizeAttribute());
        }
    }
}
