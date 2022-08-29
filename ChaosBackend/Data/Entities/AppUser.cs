using System;
using System.Collections.Generic;
using ChaosBackend.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChaosBackend.Data.Entities
{
	public class AppUser:IdentityUser
	{
		public string FullName { get; set; }
		public string City { get; set; }

		public List<Post> Posts { get; set; }
	}
}

