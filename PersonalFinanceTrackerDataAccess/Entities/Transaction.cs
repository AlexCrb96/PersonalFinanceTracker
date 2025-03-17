using Microsoft.EntityFrameworkCore;
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

        // Relationships
        [Required (ErrorMessage = "A transaction should always belong to a user.")]
        public string UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Each transaction belongs to one user
        [Required(ErrorMessage = "A transaction should always belong to a category.")]
        public int CategoryId { get; set; } // Foreign key to Category
        public Category Category { get; set; } // Each transaction belongs to one category

        // Transaction details
        [Required(ErrorMessage = "Transaction amount is required.")]
        [Precision(18, 2)]
        [Range(0.01, 1000000, ErrorMessage = "Transaction amount must be grater than 0.")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [StringLength(50, ErrorMessage = "Vendor must not exceed 50 characters.")]
        public string? Vendor { get; set; }
        [Required(ErrorMessage = "Transaction type is required.")]
        public TransactionType Type { get; set; }

    }
}
