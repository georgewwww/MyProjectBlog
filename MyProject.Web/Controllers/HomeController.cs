using AutoMapper;
using MyProject.Web.Extension;
using MyProject.Web.Models;
using System.Web.Mvc;
using MyProject.BusinessLogic;

namespace MyProject.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlog _blog;

        public HomeController()
		{
            var bl = new InstanceBL();
            _blog = bl.GetBlogBL();
        }

        // GET: Home
        public ActionResult Index()
        {
			SessionStatus();

            var model = new IndexPostsViewModel();
            model.featuredPost = _blog.GetFeaturedPost();
            model.lastPosts = _blog.GetLastPosts();

			return View(model);
        }

		public new ActionResult Profile()
		{
			SessionStatus();
			if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
			{
				var entity = System.Web.HttpContext.Current.GetMySessionObject();
				var u = _mapper.Map<UserData>(entity);

				return View(u);
			} else
			{
				return RedirectToAction("Index", "Home");
			}
		}

        public ActionResult SavePreferences(UserData user)
        {
            SetEmail(user.Email);
            return RedirectToAction("Profile");
        }

        public ActionResult SavePassword(UserData user)
        {
            SetPassword(user.OldPassword, user.NewPassword);
            return RedirectToAction("Profile");
        }

        public ActionResult About()
		{
			return View();
		}
    }
}