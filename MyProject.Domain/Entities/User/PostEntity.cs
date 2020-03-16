using System;

namespace MyProject.Domain.Entities
{
	public class PostEntity
	{
		public int PostId { get; set; }
		public UserEntity User { get; set; }
		public string Title { get; set; }
		public string PostContent { get; set; }
		public string ImageUrl { get; set; }
		public int Views { get; set; }
		public DateTime Date { get; set; }
		public int IsDeleted { get; set; }
	}
}
