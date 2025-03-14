using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalFinanceTrackerDataAccess.DataAccessContext;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Repositories;
using PersonalFinanceTrackerDataAccess.UnitOfWork;
using PersonalFinanceTrackerDataAccess.Utilities;
using PersonalFinanceTrackerDataAccess.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;

        public UserService(IUnitOfWork unitOfWork, SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        public async Task<string> RegisterUserAsync(User input, string inputPassword)
        {
            var result = await _signInManager.UserManager.CreateAsync(input, inputPassword);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.GetErrorMessage());
            }

            return input.Id;
        }

        public async Task<User> LoginUserAsync(User input, string inputPassword)
        {
            var userRepo = _unitOfWork.GetRepository<UserRepository, User, string>();

            User user = await userRepo.FindByEmailAsync(input.Email);

            if (!user.Exists())
            {
                throw new ValidationException("Invalid email.");
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, inputPassword, false);
            if (!loginResult.Succeeded)
            {
                throw new ValidationException("Invalid password.");
            }

            return user;
        }
    }
}
