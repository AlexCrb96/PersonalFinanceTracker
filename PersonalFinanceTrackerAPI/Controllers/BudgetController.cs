using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerAPI.DataTransferObjects.Requests;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Services;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetService _budgetService;

        public BudgetController(BudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpPost("create-personal")]
        public async Task<IActionResult> CreatePersonalBudget(CreatePersonalBudgetRequestDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Budget inputBudget = new Budget
                {
                    UserId = input.UserId,
                    FamilyId = null, // Mark it as personal budget
                    Name = input.Name,
                    Limit = input.Limit,
                    StartDate = DateTime.UtcNow,
                    EndDate = input.EndDate,
                };
                int budgetId = await _budgetService.CreatePersonalBudgetAsync(inputBudget);
                return Ok($"BudgetId = {budgetId}");
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
