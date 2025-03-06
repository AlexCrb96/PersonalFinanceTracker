using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class CategoryBudget
    {
        public int Id { get; set; }

        // TODO: make sure foreign keys are properly configured
        // Relationships
        public int CategoryId { get; set; } // Foreign key to Category
        public Category Category { get; set; } // A budget can belong to a category
        public int BudgetId { get; set; } // Foreign key to Budget
        public Budget Budget { get; set; } // Each category budget belongs to a family’s general budget

        // Category budget details
        [Required(ErrorMessage = "Category budget limit is required.")]
        [Range(0.01, 10000000, ErrorMessage = "Budget limit must be greater than zero.")]
        public decimal Limit { get; set; }
    }
}
