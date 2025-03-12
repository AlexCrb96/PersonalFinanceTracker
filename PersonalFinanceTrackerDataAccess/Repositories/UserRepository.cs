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
        public UserRepository(FinanceDbContext context) : base(context) { }

        public override async Task<User?> GetByIdAsync(string id) => 
            await _dbSet
            .Include(u => u.Family)
            .Include(u => u.PersonalBudget)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> FindByEmailAsync(string email) => await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

        public void AssignFamilyToUserAsync(User user, Family family, User.Role? familyRole = null)
        {
            user.FamilyId = family.Id;
            user.FamilyRole = familyRole;

            Update(user);
        }

    }
}
