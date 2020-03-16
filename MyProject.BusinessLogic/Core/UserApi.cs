using MyProject.BusinessLogic.DbModel;
using MyProject.Domain.Entities;
using MyProject.Helpers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using MyProject.Domain.Enums;
using System.Data.Entity.Validation;

namespace MyProject.BusinessLogic
{
	public class UserApi
	{
		private readonly IMapper _mapper;
		public UserApi()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<UsersDbTable, UserEntity>().ReverseMap();
				cfg.CreateMap<URegisterData, UsersDbTable>().ReverseMap();
			});
			_mapper = new Mapper(config);
		}

		// Gen an response from login action
		internal UActionResp UserLoginAction(ULoginData data)
		{
			UsersDbTable result;

			using (var db = new UserContext())
			{
				result = db.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == data.Password);
			}

			if (result == null)
			{
				return new UActionResp { Status = false, StatusMsg = "The username or password is incorrect" };
			}

			return new UActionResp { Status = true };
		}

		// Just logout the current user, if there is any
		internal UActionResp UserLogoutAction(string cookie)
		{
			SessionDbTable result;

			using (var db = new SessionContext())
			{
				result = db.Sessions.FirstOrDefault(s => s.CookieString == cookie);
				if (result != null)
				{
					result.ExpireTime = DateTime.Now.AddHours(-1);
					db.SaveChanges();
					return new UActionResp { Status = true };
				}
			}
			return new UActionResp { Status = false, StatusMsg = "User already signed out" };
		}

		// Insert new entry of user from register page
		internal UActionResp UserRegisterAction(URegisterData data)
		{
			UsersDbTable result;

			using (var db = new UserContext())
			{
				result = db.Users.FirstOrDefault(u => u.Username == data.Username);
				if (result == null)
				{
					var newUser = _mapper.Map<UsersDbTable>(data);
					newUser.Level = URole.User;
					newUser.AvatarUrl = "/Content/imgs/default_avatar.png";

					db.Users.Add(newUser);
					try
					{
						db.SaveChanges();
					}
					catch (DbEntityValidationException ex)
					{
						// Retrieve the error messages as a list of strings.
						var errorMessages = ex.EntityValidationErrors
								.SelectMany(x => x.ValidationErrors)
								.Select(x => x.ErrorMessage);

						// Join the list to a single string.
						var fullErrorMessage = string.Join(Environment.NewLine, errorMessages);
						return new UActionResp { Status = false, StatusMsg = fullErrorMessage };
					}
					return new UActionResp { Status = true };
				}
			}
			return new UActionResp { Status = false, StatusMsg = "This user already exists" };
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
						ExpireTime = DateTime.Now.AddDays(1)
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
            var user = _mapper.Map<UserEntity>(curentUser);

            return user;
        }

        internal void UserNewMail(int id, string email)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.Email = email;
                    db.SaveChanges();
                }
            }
        }

        internal void UserNewPassword(int id, string oldPass, string pass)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id && u.Password == oldPass);
                if (user != null)
                {
                    user.Password = pass;
                    db.SaveChanges();
                }
            }
        }

    }
}
