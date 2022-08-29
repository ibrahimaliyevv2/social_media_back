using System;
using ChaosBackend.Data.Entities;

namespace ChaosBackend.Entities
{
	public class PostComment
	{
		public int Id { get; set; }
		public DateTime PublishTime { get; set; }
		public int LikeCount { get; set; }
		public string CommentText { get; set; }

		public int UserId { get; set; }
		public AppUser User { get; set; }

		public int PostId { get; set; }
        public Post Post { get; set; }
	}
}

