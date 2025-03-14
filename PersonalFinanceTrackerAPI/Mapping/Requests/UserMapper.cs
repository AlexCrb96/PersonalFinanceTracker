using PersonalFinanceTrackerAPI.DataTransferObjects.Requests;
using PersonalFinanceTrackerDataAccess.Entities;

namespace PersonalFinanceTrackerAPI.Mapping.Requests
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserRequestDto registerRequest)
        {
            return new User
            {
                Email = registerRequest.Email,
                UserName = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName
            };
        }

        public static User ToUser(this LoginUserRequestDto loginRequest)
        {
            return new User
            {
                Email = loginRequest.Email,
                UserName = loginRequest.Email
            };
        }
    }
}
