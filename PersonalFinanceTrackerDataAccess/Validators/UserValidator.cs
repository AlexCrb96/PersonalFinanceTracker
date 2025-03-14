using Microsoft.AspNetCore.Identity;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Validators
{
    public static class UserValidator
    {
        public static bool Exists(this User user) => user != null;
        public static bool IsPartOfAFamily(this User user) => user.FamilyId == null;

        public static void ValidateRegisterResult(this IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new ValidationException(result.GetErrorMessage());
            }
        }

        public static void ValidateUserEmail(this User user)
        {
            if (!user.Exists())
            {
                throw new ValidationException("Invalid email.");
            }
        }

        public static void ValidateUserPassword(this SignInResult result)
        {
            if (!result.Succeeded)
            {
                throw new ValidationException("Invalid password.");
            }
        }
    }
}
