using System.Web;
using MyProject.BusinessLogic.DbModel;
using MyProject.Domain.Entities;

namespace MyProject.BusinessLogic
{
	public class SessionBL : UserApi, ISession
	{
		public UActionResp UserLogin(ULoginData data)
		{
			return UserLoginAction(data);
		}
		public UActionResp UserLogout(string cookie)
		{
			return UserLogoutAction(cookie);
		}
		public HttpCookie GenCookie(string username)
		{
			return Cookie(username);
		}
		public UserEntity GetUserByCookie(string apiCookieValue)
		{
			return UserCookie(apiCookieValue);
		}
		public UActionResp UserRegister(URegisterData data)
		{
			return UserRegisterAction(data);
		}
	}
}
