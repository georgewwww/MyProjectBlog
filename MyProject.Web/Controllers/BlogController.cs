using MyProject.BusinessLogic;
using MyProject.Domain.Entities;
using MyProject.Web.Controllers.Attributes;
using MyProject.Web.Extension;
using MyProject.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers
{
	public class BlogController : BaseController
	{
		private readonly IBlog _blog;

		public BlogController()
		{
			var bl = new InstanceBL();
			_blog = bl.GetBlogBL();
		}

		// GET: Blog
		public ActionResult Index()
        {
            return View(_blog.GetAllPosts());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int id)
        {
            _blog.IncreasePostViews(id);
            return View(_blog.GetPostById(id));
        }

		// GET: Blog/Create
		[EditorMod]
		public ActionResult Create()
		{
			return View();
        }

		// POST: Blog/Create
		[EditorMod]
		[HttpPost]
        public ActionResult Create(PostViewModel post)
		{
			if (post.Title == null || post.PostContent == null)
				return View();

            post.PostContent = HttpUtility.HtmlDecode(post.PostContent);
			var user = System.Web.HttpContext.Current.GetMySessionObject();

			try
			{
				var p = _mapper.Map<PostEntity>(post);
				if (p.ImageUrl == null)
					p.ImageUrl = "/Content/imgs/nothing.png";
				p.Date = DateTime.Now;

				_blog.TryAddPost(user.Id, p);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		// GET: Blog/Edit/5
		[EditorMod]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var model = _blog.GetPostById(Int32.Parse(id.ToString()));
            return PartialView("_EditPost", model);
        }

		// POST: Blog/Edit/{Model}
        [EditorMod]
        [HttpPost]
        public ActionResult Edit(PostEntity model)
        {
            if (model == null)
                return RedirectToAction("Index");
            try
            {
                var result = _blog.TryEditPost(model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: Blog/Delete/5
		[AdminMod]
        [HttpPost]
        public JsonResult Delete(int postId)
        {
            var result = _blog.TryDeletePost(postId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
