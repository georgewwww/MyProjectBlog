using MyProject.Domain.Entities;
using System.Collections.Generic;

namespace MyProject.BusinessLogic
{
	public interface IBlog
    {
        BlogEntity GetPostById(int id);
        BlogEntity GetFeaturedPost();
		List<BlogEntity> GetLastPosts();
		List<BlogEntity> GetAllPosts();
        bool TryAddPost(int userId, BlogEntity post);
        bool TryEditPost(BlogEntity post);
        bool TryDeletePost(int id);
        bool IncreasePostViews(int postId);
    }
}
