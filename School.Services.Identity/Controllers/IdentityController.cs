using Microsoft.AspNetCore.Mvc;
using Exam.Services.Identity.Services;
using IdentityModel = Exam.Models.Identity.Identity.Identity;

namespace Exam.Services.Identity.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService userService;
        public IdentityController(IdentityService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IdentityModel>> GetIdentity([FromBody] Guid Id)
        {
            var identity = await userService.GetIdentity(Id);

            return identity != null ? Ok(identity) : NotFound(identity);
        }

        [HttpDelete]
        public async Task<ActionResult<IdentityModel>> DeleteIdentity([FromBody] Guid Id)
        {
            var deletionSuccess = await userService.DeleteIdentity(Id);

            return deletionSuccess != false ? Ok(deletionSuccess) : NotFound(deletionSuccess);
        }

        [HttpPut]
        public async Task<ActionResult<IdentityModel>> PutIdentity([FromBody] Guid Id, [FromQuery] IdentityModel identity)
        {
            var updatedIdentity = await userService.PutIdentity(Id, identity);

            return updatedIdentity != null ? Ok(updatedIdentity) : NotFound(identity);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityModel>> CreateIdentity([FromQuery] IdentityModel identity)
        {
            var createdIdentity = await userService.CreateIdentity(identity);

            return createdIdentity != null ? Ok(createdIdentity) : BadRequest(identity);
        }
    }
}