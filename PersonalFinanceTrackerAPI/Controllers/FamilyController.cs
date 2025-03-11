using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerAPI.DataTransferObjects.Requests;
using PersonalFinanceTrackerDataAccess.Validators;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Services;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.Controllers
{
    [Route("api/family")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly FamilyService _familyService;
        private readonly FamilyValidator _familyValidator;

        public FamilyController(UserService userService, FamilyService familyService, FamilyValidator familyValidator)
        {
            _userService = userService;
            _familyService = familyService;
            _familyValidator = familyValidator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFamily(CreateFamilyRequestDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User? headOfFamily = await _userService.GetUserByIdAsync(input.HeadOfFamilyId);
            try
            {
                _familyValidator.ValidateHeadOfFamily(headOfFamily);
            }
            catch(ValidationException e)
            {
                return BadRequest(e.Message);
            }

            Family inputFamily = new Family
            {
                Name = input.Name,
                HeadOfFamilyId = headOfFamily.Id,
                HeadOfFamily = headOfFamily
            };

            inputFamily.Id = await _familyService.CreateFamilyAsync(inputFamily);
            await _userService.AssignFamilyToUserAsync(headOfFamily, inputFamily, PersonalFinanceTrackerDataAccess.Entities.User.Role.HeadOfFamily);

            return Ok($"FamilyId = {inputFamily.Id}");
        }
    }
}
