using System.Web;
using MyProject.BusinessLogic.DbModel;
using MyProject.Domain.Entities;

namespace MyProject.BusinessLogic
{
	public class SessionBL : UserApi, ISession
	{
		public ULoginResp UserLogin(ULoginData data)
		{
			return UserLoginAction(data);
		}
		public HttpCookie GenCookie(string username)
		{
			return Cookie(username);
		}
		public UserEntity GetUserByCookie(string apiCookieValue)
		{
			return UserCookie(apiCookieValue);
		}
	}
}
