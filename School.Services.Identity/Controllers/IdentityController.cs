using Exam.Models.Identity.Requests;
using Exam.Models.Identity.Responses;
using Exam.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Services.Identity.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _service;

        public IdentityController(IdentityService userService)
        {
            _service = userService;
        }

        [HttpPost]
        public async Task<ActionResult<GetResponse>> Create([FromBody] CreateRequest request)
        {
            var id = await _service.Create(request);

            return CreatedAtAction(nameof(Get), id);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete( Guid id)
        {
            await _service.Delete(id);

            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetResponse>> Get(GetRequest request)
        {
            var response = await _service.Get(request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GetResponse>> Update(Guid id, [FromBody] UpdateRequest request)
        {
            await _service.Update(id, request);

            return NoContent();
        }
    }
}