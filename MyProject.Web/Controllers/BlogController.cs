using AutoMapper;
using MyProject.BusinessLogic;
using MyProject.Domain.Entities;
using MyProject.Web.Controllers.Attributes;
using MyProject.Web.Extension;
using MyProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
			var user = GetUser();
			ViewBag.Username = user.Username;
			ViewBag.Level = user.Level;

            return View(_blog.GetAllPosts());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

		// GET: Blog/Create
		[EditorMod]
		public ActionResult Create()
		{
			var user = GetUser();
			ViewBag.Username = user.Username;
			ViewBag.Level = user.Level;

			return View();
        }

		// POST: Blog/Create
		[EditorMod]
		[HttpPost]
        public ActionResult Create(PostViewModel post)
		{
			if (post.Title == null || post.PostContent == null)
				return View();

			var user = System.Web.HttpContext.Current.GetMySessionObject();
			ViewBag.Username = user.Username;
			ViewBag.Level = user.Level;
			try
			{
				var p = Mapper.Map<BlogEntity>(post);
				if (p.ImageUrl == null)
					p.ImageUrl = "/Content/imgs/nothing.png";
				p.AuthorAvatar = user.AvatarUrl;
				p.Date = DateTime.Now;
				p.PostAuthor = user.Username;
				p.UserId = user.Id;

				_blog.TryAddPost(user.Id, p);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

		// GET: Blog/Edit/5
		[EditorMod]
		public ActionResult Edit(int id)
        {
            return View();
        }

		// POST: Blog/Edit/5
		[EditorMod]
		[HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Delete/5
		[AdminMod]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
		[AdminMod]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
