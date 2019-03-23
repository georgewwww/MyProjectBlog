﻿using MyProject.Domain.Entities;
using System.Web;

namespace MyProject.BusinessLogic
{
	public interface ISession
	{
		UActionResp UserLogin(ULoginData data);
		UActionResp UserLogout(string cookie);
		UActionResp UserRegister(URegisterData data);
		HttpCookie GenCookie(string username);
		UserEntity GetUserByCookie(string apiCookieValue);
	}
}
