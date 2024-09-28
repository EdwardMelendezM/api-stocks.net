using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interface;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository; 

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }
      
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            var commentDto = comments.Select(S => S.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto commentDto, int stockId)
        {
            if (!await _stockRepository.StockExists(stockId)){
                return BadRequest("Stock does not exist");
            }
            var comment = commentDto.ToCommentFromCreate(stockId);
            await _commentRepository.CreateComment(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            comment = updateCommentDto.ToCommentFromUpdate(comment);
            await _commentRepository.UpdateComment(comment);
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound("Comment does not exist");
            }
            await _commentRepository.DeleteComment(comment);
            return NoContent();
        }
    }
}