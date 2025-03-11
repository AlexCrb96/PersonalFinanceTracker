using PersonalFinanceTrackerDataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.DataTransferObjects.Requests
{
    public class CreateFamilyRequestDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Family name must not exceed 50 characters.")]
        public string Name { get; set; }

        [Required]
        public string HeadOfFamilyId { get; set; }
    }
}
