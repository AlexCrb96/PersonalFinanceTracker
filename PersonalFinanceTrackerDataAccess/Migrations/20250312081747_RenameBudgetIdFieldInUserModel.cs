using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceTrackerDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameBudgetIdFieldInUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BudgetId",
                table: "AspNetUsers",
                newName: "PersonalBudgetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalBudgetId",
                table: "AspNetUsers",
                newName: "BudgetId");
        }
    }
}
