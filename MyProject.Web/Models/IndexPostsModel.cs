using System.Collections.Generic;

namespace MyProject.Web.Models
{
	public class IndexPostsModel
    {
        public PostModel FeaturedPost { get; set; }
        public List<PostModel> LastPosts { get; set; }
    }
}