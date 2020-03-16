using AutoMapper;
using MyProject.Domain.Entities;
using MyProject.Domain.Entities.Core;
using MyProject.Web.Models;

namespace MyProject.Web
{
	public class AutoMapperConfig
	{
		public static MapperConfiguration Initialize()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<UserLoginModel, ULoginData>();
				cfg.CreateMap<User, UserEntity>();
				cfg.CreateMap<UserEntity, UserData>();
				cfg.CreateMap<UserRegisterModel, URegisterData>();
				cfg.CreateMap<URegisterData, User>();
				cfg.CreateMap<Post, PostEntity>();
				cfg.CreateMap<PostEntity, Post>();
				cfg.CreateMap<PostViewModel, PostEntity>();
				cfg.CreateMap<PostModel, PostEntity>().ReverseMap();
				cfg.CreateMap<UserModel, UserEntity>().ReverseMap();
			});
			return config;
		}
	}
}