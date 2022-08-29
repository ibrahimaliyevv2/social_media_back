using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ChaosBackend.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace ChaosBackend.Entities
{
	public class Post
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public DateTime PublisheTime { get; set; }
		public string PostText { get; set; }
		public int LikeCount { get; set; }
		public int CommentCount { get; set; }
		public int ShareCount { get; set; }

		public int UserId { get; set; }
		public AppUser User { get; set; }

		public List<PostComment> Comments { get; set; }

		[NotMapped]
		public IFormFile files { get; set; }
	}
}

