using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {

            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Comments.ToListAsync());

        }

        [HttpGet("details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {

                return BadRequest("ID é obrigatório.");

            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comment == null)
            {

                return NotFound("Comentário não encontrado.");

            }

            return View(comment);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {

            return View();

        }


        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content, Author")] Comment comment)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request

            try
            {
                var author = await _context.Authors.FindAsync(comment.Author);
                if (author == null)
                {
                    ModelState.AddModelError("Author", "Autor não encontrado.");
                    return BadRequest(ModelState); // 400 Bad Request
                }

                comment.Author = author;
                _context.Add(comment);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Details), new { id = comment.Id }, comment); // 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar comentário: {ex.Message}"); // 500 Internal Server Error
            }
            return View(comment);
        }

        [HttpGet("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {

                return BadRequest("ID é obrigatório.");

            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Comentário não encontrado.");
            }
            return View(comment);
        }


        [HttpPut("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Content,Id")] Comment updatedComment)
        {
            if (id != updatedComment.Id)
            {
                return BadRequest("ID do comentário não corresponde.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request

            try
            {
                var existingComment = await _context.Comments.FindAsync(id);
                if (existingComment == null)
                    return NotFound("Comentário não encontrado."); // 404 Not Found

                existingComment.Content = updatedComment.Content;


                _context.Update(existingComment);
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(updatedComment.Id))
                    return NotFound("Comentário não encontrado."); // 404 Not Found

                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar comentário: {ex.Message}"); // 500 Internal Server Error
            }

            return View(updatedComment);
        }

        [HttpGet("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {

                return BadRequest("ID é obrigatório.");

            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {

                return NotFound("Comentário não encontrado.");

            }

            return View(comment);
        }


        [HttpPost("delete/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(id);
                if (comment == null)
                    return NotFound("Comentário não encontrado."); // 404 Not Found

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir comentário: {ex.Message}"); // 500 Internal Server Error
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
