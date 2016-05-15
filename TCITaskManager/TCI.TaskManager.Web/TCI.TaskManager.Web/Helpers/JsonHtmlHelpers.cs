using System.Web;
using System.Web.Mvc;
using TCI.TaskManager.Web.Utilities;

namespace TCI.TaskManager.Web.Helpers
{
	public static class JsonHtmlHelpers
	{
		public static IHtmlString JsonFor<T>(this HtmlHelper helper, T obj)
		{
			return helper.Raw(obj.ToJson());
		}
	}
}