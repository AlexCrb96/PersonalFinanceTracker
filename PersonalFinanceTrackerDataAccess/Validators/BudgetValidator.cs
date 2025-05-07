using PersonalFinanceTrackerDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Validators
{
    public static class BudgetValidator
    {
        public static void ValidatePersonalBudget (this User budgetUser)
        {
            if (!budgetUser.Exists())
            {
                throw new KeyNotFoundException("User does not exist.");
            }

            if (budgetUser.IsPartOfAFamily())
            {
                throw new ValidationException("User is part of a family. Use the family budget instead.");
            }
        }
    }
}
