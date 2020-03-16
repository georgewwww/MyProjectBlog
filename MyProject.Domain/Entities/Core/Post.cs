using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Domain.Entities.Core
{
	public class Post
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PostId { get; set; }

		[Required]
		public User User { get; set; }

		[Required]
		[StringLength(50)]
		public string Title { get; set; }

		[Required]
		[DataType(DataType.Html)]
		public string PostContent { get; set; }

		[Required]
		[StringLength(150)]
		public string ImageUrl { get; set; }

		[Required]
		public int Views { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Required]
		public int IsDeleted { get; set; }
	}
}
