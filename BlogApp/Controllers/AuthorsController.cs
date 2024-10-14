using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace BlogApp.Controllers
{
    [Route("authors")]
    [ApiController]
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }

        [HttpGet("details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return BadRequest("ID é obrigatório."); // 400 Bad Request

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound("Autor não encontrado."); // 404 Not Found

            return View(author); // 200 OK
        }


        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email")] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request

            try
            {
                author.Id = Guid.NewGuid();
                _context.Add(author);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = author.Id }, author); // 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar autor: {ex.Message}"); // 500 Internal Server Error
            }
            
        }

        [HttpGet("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return BadRequest("ID é obrigatório."); // 400 Bad Request

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound("Autor não encontrado."); // 404 Not Found

            return View(author); // 200 OK
        }


        [HttpPut("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Email")] Author updatedAuthor)
        {
            if (id != updatedAuthor.Id)
                return BadRequest("ID do autor não corresponde."); // 400 Bad Request

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request

            try
            {

                var existingAuthor = await _context.Authors.FindAsync(id);
                if (existingAuthor == null)
                    return NotFound("Autor não encontrado."); // 404 Not Found

                existingAuthor.UpdateAuthor(updatedAuthor.Name, updatedAuthor.Email);
                _context.Update(existingAuthor);
                await _context.SaveChangesAsync();
                return NoContent(); // 204 No Content

            }
            catch (DbUpdateConcurrencyException)
            {

                if (!AuthorExists(updatedAuthor.Id))
                    return NotFound("Autor não encontrado."); // 404 Not Found

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao atualizar autor: {ex.Message}"); // 500 Internal Server Error

            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {

            if (id == null)
            {

                return BadRequest("ID é obrigatório.");

            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);

            if (author == null)
            {

                return NotFound("Autor não encontrado.");

            }

            return View(author);
        }


        [HttpDelete("delete/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            try
            {

                var author = await _context.Authors.FindAsync(id);
                if (author == null) return NotFound("Autor não encontrado.");

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Autor excluído com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao excluir autor: {ex.Message}");
                
            }
        }

        private bool AuthorExists(Guid id)
        {

            return _context.Authors.Any(e => e.Id == id);

        }
    }
}
