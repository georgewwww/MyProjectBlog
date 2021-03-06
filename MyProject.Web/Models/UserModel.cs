﻿using MyProject.Domain.Enums;
using System;

namespace MyProject.Web.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public URole Level { get; set; }
		public string AvatarUrl { get; set; }
		public DateTime RegisterDate { get; set; }
	}
}