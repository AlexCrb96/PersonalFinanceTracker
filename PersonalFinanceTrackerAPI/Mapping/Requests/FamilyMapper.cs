using PersonalFinanceTrackerAPI.DataTransferObjects.Requests;
using PersonalFinanceTrackerDataAccess.Entities;

namespace PersonalFinanceTrackerAPI.Mapping.Requests
{
    public static class FamilyMapper
    {
        public static Family ToFamily(this CreateFamilyRequestDto input)
        {
            return new Family
            {
                Name = input.Name,
                HeadOfFamilyId = input.HeadOfFamilyId
            };
        }

        public static FamilyInvitation ToFamilyInvitation(this InviteUserToFamilyRequestDto input)
        {
            return new FamilyInvitation
            {
                Id = input.FamilyId,
                RecipientEmail = input.RecipientEmail,
                Status = FamilyInvitationStatus.Pending
            };
        }
    }
}
