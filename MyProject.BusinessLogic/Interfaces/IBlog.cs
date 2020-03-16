using MyProject.Domain.Entities;
using System.Collections.Generic;

namespace MyProject.BusinessLogic
{
	public interface IBlog
    {
        PostEntity GetPostById(int id);
        PostEntity GetFeaturedPost();
		List<PostEntity> GetLastPosts();
		List<PostEntity> GetAllPosts();
        bool TryAddPost(int userId, PostEntity post);
        bool TryEditPost(PostEntity post);
        bool TryDeletePost(int id);
        bool IncreasePostViews(int postId);
    }
}
