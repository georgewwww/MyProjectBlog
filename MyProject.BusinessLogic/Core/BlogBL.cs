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
		public BlogEntity GetFeaturedPost() => FeaturedPost();

		public List<BlogEntity> GetLastPosts() => LastPosts();

		public List<BlogEntity> GetAllPosts() => AllPosts();

		public bool TryAddPost(int userId, BlogEntity post)
		{
			return AddPost(userId, post);
		}
		
	}
}
