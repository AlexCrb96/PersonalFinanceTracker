using PersonalFinanceTrackerAPI.DataTransferObjects.Requests;
using PersonalFinanceTrackerDataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceTrackerAPI.Mapping.Requests
{
    public static class BudgetMapper
    {
        public static Budget ToBudget(this CreatePersonalBudgetRequestDto dto, DateTime startDate, DateTime endDate)
        {
            string name = GetName(dto);
            return new Budget
            {
                UserId = dto.UserId,
                FamilyId = null, // Mark it as personal budget
                Name = name,
                Limit = dto.Limit,
                Period = dto.Period,
                StartDate = startDate,
                EndDate = endDate
            };
        }

        private static string GetName(CreatePersonalBudgetRequestDto dto)
        {
            if (dto.Name == null)
            {
                return "Personal Budget";
            }

            return dto.Name;
        }     
    }
}
