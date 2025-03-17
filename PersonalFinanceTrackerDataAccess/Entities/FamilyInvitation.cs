using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class FamilyInvitation
    {
        public int Id { get; set; }

        // Relationships
        [Required (ErrorMessage = "Family ID is required in order to send an invitation.")]
        public int FamilyId { get; set; } // Foreign key to Family.
        public Family Family { get; set; } // Each invitation belongs to a family
        [Required(ErrorMessage = "Sender ID is required in order to send an invitation.")]
        public string SenderId { get; set; } // Foreign Key to User.
        public User Sender { get; set; } // Each invitation has a sender

        // Invitation details
        [Required(ErrorMessage = "Recipient email is required in order to send an invitation.")]
        public string RecipientEmail { get; set; }
        public User? Recipient { get; set; } // Recipient might not be a user yet. Can be null
        [Required(ErrorMessage = "Invitation token is required in order to send an invitation.")]
        public string Token { get; set; }
        public FamilyInvitationStatus Status { get; set; } = FamilyInvitationStatus.Pending;
        public DateTime ExpirationDate { get; set; }

    }
}
