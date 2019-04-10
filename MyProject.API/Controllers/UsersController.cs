using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyProject.API.Models;
using MyProject.BusinessLogic;
using AutoMapper;
using MyProject.Domain.Entities;
using System.Data.Entity.Infrastructure;

namespace MyProject.API.Controllers
{
    public class UsersController : ApiController
    {
		private UserContext db { get; }

		public UsersController()
		{
			db = new UserContext();
		}

		// get all users
		// GET api/users
		[HttpGet]
		public IEnumerable<UsersDbTable> GetAllUsers()
		{
			return db.Users.AsEnumerable();
		}

		// get user by id
		// GET api/users/1
		public UsersDbTable GetUser(int id)
		{
			var user = db.Users.Find(id);
			if (user == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}
			return user;
		}

		// add new user  
		// POST api/values  
		//public void Post(UsersDbTable user)
		//{
		//}

		// update user
		// PUT api/users/1
		//public HttpResponseMessage Put(int id, string email)
		//{
		//}

		// delete customer by id
		// DELETE api/users/1
		//[HttpDelete, Route("{id}")]
		//public HttpResponseMessage Delete(int id)
		//{
		//}
	}
}
