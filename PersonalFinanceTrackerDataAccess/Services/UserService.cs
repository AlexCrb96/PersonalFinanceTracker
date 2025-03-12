using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalFinanceTrackerDataAccess.DataAccessContext;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Repositories;
using PersonalFinanceTrackerDataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
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

        public async Task<(bool Success, IEnumerable<string> Errors)> RegisterUserAsync(User input, string inputPassword)
        {
            var result = await _signInManager.UserManager.CreateAsync(input, inputPassword);
            if (result.Succeeded)
            {
                return (true, Array.Empty<string>());
            }
            else
            {
                return (false, result.Errors.Select(e => e.Description));
            }
        }

        public async Task<(bool Success, User? User, string? Error)> LoginUserAsync(string email, string password)
        {
            var userRepo = _unitOfWork.GetRepository<UserRepository, User, string>();

            var user = await userRepo.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, user, "Invalid e-mail.");
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!loginResult.Succeeded)
            {
                return (false, user, "Invalid password.");
            }

            return (true, user, null);
        }

    }
}
