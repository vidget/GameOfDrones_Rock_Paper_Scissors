using System.Web;
using System.Web.Mvc;

namespace GameOfDrones_Rock_Paper_Scissors
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
