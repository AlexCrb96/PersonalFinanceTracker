using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Repositories;
using PersonalFinanceTrackerDataAccess.UnitOfWork;
using PersonalFinanceTrackerDataAccess.Utilities;
using PersonalFinanceTrackerDataAccess.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Services
{
    public class BudgetService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BudgetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreatePersonalBudgetAsync (Budget inputBudget)
        {
            var userRepository = _unitOfWork.GetRepository<UserRepository>();
            var budgetRepository = _unitOfWork.GetRepository<Budget, int>();

            var user = await userRepository.GetByIdAsync(inputBudget.UserId);
            user.ValidatePersonalBudget();

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await budgetRepository.AddAsync(inputBudget);
                await _unitOfWork.SaveAsync();

                userRepository.AssignBudgetToUser(user, inputBudget);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();

                return inputBudget.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public (DateTime startDate, DateTime endDate) GetDates(RecurringPeriod period)
        {
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate;

            switch (period)
            {
                case RecurringPeriod.Daily:
                    endDate = startDate.GetEndOfDay();
                    break;
                case RecurringPeriod.Weekly:
                    endDate = startDate.GetEndOfWeek();
                    break;
                case RecurringPeriod.Monthly:
                    endDate = startDate.GetEndOfMonth();
                    break;
                case RecurringPeriod.Quarterly:
                    endDate = startDate.GetEndOfQuarter();
                    break;
                case RecurringPeriod.Yearly:
                    endDate = startDate.GetEndOfYear();
                    break;
                default:
                    throw new ValidationException("Invalid period. Valid values are: Daily, Weekly, Monthly, Quarterly, Yearly.");
            }

            return (startDate, endDate);
        }

    }
}
