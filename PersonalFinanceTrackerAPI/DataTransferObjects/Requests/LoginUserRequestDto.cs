using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.DataTransferObjects.Requests
{
    public class LoginUserRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
