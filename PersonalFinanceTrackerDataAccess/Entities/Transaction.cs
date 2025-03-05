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

        [Required(ErrorMessage = "Transaction amount is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Transaction amount must be grater than 0.")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Transaction description is required.")]
        [StringLength(50, ErrorMessage = "Description must not exceed 50 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Transaction type is required.")]
        public TransactionType Type { get; set; }

        // TODO: make sure foreign key is properly configured
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }

        // TODO: make sure foreign key is properly configured
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // TODO: make sure to convert enum from int to string when creating the table
        public enum TransactionType
        {
            Income,
            Expense
        }
    }
}
