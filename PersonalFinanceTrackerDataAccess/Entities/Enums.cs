using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public enum RecurringPeriod
    {
        Daily,
        Weekly,
        Monthly,
        Quarterly,
        Yearly
    }

    public enum UserRole
    {
        Child, // Read-Only User: Can only view what's going on.
        Adult, // Standard User: Can manage personal transactions and make requests to alter budgets.
        HeadOfFamily // Family User: Can manage budgets and invite others.
    }

    public enum TransactionType
    {
        Income,
        Expense
    }

    public enum FamilyInvitationStatus
    {
        Pending,
        Accepted,
        Declined,
        Expired
    }
}
