using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerAPI.DataTransferObjects.Requests;
using PersonalFinanceTrackerAPI.Mapping.Requests;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace PersonalFinanceTrackerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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
                Family inputFamily = input.ToFamily();
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
