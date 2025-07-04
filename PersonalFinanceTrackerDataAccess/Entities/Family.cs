﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class Family
    {
        public int Id { get; set; }

        // Relationships
        [Required(ErrorMessage = "A Family should always have a leader.")]
        public string HeadOfFamilyId { get; set; } // Foreign key to family leader User
        public User HeadOfFamily { get; set; } // Each family can have one leader
        public ICollection<User> Members { get; set; } = new List<User>(); // Each family can have multiple members.
        public int? BudgetId { get; set; } // Foreign key to general budget
        public Budget? GeneralBudget { get; set; } // Each family has one general budget
        public ICollection<FamilyInvitation> Invitations { get; set; } = new List<FamilyInvitation>(); // Each family can have multiple invitations

        // Family details
        [Required(ErrorMessage = "Family name is required.")]
        [StringLength(50, ErrorMessage = "Family name must not exceed 50 characters.")]
        public string Name { get; set; }

    }
}
