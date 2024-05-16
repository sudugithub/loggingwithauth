using Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.BlogService;
using Service.Contract;
using SpanTechnologyTask.Utils;

namespace SpanTechnologyTask.Controllers
{
    [ApiController]
    [Authorize]
    public class BlogController(IBlogService blogService) : ControllerBase
    {
        private readonly IBlogService _blogService = blogService;

        [Route("/api/blog/create")]
        [HttpPost]
        public async Task<ActionResult<GenericResponse<long>>> Create([FromBody] BlogCreateContract contract)
        {
            var result = await _blogService.Create(contract);
            return Ok(new GenericResponse<long>(true, "Blog has been created", result));
        }

        [Route("/api/blog/delete")]
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<bool>>> Delete([FromQuery] long id)
        {
            var result = await _blogService.Delete(id);
            return Ok(new GenericResponse<bool>(true, "Blog has been deleted", result));
        }

        [Route("/api/blog/all")]
        [HttpGet]
        public async Task<ActionResult<GenericResponse<BlogPaginationResponse>>> GetAll([FromQuery] int pageSize, [FromQuery] int currentPage)
        {
            var result = await _blogService.GetAll(pageSize, currentPage);
            return Ok(new GenericResponse<BlogPaginationResponse>(true, "Blog has been deleted", result));
        }

        [Route("/api/blog/getById")]
        [HttpGet]
        public async Task<ActionResult<GenericResponse<Blog>>> GetById([FromQuery] long id)
        {
            var result = await _blogService.GetById(id);
            return Ok(new GenericResponse<Blog>(true, "Blog has been deleted", result));
        }

        [Route("/api/blog/update")]
        [HttpPut]
        public async Task<ActionResult<GenericResponse<bool>>> Update([FromQuery] long id, [FromBody] BlogCreateContract blog)
        {
            var result = await _blogService.Update(id, blog);
            return Ok(new GenericResponse<bool>(true, "Blog has been updated", result));
        }
    }
}
