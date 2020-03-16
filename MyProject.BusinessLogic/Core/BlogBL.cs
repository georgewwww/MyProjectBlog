using MyProject.Domain.Entities;
using System.Collections.Generic;

namespace MyProject.BusinessLogic
{
	public class BlogBL : BlogApi, IBlog
    {
        public PostEntity GetPostById(int id) => PostById(id);

        public PostEntity GetFeaturedPost() => FeaturedPost();

		public List<PostEntity> GetLastPosts() => LastPosts();

        public List<PostEntity> GetAllPosts() => AllPosts();

        public bool TryEditPost(PostEntity post)
        {
            return EditPost(post);
        }

        public bool TryDeletePost(int id) => DeletePost(id);

        public bool TryAddPost(int userId, PostEntity post)
		{
			return AddPost(userId, post);
		}

        public bool IncreasePostViews(int postId)
        {
            return PostViewed(postId);
        }

    }
}
