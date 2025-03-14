using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerDataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.DataTransferObjects.Requests
{
    public class CreatePersonalBudgetRequestDto
    {
        [Required(ErrorMessage = "A personal Budget must be assigned to a User.")]
        public string UserId { get; set; }

        public string? Name { get; set; }

        [Required(ErrorMessage = "Budget limit is required.")]
        [Precision(18, 2)]
        [Range(0.01, 10000000, ErrorMessage = "Budget limit must be greater than 0.")]
        public decimal Limit { get; set; }

        [Required(ErrorMessage = "Budget period is required.")]
        [StringLength(20, ErrorMessage = "Budget period cannot exceed 20 characters.")]
        public RecurringPeriod Period { get; set; }
    }
}
