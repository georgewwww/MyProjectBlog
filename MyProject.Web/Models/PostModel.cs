using System;

namespace MyProject.Web.Models
{
	public class PostModel
	{
		public int PostId { get; set; }
		public UserModel User { get; set; }
		public string Title { get; set; }
		public string PostContent { get; set; }
		public string ImageUrl { get; set; }
		public int Views { get; set; }
		public DateTime Date { get; set; }
	}
}