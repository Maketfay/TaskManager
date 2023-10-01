using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using WorkSpace.Models;

namespace WorkSpace.Controllers
{
    [ApiController]
    public class WorkSpaceController: ControllerBase
    {
        private readonly IWorkSpaceService _workSpaceService;

        public WorkSpaceController(IWorkSpaceService workSpaceService) 
        {
            _workSpaceService = workSpaceService;
        }

        [Authorize]
        [HttpPost("/workSpace")]
        public async Task<IActionResult> CreateWorkSpace(WorkSpaceCreateModel model) 
        {
            var userId = Guid.Parse(User.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sid).Value);

            var workSpace = await _workSpaceService.CreateAsync(userId, model.WorkSpaceName);
            if (workSpace is null)
                return BadRequest();

            return Ok(new WorkSpaceResponseModel { WorkSpaceId = workSpace.Id });
        }
    }
}
    