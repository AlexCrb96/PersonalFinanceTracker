using PersonalFinanceTrackerDataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerDataAccess.Validators
{
    public class FamilyValidator
    {
        public void ValidateHeadOfFamily(User headOfFamily)
        {
            if (headOfFamily == null)
            {
                throw new ValidationException("Head of family User not found.");
            }

            if (headOfFamily.FamilyId != null)
            {
                throw new ValidationException("User is already a member of a family.");
            }
        }
    }
}
