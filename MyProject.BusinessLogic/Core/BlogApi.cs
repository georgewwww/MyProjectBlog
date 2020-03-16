using AutoMapper;
using MyProject.Domain.Entities;
using MyProject.Domain.Entities.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyProject.BusinessLogic
{
	public class BlogApi
	{
		private readonly IMapper _mapper;
		public BlogApi()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<PostEntity, Post>().ReverseMap();
				cfg.CreateMap<UserEntity, User>().ReverseMap();
			});

			_mapper = new Mapper(config);
		}

        internal PostEntity PostById(int id)
        {
            Post currentPost;
            using (var db = new BlogContext())
            {
				currentPost = db.Posts.Where(x => x.PostId == id && x.IsDeleted == 0).Include(p => p.User).SingleOrDefault();
                if (currentPost == null)
                    return null;
            }

            return _mapper.Map<PostEntity>(currentPost);
        }

        internal PostEntity FeaturedPost()
		{
			Post currentPost = null;
			using (var db = new BlogContext())
			{
				if (db.Posts.Any())
				{
					currentPost = db.Posts.OrderByDescending(p => p.Views).Include(p => p.User).First(x => x.IsDeleted == 0);
				}
			}

			if (currentPost == null) return null;

			return _mapper.Map<PostEntity>(currentPost);
		}

		internal List<PostEntity> LastPosts()
		{
			List<Post> blogPosts;
			using (var db = new BlogContext())
			{
				int toTake = db.Posts.Count() > 3 ? 3 : db.Posts.Count();
				blogPosts = db.Posts
					.OrderByDescending(p => p.PostId)
					.Where(x => x.IsDeleted == 0)
					.Take(toTake)
					.Include(p => p.User)
					.ToList();
			}

			return _mapper.Map<List<PostEntity>>(blogPosts);
		}

		internal List<PostEntity> AllPosts()
		{
			List<Post> blogPosts;
			using (var db = new BlogContext())
			{
				blogPosts = db.Posts
					.OrderByDescending(p => p.PostId)
                    .Where(p => p.IsDeleted == 0)
					.Include(p => p.User)
					.ToList();
			}

			return _mapper.Map<List<PostEntity>>(blogPosts);
		}

        internal bool AddPost(int userId, PostEntity post)
        {
            var user = GetUserByPosterId(userId);
            if (post != null && user != null)
            {
                var p = _mapper.Map<Post>(post);
				p.User = user;

                using (var db = new BlogContext())
                {
                    db.Posts.Add(p);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        internal bool EditPost(PostEntity post)
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
	        if (postId < 0) return false;
	        using (var db = new BlogContext())
            {
	            var post = db.Posts.First(p => p.PostId == postId);
	            post.IsDeleted = 1;
	            db.SaveChanges();
	            return true;
            }
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

        private User GetUserByPosterId(int id)
		{
			using (var db = new BlogContext())
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
