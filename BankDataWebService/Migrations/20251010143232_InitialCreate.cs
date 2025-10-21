using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankDataWebService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNo = table.Column<int>(type: "INTEGER", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "BLOB", nullable: true),
                    StreetAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Suburb = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<int>(type: "INTEGER", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountNo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pin = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountType = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountNo);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsLegal = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccountNo = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetAccountNo = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountNo",
                        column: x => x.AccountNo,
                        principalTable: "Accounts",
                        principalColumn: "AccountNo",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_TargetAccountNo",
                        column: x => x.TargetAccountNo,
                        principalTable: "Accounts",
                        principalColumn: "AccountNo");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "Country", "Email", "FirstName", "LastName", "Password", "PhoneNo", "PostalCode", "ProfilePicture", "State", "StreetAddress", "Suburb" },
                values: new object[,]
                {
                    { "john4534", null, "john4534@gmail.com", "John", "Small", "342345435", 458392394, null, null, null, null, null },
                    { "sophia3423", null, "sophia3423@gmail.com", "Sophia", "Matassa", "9348230249", 423434332, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountNo", "AccountType", "Balance", "Pin", "UserName" },
                values: new object[,]
                {
                    { 1, "savings", 0m, 3423, "sophia3423" },
                    { 2, "cheque", 0m, 4353, "john4534" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "AccountNo", "Amount", "Description", "IsLegal", "TargetAccountNo", "TimeStamp" },
                values: new object[,]
                {
                    { 1, 1, 134m, null, true, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 293m, null, true, null, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserName",
                table: "Accounts",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountNo",
                table: "Transactions",
                column: "AccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TargetAccountNo",
                table: "Transactions",
                column: "TargetAccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Password",
                table: "Users",
                column: "Password",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
