using MyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BusinessLogic.DbModel
{
	public class SessionContext : DbContext
	{
		public SessionContext() : base("name=MyProjectDB")
		{
		}

		public virtual DbSet<SessionDbTable> Sessions { get; set; }
	}
}
