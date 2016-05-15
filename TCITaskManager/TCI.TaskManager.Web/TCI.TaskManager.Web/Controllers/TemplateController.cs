using System.Web.Mvc;

namespace TCI.TaskManager.Web.Controllers
{
	public class TemplateController : TCITaskManagerControllerBase
    {
		public PartialViewResult Render(string feature, string name)
		{
			return PartialView(string.Format("~/scripts/app/{0}/templates/{1}", feature, name));
		}
	}
}