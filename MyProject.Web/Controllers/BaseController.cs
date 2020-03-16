using MyProject.Web.Extension;
using MyProject.BusinessLogic;
using System.Web.Mvc;
using AutoMapper;
using MyProject.Web.Models;

namespace MyProject.Web.Controllers
{
    public class BaseController : Controller
    {
	    public readonly Mapper _mapper;
	    private readonly ISession _session;
       
		public BaseController()
		{
			_mapper = new Mapper(AutoMapperConfig.Initialize());
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

        public void SetEmail(string email)
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                var user = System.Web.HttpContext.Current.GetMySessionObject();

                _session.SetUserEmail(user.Id, email);
            }
        }

        public void SetPassword(string oldPass, string pass)
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                var user = System.Web.HttpContext.Current.GetMySessionObject();

                _session.SetUserPassword(user.Id, oldPass, pass);
            }
        }
    }
}