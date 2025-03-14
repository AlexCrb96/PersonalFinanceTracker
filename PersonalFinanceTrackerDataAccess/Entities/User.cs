using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class User : IdentityUser
    {
        // Relationships
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // A user can have multiple transactions
        public int? FamilyId { get; set; } // Foreign Key to Family
        public Family? Family { get; set; } // A user can be part of one family
        public int? PersonalBudgetId { get; set; } // Foreign Key to personal budget
        public Budget? PersonalBudget { get; set; } // Only used when not part of a family

        // User details
        [Required]
        [StringLength(50, ErrorMessage = "First name must not exceed 50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name must not exceed 50 characters.")]
        public string LastName { get; set; }
        [Required]
        public DateTime DateJoined { get; private set; } = DateTime.UtcNow;
        public UserRole? FamilyRole { get; set; } // Role of the user in the family

    }
}
