using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletGraphs.Migrations
{
    /// <inheritdoc />
    public partial class RecieverAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reciever",
                table: "Expenditures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reciever",
                table: "Expenditures");
        }
    }
}
