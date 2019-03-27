using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyProject.API.Models;
using MyProject.Domain.Entities;

namespace MyProject.API
{
	public class AutoMapperConfig
	{
		public static void Initialize()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<UsersDbTable, User>();
			});
		}
	}
}