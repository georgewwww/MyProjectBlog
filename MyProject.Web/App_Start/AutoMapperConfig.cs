using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyProject.Domain.Entities;
using MyProject.Web.Models;

namespace MyProject.Web
{
	public class AutoMapperConfig
	{
		public static void Initialize()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<UserLogin, ULoginData>();
				cfg.CreateMap<UsersDbTable, UserEntity>();
			});
		}
	}
}