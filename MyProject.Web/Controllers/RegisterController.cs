using AutoMapper;
using MyProject.BusinessLogic;
using MyProject.Domain.Entities;
using MyProject.Web.Controllers.Attributes;
using MyProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
	[GuestMod]
    public class RegisterController : Controller
    {
		private readonly ISession _session;

		public RegisterController()
		{
			var bl = new InstanceBL();
			_session = bl.GetSessionBL();
		}

		// GET: Register
		public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(UserRegister user)
		{
			if (ModelState.IsValid)
			{
				if (user.Password != user.RePassword)
				{
					ModelState.AddModelError("", "Password is not the same");
					return View();
				}

				var data = Mapper.Map<URegisterData>(user);
				data.RegisterDate = DateTime.Now;

				var registerResponse = _session.UserRegister(data);
				if (registerResponse.Status)
				{
					return RedirectToAction("Index", "Home");
				} else
				{
					ModelState.AddModelError("", registerResponse.StatusMsg);
				}
			}
			return View();
		}
    }
}