using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyProject.Domain.Enums;

namespace MyProject.Domain.Entities
{
	public class UsersDbTable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[Display(Name = "Username")]
		[StringLength(30, MinimumLength = 4, ErrorMessage = "Username must be more than 4 and less than 30 characters")]
		public string Username { get; set; }

		[Required]
		[Display(Name = "Password")]
		[StringLength(40, MinimumLength = 4, ErrorMessage = "Password must be more than 4 and less than 40 characters")]
		public string Password { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email Address")]
		[StringLength(30)]
		public string Email { get; set; }

		public URole Level { get; set; }

		[StringLength(100)]
		public string AvatarUrl { get; set; }

		[DataType(DataType.Date)]
		public DateTime RegisterDate { get; set; }
	}
}
