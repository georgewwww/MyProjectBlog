using MyProject.Domain.Entities.Core;
using System.Data.Entity;

namespace MyProject.BusinessLogic
{
	public class BlogContext : DbContext
	{
		public BlogContext() : base("name=MyProject")
		{
		}

		public virtual DbSet<Post> Posts { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Session> Sessions { get; set; }
	}
}
