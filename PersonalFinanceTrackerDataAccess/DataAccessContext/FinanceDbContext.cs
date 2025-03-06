using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.DataAccessContext
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<CategoryBudget> CategoryBudgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Enum conversions to store their string values in the database instead of integer values
            modelBuilder.Entity<Transaction>().Property(t => t.Type).HasConversion<string>().HasMaxLength(20);
            modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>().HasMaxLength(20);
            modelBuilder.Entity<RecurringTransaction>().Property(rt => rt.Frequency).HasConversion<string>().HasMaxLength(20);

            // Define One-to-One relationship between Family and FamilyLeader (User)
            modelBuilder.Entity<Family>()
                .HasOne(f => f.FamilyLeader) // Family has one FamilyLeader (User)
                .WithOne() // No inverse navigation needed
                .HasForeignKey<Family>(f => f.FamilyLeaderId) // Foreign key in Family table
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of FamilyLeader if Family exists

            // Define One-to-One relationship between Family and Budget
            modelBuilder.Entity<Family>()
                .HasOne(f => f.GeneralBudget)  // Family has one GeneralBudget
                .WithOne(b => b.Family)        // Budget belongs to a Family
                .HasForeignKey<Budget>(b => b.FamilyId) // Foreign key in Budget table
                .OnDelete(DeleteBehavior.Cascade); // If a Family is deleted, the corresponding Budget is also deleted (Cascade)

            // Define Ont-to-One relationship between User and Budget
            modelBuilder.Entity<User>()
                .HasOne(u => u.PersonalBudget) // User has one PersonalBudget
                .WithOne(b => b.User)          // Budget belongs to a User
                .HasForeignKey<Budget>(b => b.UserId) // Foreign key in Budget table
                .OnDelete(DeleteBehavior.Cascade); // If a User is deleted, the corresponding Budget is also deleted (Cascade)

            // Define max length for password hash in User table
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(128);
        }
    }
}
