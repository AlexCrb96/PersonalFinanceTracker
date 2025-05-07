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
            var userRepo = _unitOfWork.GetRepository<UserRepository>();
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

                userRepo.AssignFamilyToUser(headOfFamily, input, UserRole.HeadOfFamily);

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

        public async Task<int> InviteUserToFamilyAsync(FamilyInvitation inputFamilyInvite, string token)
        {
            var userRepo = _unitOfWork.GetRepository<UserRepository>();
            var familyRepo = _unitOfWork.GetRepository<Family, int>();
            var familyInviteRepo = _unitOfWork.GetRepository<FamilyInvitation, int>();

            User? sender = await userRepo.GetByIdAsync(inputFamilyInvite.SenderId);
            sender.ValidateCanInvite();

            Family? family = await familyRepo.GetByIdAsync(inputFamilyInvite.FamilyId);
            family.ExistsAndOwnedBy(sender.Id);

            await familyInviteRepo.EnsureNoDuplicateIniteAsync(inputFamilyInvite);

            inputFamilyInvite.Token = token;
            inputFamilyInvite.Status = FamilyInvitationStatus.Pending;
            inputFamilyInvite.ExpirationDate = DateTime.UtcNow.AddDays(7);

            // Start transaction
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await familyInviteRepo.AddAsync(inputFamilyInvite);
                await _unitOfWork.SaveAsync();

                if (inputFamilyInvite.Id == 0)
                {
                    throw new Exception("Failed to create invitation.");
                }

                // Send email

                await _unitOfWork.CommitAsync();
                return inputFamilyInvite.Id;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }
    }
}
