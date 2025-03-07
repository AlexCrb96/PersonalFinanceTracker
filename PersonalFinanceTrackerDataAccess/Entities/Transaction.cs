using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        // TODO: make sure foreign keys are properly configured
        // Relationships
        public required string UserId { get; set; } // Foreign key to User
        public required User User { get; set; } // Each transaction belongs to one user
        public required int CategoryId { get; set; } // Foreign key to Category
        public required Category Category { get; set; } // Each transaction belongs to one category

        // Transaction details
        [Required(ErrorMessage = "Transaction amount is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Transaction amount must be grater than 0.")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [StringLength(50, ErrorMessage = "Vendor must not exceed 50 characters.")]
        public string? Vendor { get; set; }
        [Required(ErrorMessage = "Transaction type is required.")]
        public TransactionType Type { get; set; }


        public enum TransactionType
        {
            Income,
            Expense
        }
    }
}
