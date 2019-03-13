using MyProject.Domain.Entities;
using System.Web;

namespace MyProject.Web.Extension
{
	public static class HttpContextExtensions
	{
		public static UserEntity GetMySessionObject(this HttpContext current)
		{
			return (UserEntity)current?.Session["__SessionObject"];
		}

		public static void SetMySessionObject(this HttpContext current, UserEntity profile)
		{
			current.Session.Add("__SessionObject", profile);
		}
	}
}