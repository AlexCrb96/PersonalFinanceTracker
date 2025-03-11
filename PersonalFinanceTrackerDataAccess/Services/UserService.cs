using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalFinanceTrackerDataAccess.DataAccessContext;
using PersonalFinanceTrackerDataAccess.Entities;
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
        private readonly FinanceDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(FinanceDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<User>> GetUsersByIdsAsync(IEnumerable<string> ids)
        {
            return await _db.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
        }

        public async Task<(bool Success, IEnumerable<string> Errors)> RegisterUserAsync(User input, string inputPassword)
        {
            var result = await _userManager.CreateAsync(input, inputPassword);
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
            var user = await _userManager.FindByEmailAsync(email);
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

        public async Task AssignUserToFamilyAsync(User user, Family family, User.Role? familyRole = null)
        {
            user.FamilyId = family.Id;
            user.Family = family;
            user.FamilyRole = familyRole;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
