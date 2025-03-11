using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerDataAccess.DataAccessContext;
using PersonalFinanceTrackerDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Repositories
{
    public class UserRepository : Repository<User, string>
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(FinanceDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public override async Task<List<User>> GetAllAsync() => await _userManager.Users.ToListAsync();

        public override async Task<User?> GetByIdAsync(string id) => await _userManager.FindByIdAsync(id);

        public async Task<User?> FindByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);

        public async Task AssignFamilyToUserAsync(User user, Family family, User.Role? familyRole = null)
        {
            user.FamilyId = family.Id;
            user.FamilyRole = familyRole;

            Update(user);
            await SaveAsync();
        }

    }
}
