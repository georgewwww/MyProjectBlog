using AutoMapper;
using MyProject.BusinessLogic.DbModel;
using MyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyProject.BusinessLogic
{
	public class BlogApi
	{
        internal BlogEntity PostById(int id)
        {
            BlogDbTable currentPost;
            using (var db = new BlogContext())
            {
                currentPost = db.Posts.FirstOrDefault(x => x.PostId == id && x.IsDeleted == 0);
                if (currentPost == null)
                    return null;
            }

            var post = Mapper.Map<BlogEntity>(currentPost);
            var author = GetUserByPosterId(post.UserId);
            post.PostAuthor = author.Username;
            post.AuthorAvatar = author.AvatarUrl;

            return post;
        }

        internal BlogEntity FeaturedPost()
		{
			BlogDbTable currentPost;
			using (var db = new BlogContext())
			{
				currentPost = db.Posts.OrderByDescending(p => p.Views).First(x => x.IsDeleted == 0);
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
                    .Where(p => p.IsDeleted == 0)
					.ToList();
			}

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

        internal bool EditPost(BlogEntity post)
        {
            var result = PostExistById(post.PostId);
            if (result == true)
            {
                using (var db = new BlogContext())
                {
                    var currentPost = db.Posts.SingleOrDefault(p => p.PostId == post.PostId && p.IsDeleted == 0);
                    if (currentPost == null)
                    {
                        return false;
                    }
                    currentPost.Title = post.Title;
                    currentPost.ImageUrl = post.ImageUrl;
                    currentPost.PostContent = HttpUtility.HtmlDecode(post.PostContent);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        internal bool DeletePost(int postId)
        {
            if (postId != null)
            {
                using (var db = new BlogContext())
                {
                    var post = db.Posts.First(p => p.PostId == postId);
                    post.IsDeleted = 1;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        internal bool PostViewed(int id)
        {
            using (var db = new BlogContext())
            {
                var post = db.Posts.FirstOrDefault(p => p.PostId == id && p.IsDeleted == 0);
                if (post != null)
                {
                    post.Views++;
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

        private bool PostExistById(int id)
        {
            using (var db = new BlogContext())
            {
                var post = db.Posts.SingleOrDefault(p => p.PostId == id);
                return post != null;
            }
        }
	}
}
