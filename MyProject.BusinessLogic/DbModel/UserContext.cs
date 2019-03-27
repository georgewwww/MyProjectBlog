using MyProject.Domain.Entities;
using System.Data.Entity;

namespace MyProject.BusinessLogic
{
	public class UserContext : DbContext
	{
		public UserContext() :
			base("name = MyProjectDB")
		{
		}

		public virtual DbSet<UsersDbTable> Users { get; set; }
	}
}
