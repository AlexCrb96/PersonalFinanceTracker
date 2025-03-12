using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Repositories;
using PersonalFinanceTrackerDataAccess.UnitOfWork;
using PersonalFinanceTrackerDataAccess.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Services
{
    public class BudgetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserRepository _userRepository;
        private readonly IRepository<Budget, int>? _budgetRepository;
        private readonly BudgetValidator _budgetValidator;
        public BudgetService(IUnitOfWork unitOfWork, BudgetValidator budgetValidator)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<UserRepository, User, string>();
            _budgetRepository = _unitOfWork.GetRepository<Budget, int>();
            _budgetValidator = budgetValidator;
        }

        public async Task<int> CreatePersonalBudgetAsync (Budget inputBudget)
        {
            var user = await _userRepository.GetByIdAsync(inputBudget.UserId);
            _budgetValidator.ValidateIsPersonal(user);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _budgetRepository.AddAsync(inputBudget);
                await _unitOfWork.SaveAsync();

                user.PersonalBudgetId = inputBudget.Id;
                _userRepository.Update(user);

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
    }
}
