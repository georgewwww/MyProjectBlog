using MyProject.Domain.Entities;
using System.Collections.Generic;

namespace MyProject.BusinessLogic
{
	public interface IBlog
	{
		BlogEntity GetFeaturedPost();
		List<BlogEntity> GetLastPosts();
		List<BlogEntity> GetAllPosts();
		bool TryAddPost(int userId, BlogEntity post);
	}
}
