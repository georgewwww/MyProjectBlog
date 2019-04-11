using System.Collections.Generic;
using MyProject.Domain.Entities;

namespace MyProject.Web.Models
{
    public class IndexPostsViewModel
    {
        public BlogEntity featuredPost { get; set; }
        public List<BlogEntity> lastPosts { get; set; }
    }
}