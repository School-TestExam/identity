using Exam.Models.Identity.DTO;
using Exam.Models.Identity.Requests;
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

        [HttpPost]
        [ProducesResponseType(typeof(IdentityDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<IdentityDTO>> Create([FromBody] CreateRequest request)
        {
            var dto = await _service.Create(request);

            return CreatedAtAction(nameof(Get), new { dto.Id }, dto);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _service.Delete(id);

            return NoContent();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IdentityDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityDTO>> Get([FromRoute] Guid id)
        {
            var dto = await _service.Get(id);

            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(IdentityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IdentityDTO>> Update([FromRoute] Guid id, [FromBody] UpdateRequest request)
        {
            var dto = await _service.Update(id, request);
            return dto is null ? NotFound() : Ok(dto);
        }
    }
}