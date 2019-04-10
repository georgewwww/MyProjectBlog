﻿using System;
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
				cfg.CreateMap<UserEntity, UserData>();
				cfg.CreateMap<UserRegister, URegisterData>();
				cfg.CreateMap<URegisterData, UsersDbTable>();
				cfg.CreateMap<BlogDbTable, BlogEntity>();
				cfg.CreateMap<BlogEntity, BlogDbTable>();
				cfg.CreateMap<PostViewModel, BlogEntity>();
			});
		}
	}
}