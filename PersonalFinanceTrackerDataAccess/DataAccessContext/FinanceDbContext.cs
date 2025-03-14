using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTrackerDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTrackerDataAccess.DataAccessContext
{
    public class FinanceDbContext : IdentityDbContext<User>
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<CategoryBudget> CategoryBudgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure base Identity model configuration is applied
            base.OnModelCreating(modelBuilder);

            // Define Enum conversions to store their string values in the database instead of integer values
            modelBuilder.Entity<Transaction>().Property(t => t.Type).HasConversion<string>().HasMaxLength(20);
            modelBuilder.Entity<RecurringTransaction>().Property(rt => rt.Frequency).HasConversion<string>().HasMaxLength(20);
            modelBuilder.Entity<User>().Property(u => u.FamilyRole).HasConversion<string>().HasMaxLength(20);
            modelBuilder.Entity<Budget>().Property(b => b.Period).HasConversion<string>().HasMaxLength(20);

            // Define One-to-One relationship between Family and HeadOfFamily (User)
            modelBuilder.Entity<Family>()
                .HasOne(f => f.HeadOfFamily) // Family has one HeadOfFamily (User)
                .WithOne() // No inverse navigation needed
                .HasForeignKey<Family>(f => f.HeadOfFamilyId) // Foreign key in Family table
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of HeadOfFamily if Family exists

            // Define One-to-Many relationship between Family and Members (Users)
            modelBuilder.Entity<Family>()
                .HasMany(f => f.Members) // Family has many Members (Users)
                .WithOne(u => u.Family) // User belongs to one Family
                .HasForeignKey(u => u.FamilyId) // Foreign key in User table
                .OnDelete(DeleteBehavior.SetNull); // Set FamilyId to NULL if the family is deleted

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

            // Define One-to-Many relationship between User and Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User) // Transaction has one User
                .WithMany(u => u.Transactions) // User has many Transactions
                .HasForeignKey(t => t.UserId) // Foreign key in Transaction table
                .OnDelete(DeleteBehavior.Cascade); // If a User is deleted, its transactions will also be deleted (Cascade)

        }
    }
}
