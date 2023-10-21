using Desk.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Desk.Controllers
{
    public class DeskController: ControllerBase
    {
        private readonly IDeskVisibilityTypeRepository _deskVisibilityTypeRepository;

        private readonly IDeskService _deskService;

        private readonly IUserRepository _userRepository;

        private readonly IWorkSpaceRepository _workSpaceRepository;
        public DeskController(IDeskVisibilityTypeRepository deskVisibilityTypeRepository, IDeskService deskService, IUserRepository userRepository, IWorkSpaceRepository workSpaceRepository) 
        {
            _deskVisibilityTypeRepository = deskVisibilityTypeRepository;
            _deskService = deskService;
            _userRepository = userRepository;
            _workSpaceRepository = workSpaceRepository;
        }

        [HttpGet("desk/visibility")]
        public async Task<IActionResult> GetVisibility() 
        {
            var visibilityTypes = await _deskVisibilityTypeRepository.ReadAllAsync();

            var result = visibilityTypes?.Select(vt => new DeskVisibilityTypeModel { Code = vt.Code });

            return Ok(result);
        }

        [Authorize]
        [HttpPost("desk")]
        public async Task<IActionResult> CreateDesk([FromBody]DeskCreateModel model) 
        {
            var userId = Guid.Parse(User.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sid).Value);
            var user = await _userRepository.ReadAsync(userId);

            var deskVisibilityType = await _deskVisibilityTypeRepository.ReadAsync(model.DeskVisibilityTypeCode);

            var workSpace = await _workSpaceRepository.ReadAsync(model.WorkSpaceId);

            var desk = await _deskService.CreateAsync(model.Name, deskVisibilityType, user, workSpace);

            if (desk is null)
                return BadRequest();

            return Ok(new DeskModel { Id = desk.Id });
        }
    }
}
