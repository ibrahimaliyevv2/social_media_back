using System;
namespace ChaosBackend.Data.Entities
{
	public class UserImage
	{
		public int Id { get; set; }
		public string ImageString { get; set; }

		public int UserId { get; set; }
		public AppUser User { get; set; }
	}
}

