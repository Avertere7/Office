using System.Web;
using System.Web.Mvc;

namespace BiuroRachunkowe
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
