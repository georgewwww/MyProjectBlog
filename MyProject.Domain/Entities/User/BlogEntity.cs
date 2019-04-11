using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Domain.Entities
{
	public class BlogEntity
	{
		public int PostId { get; set; }
		public int UserId { get; set; }
		public string Title { get; set; }
		public string PostContent { get; set; }
		public string ImageUrl { get; set; }
		public int Views { get; set; }
		public DateTime Date { get; set; }
		public int IsDeleted { get; set; }

		// Additional
		public string PostAuthor { get; set; }
		public string AuthorAvatar { get; set; }
	}
}
