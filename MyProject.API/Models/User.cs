﻿using MyProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.API.Models
{
    public class User
    {
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public URole? Level { get; set; }
		public string AvatarUrl { get; set; }
		public DateTime RegisterDate { get; set; }
	}
}