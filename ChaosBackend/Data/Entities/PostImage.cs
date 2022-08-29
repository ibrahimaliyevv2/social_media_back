using System;
using ChaosBackend.Entities;

namespace ChaosBackend.Data.Entities
{
	public class PostImage
	{
		public int Id { get; set; }
		public string ImageString { get; set; }

		public int PostId { get; set; }
		public Post Post { get; set; }
	}
}

