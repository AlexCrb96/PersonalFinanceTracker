using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class Budget
    {
        public int Id { get; set; }

        // Relationships
        public int? FamilyId { get; set; } // Foreing key to Family. If not null, belongs to a Family.
        public Family? Family { get; set; } // A budget can belong to one family
        public string? UserId { get; set; } // Foreign key to User. If not null, belongs to a User
        public User? User { get; set; } // A budget can belong to one user
        public ICollection<CategoryBudget> CategoryBudgets { get; set; } = new List<CategoryBudget>(); // Category specific budgets
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        // Budget details
        [Required(ErrorMessage = "Budget name is required.")]
        [StringLength(50, ErrorMessage = "Budget name cannot exceed 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Budget limit is required.")]
        [Precision(18, 2)]
        [Range(0.01, 10000000, ErrorMessage = "Budget limit must be greater than 0.")]
        public decimal Limit { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2022", "12/31/9999", ErrorMessage = "Start Date must be a valid date.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2022", "12/31/9999", ErrorMessage = "End Date must be a valid date.")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Recurring Period is required.")]
        public RecurringPeriod Period { get; set; }
        
    }
}
