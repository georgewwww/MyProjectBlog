using MyProject.Domain.Entities;
using System.Web;

namespace MyProject.BusinessLogic
{
	public interface ISession
	{
		ULoginResp UserLogin(ULoginData data);
		HttpCookie GenCookie(string username);
		UserEntity GetUserByCookie(string apiCookieValue);
	}
}
