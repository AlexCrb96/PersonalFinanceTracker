using PersonalFinanceTrackerDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Validators
{
    public class BudgetValidator
    {
        public void ValidateIsPersonal (User user)
        {
            ValidateUserExists(user);

            if (user.FamilyId != null)
            {
                throw new ValidationException("User is part of a family. Use the family budget instead.");
            }
        }

        private void ValidateUserExists(User user)
        {
            if (user == null)
            {
                throw new ValidationException("User not found.");
            }
        }

    }
}
