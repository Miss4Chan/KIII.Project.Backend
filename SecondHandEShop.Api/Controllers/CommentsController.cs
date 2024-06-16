using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;

namespace SecondHandEShop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("addComment")]
        public IActionResult AddComment([FromBody] CommentDTO commentDTO, [FromQuery] int rating)
        {
            try
            {
                var addedComment = _commentService.AddComment( commentDTO, rating);
                return Ok(addedComment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            var isDeleted = _commentService.DeleteComment(commentId);

            if (isDeleted)
            {
                return Ok("Comment deleted successfully.");
            }

            return NotFound("Comment not found.");
        }

        [HttpGet("{receiverUsername}")]
        public IActionResult GetCommentsByReceiverUsername(string receiverUsername)
        {
            try
            {
                var comments = _commentService.GetCommentsByReceiverUsername(receiverUsername);
                return Ok(comments);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
