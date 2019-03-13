using MyProject.BusinessLogic;
using MyProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyProject.Domain.Entities;

namespace MyProject.Web.Controllers
{
    public class LoginController : Controller
    {
		private readonly ISession _session;

		public LoginController()
		{
			var bl = new InstanceBL();
			_session = bl.GetSessionBL();
		}

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(UserLogin login)
		{
			if (ModelState.IsValid)
			{
				var data = Mapper.Map<ULoginData>(login);
				data.LoginDateTime = DateTime.Now;

				var userLogin = _session.UserLogin(data);
				if (userLogin.Status)
				{
					HttpCookie cookie = _session.GenCookie(login.Username);
					ControllerContext.HttpContext.Response.Cookies.Add(cookie);

					return RedirectToAction("Index", "Home");
				} else
				{
					ModelState.AddModelError("", userLogin.StatusMsg);
				}
			}
			return View();
		}
    }
}