using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceTrackerDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameFamilyLeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Families_AspNetUsers_FamilyLeaderId",
                table: "Families");

            migrationBuilder.RenameColumn(
                name: "FamilyLeaderId",
                table: "Families",
                newName: "HeadOfFamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_Families_FamilyLeaderId",
                table: "Families",
                newName: "IX_Families_HeadOfFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_AspNetUsers_HeadOfFamilyId",
                table: "Families",
                column: "HeadOfFamilyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Families_AspNetUsers_HeadOfFamilyId",
                table: "Families");

            migrationBuilder.RenameColumn(
                name: "HeadOfFamilyId",
                table: "Families",
                newName: "FamilyLeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Families_HeadOfFamilyId",
                table: "Families",
                newName: "IX_Families_FamilyLeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_AspNetUsers_FamilyLeaderId",
                table: "Families",
                column: "FamilyLeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
