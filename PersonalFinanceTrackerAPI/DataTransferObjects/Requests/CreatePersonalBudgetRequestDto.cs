using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerDataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.DataTransferObjects.Requests
{
    public class CreatePersonalBudgetRequestDto
    {
        [Required(ErrorMessage = "A personal Budget must be assigned to a User.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Budget name is required.")]
        [StringLength(50, ErrorMessage = "Budget name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Budget limit is required.")]
        [Precision(18, 2)]
        [Range(0.01, 10000000, ErrorMessage = "Budget limit must be greater than 0.")]
        public decimal Limit { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2022", "12/31/9999", ErrorMessage = "End Date must be a valid date.")]
        public DateTime EndDate { get; set; }
    }
}
