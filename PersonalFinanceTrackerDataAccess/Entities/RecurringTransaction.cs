using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Entities
{
    public class RecurringTransaction : Transaction
    {
        // RecurringTransaction details
        [Required(ErrorMessage = "Frequency is required.")]
        public RecurringFrequency Frequency { get; set; }
        [Required(ErrorMessage = "Next Occurrence Date is required.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2022", "12/31/9999", ErrorMessage = "Next Occurrence Date must be a valid date.")]
        public DateTime NextOccurrenceDate { get; set; }
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/2022", "12/31/9999", ErrorMessage = "End Date must be a valid date.")]
        public DateTime? EndDate { get; set; }

        public enum RecurringFrequency
        {
            Daily,
            Weekly,
            Monthly,
            Yearly
        }
    }
}
