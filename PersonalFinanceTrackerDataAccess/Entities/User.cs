using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }

        // TODO: make sure foreign keys are properly configured
        // Relationships
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // A user can have multiple transactions
        public int? FamilyId { get; set; } // Foreign Key to Family
        public Family? Family { get; set; } // A user can be part of one family
        public int? BudgetId { get; set; } // Foreign Key to personal budget
        public Budget? PersonalBudget { get; set; } // Only used when not part of a family

        // User details
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role must not exceed 50 characters.")]
        public UserRole Role { get; set; }
        public readonly DateTime DateJoined = DateTime.UtcNow;


        // TODO: make sure to convert enum from int to string when creating the table
        public enum UserRole
        {
            Child, // Read only user
            Adult, // Standard user - Can manage personal transactions and make requests to alter budgets.
            FamilyLeader // Full user - Can manage budgets, invite others and set roles.
        }
    }
}
