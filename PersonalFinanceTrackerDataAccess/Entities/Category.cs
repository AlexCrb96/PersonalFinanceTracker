using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class Category
    {
        public int Id { get; set; }

        // Relationships
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // A category can have multiple transactions
        public ICollection<CategoryBudget> CategoryBudgets { get; set; } = new List<CategoryBudget>(); // A category can have multiple budgets

        // Category details
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string Name { get; set; }


    }
}
