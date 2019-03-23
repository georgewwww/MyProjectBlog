﻿using MyProject.Web.Extension;
using MyProject.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Web.Models;

namespace MyProject.Web.Controllers
{
    public class BaseController : Controller
    {
		private readonly ISession _session;
       
		public BaseController()
		{
			var bl = new InstanceBL();
			_session = bl.GetSessionBL();
		}

		public void SessionStatus()
		{
			var apiCookie = Request.Cookies["LOGIN-KEY"];
			if (apiCookie != null)
			{
				var profile = _session.GetUserByCookie(apiCookie.Value);
				if (profile != null)
				{
					System.Web.HttpContext.Current.SetMySessionObject(profile);
					System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
				} else
				{
					System.Web.HttpContext.Current.Session.Clear();
					System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
				}
			} else
			{
				System.Web.HttpContext.Current.Session.Clear();
				System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
			}
		}
		
		public UserData GetUser()
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
				return u;
			}
			return new UserData();
		}
    }
}