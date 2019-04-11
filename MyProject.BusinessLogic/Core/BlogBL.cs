using MyProject.BusinessLogic;
using MyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BusinessLogic
{
	public class BlogBL : BlogApi, IBlog
    {
        public BlogEntity GetPostById(int id) => PostById(id);

        public BlogEntity GetFeaturedPost() => FeaturedPost();

		public List<BlogEntity> GetLastPosts() => LastPosts();

        public List<BlogEntity> GetAllPosts() => AllPosts();

        public bool TryEditPost(BlogEntity post)
        {
            return EditPost(post);
        }

        public bool TryDeletePost(int id) => DeletePost(id);

        public bool TryAddPost(int userId, BlogEntity post)
		{
			return AddPost(userId, post);
		}

        public bool IncreasePostViews(int postId)
        {
            return PostViewed(postId);
        }

    }
}
