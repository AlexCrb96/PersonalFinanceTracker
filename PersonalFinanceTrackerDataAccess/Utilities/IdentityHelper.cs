using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Utilities
{
    public static class IdentityHelper
    {
        public static string GetErrorMessage(this IdentityResult result)
        {
            return string.Join(", ", result.Errors.Select(e => e.Description));
        }
    }
}
