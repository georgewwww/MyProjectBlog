using AutoMapper;
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
			if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
			{
				var entity = System.Web.HttpContext.Current.GetMySessionObject();
				var u = Mapper.Map<UserData>(entity);

				return View(u);
			} else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		
		public ActionResult About()
		{
			var user = GetUser();
			if (user != null)
				return View(user);

			return View();
		}
    }
}