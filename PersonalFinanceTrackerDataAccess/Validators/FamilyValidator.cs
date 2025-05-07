using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Repositories;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerDataAccess.Validators
{
    public static class FamilyValidator
    {
        public static bool Exists(this Family family) => family != null;
        public static void ValidateHeadOfFamily(this User headOfFamily)
        {
            if (!headOfFamily.Exists())
            {
                throw new KeyNotFoundException("Head of family not found.");
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
        public static void ValidateCanInvite(this User sender)
        {
            if (!sender.Exists())
            {
                throw new KeyNotFoundException("Sender not found.");
            }

            if (!sender.HasRole(UserRole.HeadOfFamily))
            {
                throw new ValidationException("Sender does not have the right to send invitations.");
            }
        }

        public static void ExistsAndOwnedBy(this Family family, string userId)
        {
            if (!family.Exists())
            {
                throw new KeyNotFoundException("Family not found.");
            }

            if (family.HeadOfFamilyId != userId)
            {
                throw new ValidationException("You can only manage your own family.");
            }
        }

        private static async Task<bool> IsDuplicateInviteAsync(this IRepository<FamilyInvitation, int> familyInviteRepo, FamilyInvitation inputFamilyInvite)
        {
            return await familyInviteRepo
                .ExistsAsync(inv => inv.FamilyId == inputFamilyInvite.FamilyId
                                 && inv.RecipientEmail == inputFamilyInvite.RecipientEmail
                                 && inv.Status == FamilyInvitationStatus.Pending);
        }

        public static async Task EnsureNoDuplicateIniteAsync(this IRepository<FamilyInvitation, int> familyInviteRepo, FamilyInvitation inputFamilyInvite)
        {
            if (await IsDuplicateInviteAsync(familyInviteRepo, inputFamilyInvite))
            {
                throw new ValidationException("An invitation to this email already exists.");
            }
        }
    }
}
