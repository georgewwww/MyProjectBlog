using AutoMapper;
using MyProject.BusinessLogic.DbModel;
using MyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BusinessLogic
{
	public class BlogApi
	{
		internal BlogEntity FeaturedPost()
		{
			BlogDbTable currentPost;
			using (var db = new BlogContext())
			{
				currentPost = db.Posts.OrderByDescending(p => p.Views).Where(x => x.IsDeleted == 0).First();
			}

			if (currentPost == null) return null;

			var post = Mapper.Map<BlogEntity>(currentPost);
			var author = GetUserByPosterId(post.UserId);
			post.PostAuthor = author.Username;
			post.AuthorAvatar = author.AvatarUrl;
			
			return post;
		}

		internal List<BlogEntity> LastPosts()
		{
			List<BlogDbTable> blogPosts;
			using (var db = new BlogContext())
			{
				int toTake = db.Posts.Count() > 3 ? 3 : db.Posts.Count();
				blogPosts = db.Posts
					.OrderByDescending(p => p.PostId)
					.Where(x => x.IsDeleted == 0)
					.Take(toTake)
					.ToList();
			}

			if (blogPosts == null) return null;

			var posts = new List<BlogEntity>();
			foreach(var p in blogPosts)
			{
				var post = Mapper.Map<BlogEntity>(p);
				var author = GetUserByPosterId(p.UserId);
				post.PostAuthor = author.Username;
				post.AuthorAvatar = author.AvatarUrl;
				posts.Add(post);
			}

			return posts;
		}

		internal List<BlogEntity> AllPosts()
		{
			List<BlogDbTable> blogPosts;
			using (var db = new BlogContext())
			{
				blogPosts = db.Posts
					.OrderByDescending(p => p.PostId)
					.ToList();
			}

			if (blogPosts == null) return null;

			var posts = new List<BlogEntity>();
			foreach (var p in blogPosts)
			{
				var post = Mapper.Map<BlogEntity>(p);
				var author = GetUserByPosterId(p.UserId);
				post.PostAuthor = author.Username;
				post.AuthorAvatar = author.AvatarUrl;
				posts.Add(post);
			}

			return posts;
		}

		internal bool AddPost(int userId, BlogEntity post)
		{
			var user = GetUserByPosterId(userId);
			if (post != null && user != null)
			{
				var p = Mapper.Map<BlogDbTable>(post);

				using (var db = new BlogContext())
				{
					db.Posts.Add(p);
					db.SaveChanges();
					return true;
				}
			}
			return false;
		}

		private UsersDbTable GetUserByPosterId(int id)
		{
			using (var db = new UserContext())
			{
				return db.Users.SingleOrDefault(u => u.Id == id);
			}
		}
	}
}
