using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.DataTransferObjects.Requests
{
    public class CreateCategoryBudgetRequestDto
    {
        [Required(ErrorMessage = "A category budget must be linked to a Category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "A category budget must be linked to a Budget.")]
        public int BudgetId { get; set; }

        [Required(ErrorMessage = "Category budget limit is required.")]
        [Precision(18, 2)]
        [Range(0.01, 10000000, ErrorMessage = "Budget limit must be greater than zero.")]
        public decimal Limit { get; set; }
    }
}
