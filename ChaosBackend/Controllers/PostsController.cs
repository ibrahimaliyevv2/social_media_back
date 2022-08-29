using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChaosBackend.DAL;
using ChaosBackend.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChaosBackend.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PostsController:ControllerBase
	{
		private readonly AppDbContext _context;
		public static IWebHostEnvironment _env;


		public PostsController(AppDbContext context, IWebHostEnvironment env)
        {
			_context = context;
			_env = env;
        }

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
        {
			Post post = await _context.Posts.FirstOrDefaultAsync(x=>x.Id==id);

			return StatusCode(200, post);
        }

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> GetAll()
        {
			List<Post> posts = await _context.Posts.ToListAsync();

			return Ok(posts);
        }

		[HttpPost("")]
		public async Task<IActionResult> Create(Post post)
        {

			if(post.files != null){

				string[] mimeTypes = {"image/png", "image/jpeg", "image/jpg"};
				const int maxLeng = 2097152;

				if (Array.IndexOf(mimeTypes,post.files.ContentType) == -1)
				{
					return StatusCode(StatusCodes.Status415UnsupportedMediaType);
				}

				if (post.files.Length > maxLeng)
				{
					return StatusCode(StatusCodes.Status400BadRequest);
				}

				string path = _env.WebRootPath + "/uploads/";

				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				using (FileStream filestream = System.IO.File.Create(path + post.files.FileName))
				{
					
					filestream.CopyTo(filestream);
					filestream.Flush();
				}
			}
            else
            {
				return StatusCode(StatusCodes.Status400BadRequest);
            }
			

			await _context.Posts.AddAsync(post);
			await _context.SaveChangesAsync();

			return StatusCode(StatusCodes.Status201Created, post);
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="post"></param>
		/// <returns></returns>
		/// <response code="204">Entity updated successfully</response>
		/// <response code="400">Model is not valid</response>
		/// <response code="404">Entity is not found by Id</response>
		[HttpPut("")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Update(Post post)
        {
			Post existPost = await _context.Posts.FirstOrDefaultAsync(x => x.Id == post.Id);

			if(existPost == null)
            {
				return NotFound();
            }

			existPost.Username = post.Username;
			existPost.PublisheTime = post.PublisheTime;
			existPost.PostText = post.PostText;
			existPost.LikeCount = post.LikeCount;
			existPost.CommentCount = post.CommentCount;
			existPost.ShareCount = post.ShareCount;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public  IActionResult Delete(int id)
        {
			Post post = _context.Posts.FirstOrDefault(x => x.Id == id);

			if(post == null)
            {
				return NotFound();
            }
			_context.Posts.Remove(post);
			_context.SaveChanges();

			return NoContent();
        }
	}
}

