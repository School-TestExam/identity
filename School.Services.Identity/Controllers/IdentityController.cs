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
        public async Task<ActionResult<GetResponse>> Create([FromQuery] CreateRequest request)
        {
            var id = await _service.Create(request);

            return CreatedAtAction(nameof(Get), id);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] Guid id)
        {
            await _service.Delete(id);

            return Ok();
        }

        [HttpGet("identities")]
        public async Task<ActionResult<GetResponse>> Get([FromBody] GetRequest request)
        {
            var response = await _service.Get(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<GetResponse>> Update([FromQuery] UpdateRequest request)
        {
            await _service.Update(request);

            return Ok();
        }
    }
}