using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.DataTransferObjects.Requests
{
    public class InviteUserToFamilyRequestDto
    {
        [Required(ErrorMessage = "Family Id is required.")]
        public int FamilyId { get; set; }
        [Required(ErrorMessage = "Recipient email is required.")]
        [DataType(DataType.EmailAddress)]
        public string RecipientEmail { get; set; }
    }
}
