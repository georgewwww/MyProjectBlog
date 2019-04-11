using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Models
{
	public class PostViewModel
	{
        public int PostId { get; set; }
		public string Title { get; set; }
        [AllowHtml]
		public string PostContent { get; set; }
		public string ImageUrl { get; set; }
	}
}