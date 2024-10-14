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
    [Route("posts")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Posts.ToListAsync());

        }

        [HttpGet("details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {

                BadRequest("ID é obrigatório.");

            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
              
                NotFound("Post não encontrado."); 

            }

            return View(post);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {

            return View();

        }


        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,AuthorId,Created,Updated")] Post post)
        {

            if (ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var author = await _context.Authors.FindAsync(post.Author);

                if (author == null)
                {

                    return NotFound("Autor não encontrado.");

                }

                post.Author = author;  // Associa o autor ao post
                post.Created = post.Updated = DateTime.UtcNow;
                
                _context.Add(post);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Details), new { id = post.Id }, post);

            }
            catch (Exception exception)
            {

                return StatusCode(500, $"Erro interno: {exception.Message}");

            }
            
            return RedirectToAction(nameof(Index));

            
        }

        [HttpGet("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {

                return BadRequest("ID é obrigatório.");

            }

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {

                return NotFound("Post não encontrado.");

            }
            return View(post);
        }


        [HttpPut("edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Content,Id")] Post updatedPost)
        {
            if (id != updatedPost.Id)
            {

                return BadRequest("ID do post não corresponde.");

            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (ModelState.IsValid)
            {
                try
                {
                    var existingPost = await _context.Posts.FindAsync(updatedPost.Id);

                    if (existingPost == null)
                    {

                        return NotFound("Post não encontrado.");

                    }

                    existingPost.Title = updatedPost.Title;
                    existingPost.Content = updatedPost.Content;
                    existingPost.Updated = DateTime.UtcNow;


                    _context.Update(existingPost);
                    await _context.SaveChangesAsync();

                    return NoContent();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(updatedPost.Id))
                    {
                        return NotFound("Post não encontrado.");
                    }
                    else
                    {
                        throw;
                    }
                }

                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro interno: {ex.Message}"); // 500 Internal Server Error
                }

                return View(updatedPost);

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

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound("Post não encontrado.");
            }

            return View(post);
        }


        [HttpPost("delete/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null)
                {

                    return NotFound("Post não encontrado.");

                }
                
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception exception)
            {
               return StatusCode(500, $"Erro interno: {exception.Message}");
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }


    }
}
