using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTrackerWebAPI_Mk2.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    RecipientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RecipientName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.RecipientID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    BankName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankID);
                    table.ForeignKey(
                        name: "FK_Banks_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CreditCardID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    First4Digits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Second4Digits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Third4Digits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last4Digits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Network = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVC = table.Column<int>(type: "int", nullable: false),
                    CreditLimit = table.Column<double>(type: "float", nullable: false),
                    StatementGenDay = table.Column<int>(type: "int", nullable: false),
                    PaymentDueIn = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CreditCardID);
                    table.CheckConstraint("CHK_PaymentDueIn", "PaymentDueIn <= 31 AND PaymentDueIn >=1");
                    table.CheckConstraint("CHK_StatementGenDay", "StatementGenDay <= 31 AND StatementGenDay >=1");
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PaymentMode = table.Column<int>(type: "int", nullable: false),
                    TransactionMode = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    RewardPoints = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeneralComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreditCardID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "BankID");
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_CreditCards_CreditCardID",
                        column: x => x.CreditCardID,
                        principalTable: "CreditCards",
                        principalColumn: "CreditCardID");
                    table.ForeignKey(
                        name: "FK_Transactions_Recipients_RecipientID",
                        column: x => x.RecipientID,
                        principalTable: "Recipients",
                        principalColumn: "RecipientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), "Shopping" },
                    { new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), "Clothing" },
                    { new Guid("8815ac70-1c20-4167-a045-964d819e195f"), "Groceries" },
                    { new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), "Dining" },
                    { new Guid("a5c6d659-aaf2-495f-affc-015627059750"), "Electronics" },
                    { new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), "Travel" },
                    { new Guid("b72e8dc1-1bc0-4b1e-b086-e412ed4a9dc0"), "Healthcare" },
                    { new Guid("f5186c55-332a-4eae-b917-7f5fae0b2a7b"), "Entertainment" }
                });

            migrationBuilder.InsertData(
                table: "Recipients",
                columns: new[] { "RecipientID", "RecipientName" },
                values: new object[,]
                {
                    { new Guid("03a5b5de-e9ec-4e70-94f2-00da5d667787"), "United" },
                    { new Guid("04848095-f6d3-489e-bfd2-92b09e039019"), "Burger King" },
                    { new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), "Walmart" },
                    { new Guid("18884556-def7-499c-89df-09a8d95b62d0"), "Target" },
                    { new Guid("1a45beda-0d80-4ddc-9ba7-c12da07e7ab7"), "Insomnia Cookies" },
                    { new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), "BestBuy" },
                    { new Guid("2b32e737-6637-47b2-b150-ca3af794d620"), "Michael Kors" },
                    { new Guid("3cfff9f1-d8b3-4396-a44f-622899681719"), "Ralph Lauren" },
                    { new Guid("3ebaa280-3ec4-4b68-aa21-f9b2fe2cbd63"), "CVS" },
                    { new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), "Chick-fil-A" },
                    { new Guid("4575e719-3887-413b-9b30-326cf11d27f7"), "Southwest" },
                    { new Guid("5d47a329-4a2d-4676-b349-3d26958ff25d"), "McDonald's" },
                    { new Guid("6ecf6c47-660e-4871-a6fc-9c17167a3b5d"), "Panda Express" },
                    { new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), "American Airlines" },
                    { new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), "Delta" },
                    { new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), "Burlington" },
                    { new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), "Amazon" },
                    { new Guid("a53c4426-df1c-4325-8e52-4ea027a6e83e"), "Kroger" },
                    { new Guid("b7caea13-1d1a-4780-831a-ed973765c67a"), "Gap Inc" },
                    { new Guid("c7ee15e0-5480-4544-b2d6-094ba5134ac7"), "Costco" },
                    { new Guid("cd39920e-7b14-4043-be69-2ebab1a7cd3e"), "Dutch Bros. Coffee" },
                    { new Guid("dc769f30-ba3c-4df8-8e9e-44d0f905dea6"), "OLD NAVY" },
                    { new Guid("dea8e720-e79c-4fc3-81fd-201befa9f904"), "Dunkin' Donuts" },
                    { new Guid("df7269a9-6c65-434f-bcd5-d9853b0b75b3"), "Air France" },
                    { new Guid("dfb07eb1-81e9-4139-8419-ba545f0db07a"), "H-E-B" },
                    { new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), "H&M" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("8daa3821-3685-4299-a172-4bbf18929a73"), "Master", "User" },
                    { new Guid("bd500ce6-dee1-4445-a214-410829db561b"), "John", "Reese" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankID", "AccNumber", "Balance", "BankName", "UserID" },
                values: new object[,]
                {
                    { new Guid("4d46c466-805b-4b9d-9dc3-a636ac913ee3"), "117856353077", 95555.5, "Bank of America", new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("795fcc24-1975-454c-86ac-0cc0db1e78c6"), "122104569335", 22381.200000000001, "JP Morgan & Chase", new Guid("8daa3821-3685-4299-a172-4bbf18929a73") }
                });

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "CreditCardID", "BankName", "CVC", "CardHolderName", "CardName", "CreditLimit", "ExpDate", "First4Digits", "Last4Digits", "Network", "PaymentDueIn", "Second4Digits", "StatementGenDay", "Third4Digits", "UserID" },
                values: new object[,]
                {
                    { new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), "Bank of America", 435, "Admin Nim", "Card 1", 75000.0, new DateTime(2030, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "5 4 3 2", "6 6 4 2", 1, 20, "1 2 9 9", 21, "0 5 9 1", new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("75987053-7057-499b-bdf6-14ac41509853"), "CitiBank", 127, "Admin Nim", "CitiBank 1 7 8 0", 15000.0, new DateTime(2032, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 5 1 2", "1 7 8 0", 3, 20, "0 0 3 2", 16, "3 7 6 9", new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), "JP Morgan & Chase", 380, "Admin Nim", "Card 2", 15000.0, new DateTime(2028, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "9 8 7 6", "5 3 1 2", 0, 25, "4 3 2 7", 15, "9 7 5 2", new Guid("8daa3821-3685-4299-a172-4bbf18929a73") }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionID", "Amount", "BankID", "CategoryID", "CreditCardID", "Date", "GeneralComments", "PaymentMode", "RecipientID", "RewardPoints", "TransactionMode", "UserID" },
                values: new object[,]
                {
                    { new Guid("02f1bdff-80a5-41d1-be93-e14d97402742"), 700.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new digital camera for photography hobby", 1, new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("0321dcf8-9ddc-4b11-a880-9edbc71e977a"), 522.33000000000004, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a PS5", 1, new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("0ea1410b-e394-4f42-a622-ec8fe7b33c46"), 1200.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a powerful laptop for work and gaming", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("19e89b80-8561-49e0-b874-f49a305770da"), 1500.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a large smart TV for the living room", 1, new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1a0c6c5c-fcfd-499a-8346-c0b8cd1489c0"), 300.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new smartwatch for fitness tracking and notifications", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1d2bdc9d-f1cb-4e36-a285-1b32ed3ad6fd"), 80.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought new shoes for everyday wear", 1, new Guid("b7caea13-1d1a-4780-831a-ed973765c67a"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1db79274-33e6-4d7b-849c-44fcd745ebc7"), 30.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Had dinner at Panda Express.", 1, new Guid("6ecf6c47-660e-4871-a6fc-9c17167a3b5d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("2088043a-475b-4a61-ab6d-4e00d7e566b3"), 1200.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new laptop for work and entertainment", 1, new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("208da7f5-9bbf-4566-a3e2-253e6596cf63"), 2500.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked tickets to Tokyo, Japan for vacation", 1, new Guid("03a5b5de-e9ec-4e70-94f2-00da5d667787"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("285249e4-db59-4e36-bc15-e3496feed924"), 600.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked flights to Miami, USA for spring break", 1, new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("29f8a6d4-2ed1-4490-9e89-523bc3f67ec7"), 1199.5, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new laptop for work and entertainment", 1, new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("2f5dea5a-d1f8-4a05-8663-7c9376c0e216"), 19.75, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed lunch at Panda Express", 1, new Guid("6ecf6c47-660e-4871-a6fc-9c17167a3b5d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3cff4d4c-5d44-43b4-98c4-8f0a378b8d64"), 60.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought some spring clothes at H&M.", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3ffd8e08-d9a8-4676-abde-24639e728185"), 50.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new dress for a spring outing", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4254185d-498b-45f4-b26c-fcdc16e648f6"), 799.99000000000001, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new smartphone for upgraded features", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("44a1cebe-267f-4699-8c49-91bdc8b8ddf0"), 45.600000000000001, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dinner at Chick-fil-A", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("481f0eae-1fc0-432b-b52e-cc93d69a62a6"), 40.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new hoodie for chilly evenings", 1, new Guid("dc769f30-ba3c-4df8-8e9e-44d0f905dea6"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("49d48d35-39b6-409a-b2e1-2ae30c4db609"), 100.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new pair of sneakers for daily wear", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4f657cbb-04f2-4c6c-8288-e05c6bf637e2"), 95.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought household essentials", 1, new Guid("18884556-def7-499c-89df-09a8d95b62d0"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5552d052-f5f6-4544-b083-a2f71d594f46"), 35.5, null, new Guid("b72e8dc1-1bc0-4b1e-b086-e412ed4a9dc0"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prescription", 1, new Guid("3ebaa280-3ec4-4b68-aa21-f9b2fe2cbd63"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5abdc099-4bab-4351-9a0a-4448e3391339"), 40.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new swimsuit for the upcoming summer season", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5c75c12b-3b90-4222-887c-001b9ae75163"), 25.75, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dined at Chick-fil-A for lunch", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5f89f1e1-c265-42dc-b61b-f3947c179837"), 21.5, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dined at Chick-fil-A for dinner", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("60299a7b-25f1-4895-bbcf-0ebd697d0780"), 60.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new dress for the upcoming summer season", 1, new Guid("b7caea13-1d1a-4780-831a-ed973765c67a"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("630f921a-49ae-4e03-b5fa-211649b1fe84"), 350.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flight ticket", 1, new Guid("03a5b5de-e9ec-4e70-94f2-00da5d667787"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("666b5bea-cad5-4796-91ed-da3cba658148"), 1200.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked flight tickets to Paris, France for summer vacation", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("67b7ce82-5aea-493c-81e8-00a7f14f228b"), 350.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flight ticket - Refund", 1, new Guid("03a5b5de-e9ec-4e70-94f2-00da5d667787"), 0.0, 0, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6ca5710b-fce0-4958-93cd-63beb582576e"), 150.75, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bulk shopping for the month", 1, new Guid("c7ee15e0-5480-4544-b2d6-094ba5134ac7"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6e2f5b73-4d75-43a4-8a80-07c20544d154"), 33.460000000000001, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Returned the shoes", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), 0.0, 0, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6e86a8f7-87c5-4eb8-bc89-2acb5c03a03a"), 180.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new tablet at Walmart.", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("77c968f7-4077-4b39-b146-1819953df416"), 400.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked tickets to Los Angeles, USA for a weekend getaway", 1, new Guid("4575e719-3887-413b-9b30-326cf11d27f7"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("7f8d6c5f-c0f8-4e8b-ac8c-75a5b67b5745"), 250.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("75987053-7057-499b-bdf6-14ac41509853"), new DateTime(2024, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a flight ticket to New York with Delta.", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("866d56f8-f87a-4c5f-ad2a-7acb32fba09a"), 320.5, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought electronics from Amazon", 1, new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("892d507a-dc0c-4efa-9ae2-353244834fe8"), 65.25, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Weekly groceries", 1, new Guid("a53c4426-df1c-4325-8e52-4ea027a6e83e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("892df5a2-b46a-4a63-be9c-e3db033eb86f"), 9.9499999999999993, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought lunch at Burger King", 1, new Guid("04848095-f6d3-489e-bfd2-92b09e039019"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("8a564d1a-8a57-447c-a2ac-10f5316fdabf"), 1200.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flight tickets with Air France", 1, new Guid("df7269a9-6c65-434f-bcd5-d9853b0b75b3"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("8f2d62f3-4a2c-42c2-be4d-bced4d26ed96"), 70.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Picked up groceries at Kroger.", 1, new Guid("a53c4426-df1c-4325-8e52-4ea027a6e83e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("8f30a758-bb12-4e52-b73f-4f69c9b88c60"), 25.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed lunch at Burger King with friends.", 1, new Guid("04848095-f6d3-489e-bfd2-92b09e039019"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("944f7c7f-5706-4673-acba-598111f44441"), 120.5, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased groceries and household items", 1, new Guid("18884556-def7-499c-89df-09a8d95b62d0"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("9d9f14d1-250e-40ed-8d42-02bb85fa72f6"), 50.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Returned the new dress for a spring outing", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), 0.0, 0, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("9e74a91f-450e-4280-a876-86aaa1e3c7b1"), 75.200000000000003, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought new clothes", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("9fc18d82-1e91-44ed-b17d-5a90794b56d7"), 522.33000000000004, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a PS5", 1, new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("a2850307-d063-4227-b50f-b8996118ad21"), 70.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought new jeans for casual wear", 1, new Guid("b7caea13-1d1a-4780-831a-ed973765c67a"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("a7186dbd-fc28-44dc-91bf-dd30edb24535"), 85.75, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought household items", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("a821ce9f-e206-4c3e-be7e-5d502c777972"), 60.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new jacket for the upcoming spring season", 1, new Guid("dc769f30-ba3c-4df8-8e9e-44d0f905dea6"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("ac828fe5-632d-4d52-8917-c817338253e6"), 800.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upgraded to the latest smartphone model", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("adf83f65-1f1b-4915-b034-cca4ed5e0ef6"), 400.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new suit for upcoming business meetings", 1, new Guid("3cfff9f1-d8b3-4396-a44f-622899681719"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("b3c96fac-0a88-47fa-bebc-996a4cd3a39e"), 120.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new winter coat for the next winter season", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("b55146f1-c05b-4a13-9e6f-1eb5d79b30ea"), 8.4499999999999993, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought breakfast at Dunkin' Donuts", 1, new Guid("dea8e720-e79c-4fc3-81fd-201befa9f904"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("b7dcae9d-3f55-4ba9-ab33-855fcd44ad77"), 33.460000000000001, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a pair of shoes", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("bab80281-36d2-4b1a-844d-0b4f555cd0bd"), 15.17, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a Jacket", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("bb4b5ae6-0992-42cd-99ee-0e4b6d354bca"), 522.33000000000004, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new laptop", 1, new Guid("7ca7fcb7-5650-41ba-bcfa-41a1e428ed6e"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("c093e452-cdcc-4b7b-a498-1095b07df761"), 14.25, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grabbed lunch at McDonald's", 1, new Guid("5d47a329-4a2d-4676-b349-3d26958ff25d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("c8c934c9-1bf2-4285-bddc-6e8652dc8e72"), 500.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new gaming console for leisure time", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("cc218736-5271-43a3-9384-e21d3cd989d7"), 500.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought the latest gaming console with accessories", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), 24.449999999999999, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("cf6b8a50-cb07-4224-ab97-a81fc20a29ff"), 150.75, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Weekly grocery shopping at H-E-B", 1, new Guid("dfb07eb1-81e9-4139-8419-ba545f0db07a"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("d0c989e3-07e0-423b-a547-af6b82ffed0e"), 1200.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought new laptop", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("d41a1d2d-76e2-4577-aef0-a115183212a7"), 1200.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reserved flights to London, UK for a business conference", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("d483fb6b-134d-400c-b03e-1c5f304b0aa0"), 200.0, null, new Guid("b72e8dc1-1bc0-4b1e-b086-e412ed4a9dc0"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prescription refill at CVS", 1, new Guid("3ebaa280-3ec4-4b68-aa21-f9b2fe2cbd63"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("d75ff396-45e7-4462-b0f1-d076444aad47"), 103.83, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pair of jeans and a couple of t-shirts", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("dd04c84a-5807-43b2-88bb-17e19e96bfaf"), 153.83000000000001, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought winter wear - jackets, socks, shoes", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("df89eef9-4dc9-4e5b-a6ae-cb811a2d68fe"), 12.35, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grabbed lunch at Burger King", 1, new Guid("04848095-f6d3-489e-bfd2-92b09e039019"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("e4701e3d-ce36-4662-b196-b93d7de08973"), 80.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a portable speaker for outdoor gatherings", 1, new Guid("18884556-def7-499c-89df-09a8d95b62d0"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("e8ceef1e-6412-43a4-ba41-17f5c57ba8fb"), 30.0, null, new Guid("4d627443-1ae0-49e0-8bf9-b1ea4b8cd8e1"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought new shorts for the upcoming summer season", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("e900ca8a-b60b-4c2e-8570-3bb42c6116dc"), 17.5, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed a meal at Panda Express for dinner", 1, new Guid("6ecf6c47-660e-4871-a6fc-9c17167a3b5d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("f2da2972-b060-43c8-8039-6f36404ee06c"), 800.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new smartphone for upgraded features", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("f42dc138-78c5-487d-afee-90b6a0b22651"), 2500.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reserved flights to Tokyo, Japan for a cultural exploration trip", 1, new Guid("03a5b5de-e9ec-4e70-94f2-00da5d667787"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("f66a54e3-24d9-4842-9883-458afabc9f2a"), 19.75, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed lunch at Panda Express", 1, new Guid("6ecf6c47-660e-4871-a6fc-9c17167a3b5d"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("f8ca31ee-bea6-4f45-a0a8-4c1f80329336"), 22.5, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lunch with friends", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), 0.0, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_AccNumber",
                table: "Banks",
                column: "AccNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_BankName",
                table: "Banks",
                column: "BankName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UserID",
                table: "Banks",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UserID",
                table: "CreditCards",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_RecipientName",
                table: "Recipients",
                column: "RecipientName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankID",
                table: "Transactions",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryID",
                table: "Transactions",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreditCardID",
                table: "Transactions",
                column: "CreditCardID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecipientID",
                table: "Transactions",
                column: "RecipientID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
