using MyProject.Web.Controllers.Attributes;
using MyProject.Web.Extension;
using MyProject.Web.Models;
using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
	[AdminMod]
	public class AdminController : BaseController
	{
		// GET: Admin
		public ActionResult Index()
        {
			SessionStatus();

			if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
			{
				var user = System.Web.HttpContext.Current.GetMySessionObject();
				var u = _mapper.Map<UserData>(user);

				return View(u);
			}
			return View();
        }
    }
}