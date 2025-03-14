using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerDataAccess.DataAccessContext;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Repositories;
using PersonalFinanceTrackerDataAccess.UnitOfWork;
using PersonalFinanceTrackerDataAccess.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Services
{
    public class FamilyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FamilyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateFamilyAsync(Family input)
        {
            var userRepo = _unitOfWork.GetRepository<UserRepository, User, string>();
            var familyRepo = _unitOfWork.GetRepository<Family, int>();

            User? headOfFamily = await userRepo.GetByIdAsync(input.HeadOfFamilyId);
            headOfFamily.ValidateHeadOfFamily();
            
            input.HeadOfFamily = headOfFamily;
            input.ValidateFamilyName();

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await familyRepo.AddAsync(input);
                await _unitOfWork.SaveAsync();

                if (input.Id == 0)
                {
                    throw new Exception("Failed to create family.");
                }

                userRepo.AssignFamilyToUserAsync(headOfFamily, input, UserRole.HeadOfFamily);
                
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();

                return input.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
