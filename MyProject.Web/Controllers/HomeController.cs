using MyProject.Web.Extension;
using MyProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
			SessionStatus();

			if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
			{
				var user = System.Web.HttpContext.Current.GetMySessionObject();
				UserData u = new UserData
				{
					Username = user.Username,
					Level = user.Level
				};
				return View(u);
			}

			return View();
        }

		public new ActionResult Profile()
		{
			SessionStatus();
			if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}
    }
}