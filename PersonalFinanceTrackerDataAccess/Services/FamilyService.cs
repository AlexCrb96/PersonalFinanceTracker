using PersonalFinanceTrackerDataAccess.DataAccessContext;
using PersonalFinanceTrackerDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.Services
{
    public class FamilyService
    {
        private readonly FinanceDbContext _db;

        public FamilyService(FinanceDbContext context)
        {
            _db = context;
        }

        public async Task<int> CreateFamilyAsync(Family input)
        {
            _db.Families.Add(input);
            await _db.SaveChangesAsync();
            return input.Id;
        }
    }
}
