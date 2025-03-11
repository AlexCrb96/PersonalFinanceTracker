using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalFinanceTrackerDataAccess.DataAccessContext;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Repositories;
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
        private readonly UserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserRepository userRepository, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
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
            var user = await _userRepository.FindByEmailAsync(email);
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

        public async Task AssignFamilyToUserAsync(User user, Family family, User.Role? familyRole = null)
        {
            await _userRepository.AssignFamilyToUserAsync(user, family, familyRole);
        }
    }
}
