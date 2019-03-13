using MyProject.BusinessLogic.DbModel;
using MyProject.Domain.Entities;
using MyProject.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;

namespace MyProject.BusinessLogic
{
	public class UserApi
	{
		// Gen an response from login action
		internal ULoginResp UserLoginAction(ULoginData data)
		{
			UsersDbTable result;

			using (var db = new UserContext())
			{
				result = db.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == data.Password);
			}

			if (result == null)
			{
				return new ULoginResp { Status = false, StatusMsg = "The username or password is incorrect" };
			}

			return new ULoginResp { Status = true };
		}

		// Generate the cookie and put in database
		internal HttpCookie Cookie(string username)
		{
			var apiCookie = new HttpCookie("LOGIN-KEY")
			{
				Value = CookieGenerator.Create(username)
			};

			using (var db = new SessionContext())
			{
				SessionDbTable curent;

				curent = (from e in db.Sessions where e.Username == username select e).FirstOrDefault();

				if (curent != null)
				{
					curent.CookieString = apiCookie.Value;
					curent.ExpireTime = DateTime.Now.AddDays(1);
					using (var todo = new SessionContext())
					{
						todo.Entry(curent).State = EntityState.Modified;
						todo.SaveChanges();
					}
				}
				else
				{
					db.Sessions.Add(new SessionDbTable
					{
						Username = username,
						CookieString = apiCookie.Value,
						ExpireTime = DateTime.Now.AddMinutes(60)
					});
					db.SaveChanges();
				}
			}

			return apiCookie;
		}

		// Get the user data based on it's cookie
        internal UserEntity UserCookie(string cookie)
        {
            SessionDbTable session;
            UsersDbTable curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
            }

            if (curentUser == null) return null;
            var user = Mapper.Map<UserEntity>(curentUser);

            return user;
        }
	}
}
