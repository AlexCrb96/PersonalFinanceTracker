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

        [Required(ErrorMessage = "Budget name is required.")]
        [StringLength(50, ErrorMessage = "Budget name cannot exceed 50 characters.")]
        public string Name { get; set; }

        public decimal Total { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalSpent { get; set; }

        public List<Transaction> Incomes { get; set; } = new List<Transaction>();
        public List<Transaction> Expenses { get; set; } = new List<Transaction>();

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2022", "12/31/9999", ErrorMessage = "Start Date must be a valid date.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2022", "12/31/9999", ErrorMessage = "End Date must be a valid date.")]
        public DateTime EndDate { get; set; }
    }
}
