using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private List<BlogPost> blogPosts = new List<BlogPost>();
        private int nextId = 1;

        // POST: api/blog
        [HttpPost]
        public ActionResult<int> CreateBlogPost([FromBody] BlogPost blogPost)
        {
            if (blogPost == null || string.IsNullOrWhiteSpace(blogPost.Content) || string.IsNullOrWhiteSpace(blogPost.Author))
            {
                return BadRequest("Blog post content and author are required.");
            }

            blogPost.CreationDate = DateTime.Now;
            DatabaseLogin.InsertPost(blogPost);
            return CreatedAtAction(nameof(GetBlogPost), new { blogId = blogPost.Id }, blogPost.Id);
        }

        // GET: api/blog
        [HttpGet]
        public ActionResult<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            blogPosts = DatabaseLogin.GetPost();
            return Ok(blogPosts);
        }

        // GET: api/blog/{blogId}
        [HttpGet("{blogId}")]
        public ActionResult<BlogPost> GetBlogPost(int blogId)
        {
            blogPosts = DatabaseLogin.GetPost(blogId);
            return Ok(blogPosts);
        }

        // DELETE: api/blog/{blogId}
        [HttpDelete("{blogId}")]
        public ActionResult DeleteBlogPost(int blogId)
        {
            return Ok(DatabaseLogin.DeletePost(blogId));
        }

        // PATCH: api/blog/{blogId}
        [HttpPatch("{blogId},{newContent}")]
        public ActionResult PartialUpdateBlogPost(int blogId,string newContent, [FromBody] BlogPost updatedBlogPost)
        {
            return Ok(DatabaseLogin.PatchPost(blogId, newContent));
        }
    }

}
