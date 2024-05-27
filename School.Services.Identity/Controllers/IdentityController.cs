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
        private readonly IIdentityService _service;

        public IdentityController(IIdentityService userService)
        {
            _service = userService;
        }

        [ProducesResponseType(typeof(GetResponse), StatusCodes.Status201Created)]
        [HttpPost("identities")]
        public async Task<ActionResult<GetResponse>> Create([FromBody] CreateRequest request)
        {
            var id = await _service.Create(request);

            return CreatedAtAction(nameof(Get), id);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("identities/{id}")]
        public async Task<ActionResult> Delete([FromBody] Guid id)
        {
            await _service.Delete(id);

            return NoContent();
        }

        [ProducesResponseType(typeof(GetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("identities/id")]
        public async Task<ActionResult<GetResponse>> Get(GetRequest request)
        {
            var response = await _service.Get(request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [ProducesResponseType(typeof(GetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("identities/id")]
        public async Task<ActionResult<GetResponse>> Update([FromBody] UpdateRequest request)
        {
            var response = await _service.Update(request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}