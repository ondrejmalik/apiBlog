using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
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

            blogPost.Id = nextId++;
            blogPost.CreationDate = DateTime.Now;

            blogPosts.Add(blogPost);
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
            var blogPost = blogPosts.Find(b => b.Id == blogId);

            if (blogPost == null)
            {
                return NotFound("Blog post not found.");
            }

            return Ok(blogPost);
        }

        // DELETE: api/blog/{blogId}
        [HttpDelete("{blogId}")]
        public ActionResult DeleteBlogPost(int blogId)
        {
            var blogPost = blogPosts.Find(b => b.Id == blogId);

            if (blogPost == null)
            {
                return NotFound("Blog post not found.");
            }

            blogPosts.Remove(blogPost);

            return NoContent();
        }

        // PATCH: api/blog/{blogId}
        [HttpPatch("{blogId}")]
        public ActionResult PartialUpdateBlogPost(int blogId, [FromBody] BlogPost updatedBlogPost)
        {
            var blogPost = blogPosts.Find(b => b.Id == blogId);

            if (blogPost == null)
            {
                return NotFound("Blog post not found.");
            }

            // Update only non-null properties
            if (updatedBlogPost.Content != null)
            {
                blogPost.Content = updatedBlogPost.Content;
            }

            if (updatedBlogPost.Author != null)
            {
                blogPost.Author = updatedBlogPost.Author;
            }

            return Ok(blogPost);
        }
    }

}
