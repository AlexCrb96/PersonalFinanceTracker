using PersonalFinanceTrackerDataAccess.Entities;
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
        public static bool Exists(this User user)
        {
            if (user == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsPartOfAFamily(this User user)
        {
            if (user.FamilyId == null)
            {
                return false;
            }

            return true;
        }
    }
}
