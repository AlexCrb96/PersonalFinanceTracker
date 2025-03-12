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
        private readonly FamilyService _familyService;

        public FamilyController(FamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFamily(CreateFamilyRequestDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Family inputFamily = new Family
                {
                    Name = input.Name,
                    HeadOfFamilyId = input.HeadOfFamilyId
                };
                int familyId = await _familyService.CreateFamilyAsync(inputFamily);
                return Ok($"FamilyId = {familyId}");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Unexpected Server Error.");
            }
        }
    }
}
