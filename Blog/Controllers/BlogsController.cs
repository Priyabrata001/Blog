using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blogs.Data;
using Blogs.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogsController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Blogs.Models.Blog>>> GetBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs.Models.Blog>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blogs.Models.Blog blog)
        {

            if (id != blog.Idblog)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blogs.Models.Blog>> PostBlog(Blogs.Models.Blog blog)
        {
           // var blogs
            blog.UserId = GetidFromToken();
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.Idblog }, blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("getmypost")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> getMypost()
        {
            int myid = GetidFromToken();
            var myblogs=  _context.Blogs.Where(t=>t.UserId==myid).ToList();
            return Ok(myblogs);
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Idblog == id);
        }
        private int GetidFromToken()
        {
            var id = HttpContext.User.FindFirst("UserId").Value;
            return int.Parse(id);
        }
    }
}
