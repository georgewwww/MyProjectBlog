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
				ViewBag.Username = user.Username;
				ViewBag.Level = user.Level;
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
				ViewBag.Username = u.Username;
				ViewBag.Level = u.Level;

				return View(u);
			} else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		
		public ActionResult About()
		{
			var u = GetUser();
			ViewBag.Username = u.Username;
			ViewBag.Level = u.Level;

			return View();
		}
    }
}