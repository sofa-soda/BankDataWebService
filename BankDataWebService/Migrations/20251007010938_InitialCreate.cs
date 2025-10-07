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
                    UserId = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNo = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "BLOB", nullable: true),
                    StreetAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Suburb = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<int>(type: "INTEGER", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountNo = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    Pin = table.Column<uint>(type: "INTEGER", nullable: false),
                    AccountType = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountNo);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    SendingAccountNo = table.Column<uint>(type: "INTEGER", nullable: true),
                    ReceivingAccountNo = table.Column<uint>(type: "INTEGER", nullable: true),
                    TimeStamp = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_ReceivingAccountNo",
                        column: x => x.ReceivingAccountNo,
                        principalTable: "Accounts",
                        principalColumn: "AccountNo");
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_SendingAccountNo",
                        column: x => x.SendingAccountNo,
                        principalTable: "Accounts",
                        principalColumn: "AccountNo");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Country", "Email", "FirstName", "LastName", "Password", "PhoneNo", "PostalCode", "ProfilePicture", "State", "StreetAddress", "Suburb" },
                values: new object[,]
                {
                    { 1u, null, "sophia3423@gmail.com", "Sophia", "Matassa", "9348230249", 423434332, null, null, null, null, null },
                    { 2u, null, "john4534@gmail.com", "John", "Small", "342345435", 458392394, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountNo", "AccountType", "Balance", "Pin", "UserId" },
                values: new object[,]
                {
                    { 1u, "savings", 0m, 3423u, 1u },
                    { 2u, "cheque", 0m, 4353u, 2u }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "Amount", "Description", "ReceivingAccountNo", "SendingAccountNo", "TimeStamp" },
                values: new object[,]
                {
                    { 1u, 134m, null, null, 1u, "2024-12-02 16:43:03" },
                    { 2u, 293m, null, null, 2u, "2025-01-03 18:52:17" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ReceivingAccountNo",
                table: "Transactions",
                column: "ReceivingAccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SendingAccountNo",
                table: "Transactions",
                column: "SendingAccountNo");

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
