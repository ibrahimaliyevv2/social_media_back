using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosBackend.DAL;
using ChaosBackend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ChaosBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostCommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostCommentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            PostComment comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            return StatusCode(200, comment);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            List<PostComment> comments = await _context.Comments.ToListAsync();

            return Ok(comments);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(PostComment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, comment);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        /// <response code="204">Entity updated successfully</response>
		/// <response code="400">Model is not valid</response>
		/// <response code="404">Entity is not found by Id</response>
        [HttpPut("")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(PostComment comment)
        {
            PostComment existComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == comment.Id);

            if (existComment == null)
            {
                return NotFound();
            }

            existComment.PublishTime = comment.PublishTime;
            existComment.LikeCount = comment.LikeCount;
            existComment.CommentText = comment.CommentText;
            existComment.UserId = comment.UserId;
            existComment.PostId = comment.PostId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PostComment comment = _context.Comments.FirstOrDefault(x => x.Id == id);

            if (comment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

