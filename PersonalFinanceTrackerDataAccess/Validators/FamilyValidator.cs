using PersonalFinanceTrackerDataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerDataAccess.Validators
{
    public static class FamilyValidator
    {
        public static void ValidateHeadOfFamily(this User headOfFamily)
        {
            if (!headOfFamily.Exists())
            {
                throw new ValidationException("Head of family not found.");
            }

            if (!headOfFamily.IsPartOfAFamily())
            {
                throw new ValidationException("Head of family is already part of a family.");
            }
        }
        public static void ValidateFamilyName(this Family family)
        {
            if (string.IsNullOrWhiteSpace(family.Name))
            {
                family.Name = $"{family.HeadOfFamily.LastName}'s Family";
            }
        }
    }
}
