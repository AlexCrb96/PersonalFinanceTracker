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
            result.ValidateRegisterResult();

            return input.Id;
        }

        public async Task<User> LoginUserAsync(User input, string inputPassword)
        {
            var userRepo = _unitOfWork.GetRepository<UserRepository>();

            User user = await userRepo.FindByEmailAsync(input.Email);
            user.ValidateUserEmail();

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, inputPassword, false);
            loginResult.ValidateUserPassword();

            return user;
        }
    }
}
