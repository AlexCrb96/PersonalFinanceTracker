using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerAPI.DataTransferObjects.Requests;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Services;

namespace PersonalFinanceTrackerAPI.Controllers
{
    [Route("api/family")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly FamilyService _familyService;

        public FamilyController(UserService userService, FamilyService familyService)
        {
            _userService = userService;
            _familyService = familyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFamily(CreateFamilyRequestDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User? headOfFamily = await _userService.GetUserByIdAsync(input.HeadOfFamilyId);
            if (headOfFamily == null)
            {
                return BadRequest("Head of family User not found.");
            }

            if (headOfFamily.FamilyId != null)
            {
                return BadRequest("User is already a member of a family.");
            }

            Family inputFamily = new Family
            {
                Name = input.Name,
                HeadOfFamilyId = headOfFamily.Id,
                HeadOfFamily = headOfFamily
            };

            inputFamily.Id = await _familyService.CreateFamilyAsync(inputFamily);
            await _userService.AssignUserToFamilyAsync(headOfFamily, inputFamily, PersonalFinanceTrackerDataAccess.Entities.User.Role.HeadOfFamily);

            return Ok();
        }
    }
}
