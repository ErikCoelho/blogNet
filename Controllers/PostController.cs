using Blog.Models;
using BlogNet.Data;
using BlogNet.ViewModels;
using BlogNet.ViewModels.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogNet.Controllers
{
    [ApiController]
    public class PostController: ControllerBase
    {
        [HttpGet("v1/posts")]
        public async Task<IActionResult> GetAsync (
            [FromServices] BlogDataContext context)
        {
            try
            {
                var posts = await context
                    .Posts
                    .AsNoTracking()
                    .Select(x =>
                    new {
                        x.Id,
                        x.ImageUrl,
                        x.Title,
                        x.LastUpdateDate
                    })
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(posts));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Post>>("02X01 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/post/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(new ResultViewModel<Post>(post));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("02X02 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/posts/category/{category}")]
        public async Task<IActionResult> GetCategoryAsync(
            [FromRoute] string category,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var posts = await context
                    .Posts
                    .Where(x => x.Category == category)
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(posts));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Post>>("02X03 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/posts/user")]
        public async Task<IActionResult> GetByUserAsync(
            [FromServices] BlogDataContext context)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                var posts = await context
                    .Posts
                    .Where(x => x.Author == user)
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToListAsync();

                if (posts == null)
                    return NotFound(new ResultViewModel<Post>("Você não possui nenhuma publicação"));

                return Ok(new ResultViewModel<dynamic>(posts));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Post>>("02X04 - Falha interna no servidor"));
            }

        }

        [HttpPost("v1/posts")]
        [Authorize]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditPostViewModel model,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
                var post = new Post
                {
                    Id = 0,
                    ImageUrl = model.ImageUrl,
                    Title = model.Title,
                    Body = model.Body,
                    Slug = Post.GetSlug(model.Title),
                    LastUpdateDate = DateTime.Now,
                    Category = model.Category,
                    Author = user
                };
                await context.Posts.AddAsync(post);
                await context.SaveChangesAsync();

                return Created($"v1/posts/{post.Id}", new ResultViewModel<Post>(post));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("02X05 - Não foi possível incluir a publicação"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("02X06 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/posts/{id:int}")]
        [Authorize]
        public async Task<IActionResult> PostAsync(
            [FromRoute] int id,
            [FromBody] EditPostViewModel model,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
                
                var post = await context
                .Posts
                .FirstOrDefaultAsync(x => x.Id == id);

                if (post == null)
                    return NotFound(new ResultViewModel<Post>("Conteúdo não encontrado"));

                if (post.Author != user)
                    return StatusCode(500, new ResultViewModel<Post>("02X09 - Não foi possível editar a publicação"));

                post.ImageUrl = model.ImageUrl;
                post.Title = model.Title;
                post.Body = model.Body;
                post.Slug = Post.GetSlug(model.Title);
                post.LastUpdateDate = DateTime.Now.ToUniversalTime();
                post.Category = model.Category;

                context.Posts.Update(post);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Post>(post));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("02X10 - Não foi possível alterar a publicação"));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("02X11 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/posts/{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == id);

                if (post == null)
                    return NotFound(new ResultViewModel<Post>("Conteúdo não encontrado"));

                if (post.Author != user)
                {
                    return StatusCode(500, new ResultViewModel<Post>("02X12 - Não foi possível excluir a publicação"));
                }
                context.Posts.Remove(post);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Post>(post));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("02X13 - Não foi possível excluir a categoria"));
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("02X14 - Falha interna no servidor"));
            }
        }
        
    }
}
