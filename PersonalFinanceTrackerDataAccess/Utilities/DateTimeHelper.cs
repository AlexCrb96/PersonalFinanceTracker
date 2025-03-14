using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Utilities
{
    public static class DateTimeHelper
    {
        public static DateTime GetEndOfDay(this DateTime date)
        {
            // End of day: one tick before the next day starts
            return date.Date.AddDays(1).AddTicks(-1);
        }

        // Assuming the week ends on Sunday.
        public static DateTime GetEndOfWeek(this DateTime date, DayOfWeek weekEnd = DayOfWeek.Sunday)
        {
            int daysUntilWeekEnd = ((int)weekEnd - (int)date.DayOfWeek + 7) % 7;
            // If today is Sunday, consider the next Sunday as end date.
            if (daysUntilWeekEnd == 0)
                daysUntilWeekEnd = 7;
            // End of week: end of Sunday
            return date.Date.AddDays(daysUntilWeekEnd).AddTicks(-1);
        }

        public static DateTime GetEndOfMonth(this DateTime date)
        {
            // Get the last day of the current month
            int lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            // End of month: last moment of the month
            return new DateTime(date.Year, date.Month, lastDay).AddDays(1).AddTicks(-1);
        }

        public static DateTime GetEndOfQuarter(this DateTime date)
        {
            // Determine the current quarter (1-4)
            int currentQuarter = (int)Math.Ceiling(date.Month / 3.0);
            // The quarter ends at the end of the third month of the quarter
            int lastMonthOfQuarter = currentQuarter * 3;
            int lastDayOfQuarter = DateTime.DaysInMonth(date.Year, lastMonthOfQuarter);
            return new DateTime(date.Year, lastMonthOfQuarter, lastDayOfQuarter)
                .AddDays(1).AddTicks(-1);
        }

        public static DateTime GetEndOfYear(this DateTime date)
        {
            int lastDay = DateTime.DaysInMonth(date.Year, 12);
            return new DateTime(date.Year, 12, lastDay).AddDays(1).AddTicks(-1);
        }
    }
}
