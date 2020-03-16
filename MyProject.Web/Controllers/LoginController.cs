using MyProject.BusinessLogic;
using MyProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyProject.Domain.Entities;
using System.Web.Routing;
using MyProject.Web.Controllers.Attributes;

namespace MyProject.Web.Controllers
{
    public class LoginController : Controller
	{
		public readonly Mapper _mapper;
		private readonly ISession _session;
		
		public LoginController()
		{
			_mapper = new Mapper(AutoMapperConfig.Initialize());
			var bl = new InstanceBL();
			_session = bl.GetSessionBL();
		}

        [GuestMod]
        public ActionResult Index()
        {
            return View();
        }

		[GuestMod]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(UserLogin login)
		{
			if (ModelState.IsValid)
			{
				var data = _mapper.Map<ULoginData>(login);
				data.LoginDateTime = DateTime.Now;

				var userLogin = _session.UserLogin(data);
				if (userLogin.Status)
				{
					HttpCookie cookie = _session.GenCookie(login.Username);
					ControllerContext.HttpContext.Response.Cookies.Add(cookie);

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", userLogin.StatusMsg);
				}
			}
			return View();
		}

		public ActionResult Logout()
		{
			var apiCookie = Request.Cookies["LOGIN-KEY"];
			var sessionExist = _session.UserLogout(apiCookie.Value);
			if (sessionExist.Status)
			{
				return new RedirectToRouteResult(new
					RouteValueDictionary(new { controller = "Home", action = "Index" }));
			}
			else
			{
				ModelState.AddModelError("", sessionExist.StatusMsg);
			}
			return View();
		}
	}
}