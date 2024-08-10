using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTrackerWebAPI_Mk2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
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
                    RewardPoints = table.Column<int>(type: "int", nullable: true),
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
                columns: new[] { "UserID", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("8daa3821-3685-4299-a172-4bbf18929a73"), "admin@admin.com", "Admin", "Nimda", "admin", "admin" },
                    { new Guid("bd500ce6-dee1-4445-a214-410829db561b"), "john.reese@machine.com", "John", "Reese", "john", "John" }
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
                    { new Guid("0d5c78c6-f5e7-494f-83fb-6da06d1a50d2"), 150.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought clothes at Burlington.", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1a9b89a2-da14-4eca-bf84-8bfa6dad823a"), 100.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased groceries for the week at Costco.", 1, new Guid("c7ee15e0-5480-4544-b2d6-094ba5134ac7"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1c02bc7e-f204-4e8f-bae2-f8d24bc8d401"), 500.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a flight ticket with American Airlines.", 1, new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1cbef37d-1b75-4bfd-97a9-9c8e468f0c78"), 80.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased souvenirs at Gap before the trip.", 1, new Guid("b7caea13-1d1a-4780-831a-ed973765c67a"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1d07b74a-732e-4721-adce-f4c7c4a5a125"), 350.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked a round-trip flight with United Airlines.", 1, new Guid("03a5b5de-e9ec-4e70-94f2-00da5d667787"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1db79274-33e6-4d7b-849c-44fcd745ebc7"), 30.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Had dinner at Panda Express.", 1, new Guid("6ecf6c47-660e-4871-a6fc-9c17167a3b5d"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1e7dba55-d7a2-485e-b87b-64b98c7fa2c5"), 200.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a tablet as a holiday gift.", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("1f12b98e-9c29-4c41-8586-ad340a3565f9"), 70.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased some new jeans at Gap.", 1, new Guid("b7caea13-1d1a-4780-831a-ed973765c67a"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("29d6b144-c31c-4b0d-82db-ba042a60b8d0"), 300.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new TV at BestBuy.", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("2a15a4ed-134a-4268-9133-b28ff8c46a04"), 75.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought holiday gifts at Target.", 1, new Guid("18884556-def7-499c-89df-09a8d95b62d0"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("2b4c0db2-156b-4d56-85bc-2344b481d263"), 75.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stocked up on essentials from Kroger.", 1, new Guid("a53c4426-df1c-4325-8e52-4ea027a6e83e"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("2c7d40db-86a5-4698-8cc3-ffc79a64e572"), 400.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a gaming console from BestBuy.", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("2d62b49a-92a3-43bc-9628-ebf167d31c8f"), 55.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new sweater from Gap.", 1, new Guid("b7caea13-1d1a-4780-831a-ed973765c67a"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("2e8f06a1-6a58-429e-9b75-0f0191a57d91"), 600.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked a round-trip flight for a business trip.", 1, new Guid("03a5b5de-e9ec-4e70-94f2-00da5d667787"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("2eed2e7e-8858-491f-aff4-67c1a3130d02"), 80.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased kitchen appliances from Walmart.", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("38e4e39a-4b82-44ed-b90b-563f5c2e6f45"), 700.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a last-minute flight ticket for travel.", 1, new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3a4b28d4-2b71-42b9-a89e-13da1d10d9d6"), 25.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Had a late-night snack at Burger King.", 1, new Guid("04848095-f6d3-489e-bfd2-92b09e039019"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3cff4d4c-5d44-43b4-98c4-8f0a378b8d64"), 60.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought some spring clothes at H&M.", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3d56bf6a-7d6f-4eae-8b92-788ad83eca3e"), 25.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed a quick lunch at Burger King.", 1, new Guid("04848095-f6d3-489e-bfd2-92b09e039019"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3d5c2a86-348a-43b4-bf3d-f1e5b4b2a75f"), 150.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stocked up on groceries at Costco.", 1, new Guid("c7ee15e0-5480-4544-b2d6-094ba5134ac7"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3d6784c2-612a-4420-8d38-354f1d4b199c"), 25.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed a coffee and pastry at Dunkin' Donuts.", 1, new Guid("dea8e720-e79c-4fc3-81fd-201befa9f904"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3d7bae39-2344-4a77-bd3f-c99c320d9e3c"), 700.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a flight ticket for a vacation.", 1, new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3f35a59a-f20d-465c-8f6f-32bb0fffb56d"), 350.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked a flight with American Airlines for vacation.", 1, new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("3f3d54d9-2a94-48d4-a97c-fa8c31c57f02"), 100.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased winter accessories from Burlington.", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4a0e6c4d-6edb-43a6-877c-b9c96d1cda92"), 300.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new laptop from BestBuy for work.", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4a8c0043-0fae-42ac-b4a3-fa6b3c5d60e5"), 110.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stocked up on groceries at Costco.", 1, new Guid("c7ee15e0-5480-4544-b2d6-094ba5134ac7"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4c3c5d6c-2736-44d4-9d77-180a7bb7e10c"), 28.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Had a meal at Panda Express after shopping.", 1, new Guid("6ecf6c47-660e-4871-a6fc-9c17167a3b5d"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4d7e41f9-505c-431e-af61-32c84e07df6b"), 150.0, null, new Guid("f5186c55-332a-4eae-b917-7f5fae0b2a7b"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased tickets for a trip with Delta.", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4eaa2b7c-4b58-41c3-9e4b-3a9dbe73f19a"), 45.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new jacket from H&M.", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4f3a197b-bf8b-4745-94cc-6c1b0d1c8baf"), 50.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed snacks during a Delta flight.", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4f493f8c-5a80-493a-8f24-ec1d0897c3a3"), 70.0, null, new Guid("f5186c55-332a-4eae-b917-7f5fae0b2a7b"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed in-flight entertainment on American Airlines.", 1, new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4f8a19f6-b4d1-4a0f-bda3-8f073b6538bc"), 700.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked a flight to Barcelona with Delta Airlines.", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("4f97f8c3-f33e-4d35-912d-ca4c73f39f56"), 12.0, null, new Guid("f5186c55-332a-4eae-b917-7f5fae0b2a7b"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Watched a movie on the flight with American Airlines.", 1, new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5a1d5b77-eb9a-447d-a2b9-4f7d4e82f063"), 150.0, null, new Guid("f5186c55-332a-4eae-b917-7f5fae0b2a7b"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Attended a fashion event featuring Ralph Lauren.", 1, new Guid("3cfff9f1-d8b3-4396-a44f-622899681719"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5a8b6b9b-d8e0-49c1-ac58-bde963635df4"), 110.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Did monthly shopping at Costco.", 1, new Guid("c7ee15e0-5480-4544-b2d6-094ba5134ac7"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5a9e1d88-ff26-4f55-a4e3-5c86f5291b79"), 35.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed a family meal at Chick-fil-A.", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5c8c8ff9-d91c-4d99-a126-36a098bec1b4"), 30.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Had dinner at Chick-fil-A with family.", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5e1a58c8-40bc-47e6-baa4-3b5b6a20509e"), 300.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new smartphone from BestBuy.", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5e8e09a6-f6d0-44a3-9621-8f59f4c30534"), 35.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed a family dinner at Chick-fil-A.", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("5f6182da-b9a5-4181-bf83-0dce3c009d9a"), 150.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stocked up on groceries at Costco for the trip.", 1, new Guid("c7ee15e0-5480-4544-b2d6-094ba5134ac7"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("62b2c92b-9c07-4878-bb03-8e6010d653e8"), 120.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought bulk groceries from Costco.", 1, new Guid("c7ee15e0-5480-4544-b2d6-094ba5134ac7"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("63ba5b48-fb89-4dbd-b40d-b55b14eb5dd7"), 499.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new smartphone at BestBuy.", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6b4d03f4-3e5a-4324-8f9e-1a5b92980f53"), 900.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked a flight to Paris with Air France.", 1, new Guid("df7269a9-6c65-434f-bcd5-d9853b0b75b3"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6d23e27c-477d-4c77-b69f-8d8ed593d5c9"), 250.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a smart home device from BestBuy.", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6e2f5b73-4d75-43a4-8a80-07c20544d154"), 250.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked a flight with Delta Airlines for vacation.", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6e7a7335-7e63-4e06-bc4b-36f8586bb4c4"), 500.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a last-minute ticket for Delta Airlines.", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6e86a8f7-87c5-4eb8-bc89-2acb5c03a03a"), 180.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new tablet at Walmart.", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6e9b83e6-3fb4-494c-bf61-b08d80f6db93"), 12.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grabbed coffee and donuts from Dunkin'.", 1, new Guid("dea8e720-e79c-4fc3-81fd-201befa9f904"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6f100d66-fc30-4fa2-be71-cb62f16120b4"), 150.0, null, new Guid("f5186c55-332a-4eae-b917-7f5fae0b2a7b"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Attended a fashion show featuring Ralph Lauren.", 1, new Guid("3cfff9f1-d8b3-4396-a44f-622899681719"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("6f4f7b9c-5d96-466d-83a5-b9d77b35c4e1"), 50.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stocked up on snacks and drinks at Kroger.", 1, new Guid("a53c4426-df1c-4325-8e52-4ea027a6e83e"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("7a67d568-c099-4e61-b067-8d16c891fa68"), 65.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased winter clothing from Burlington.", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("7b7f48f6-9a35-411c-b3eb-ee79d96c4f66"), 20.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grabbed breakfast from Dunkin' Donuts.", 1, new Guid("dea8e720-e79c-4fc3-81fd-201befa9f904"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("7d8b8c6a-3f40-4908-b25b-db4a97e8f17e"), 950.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked a last-minute flight to Tokyo.", 1, new Guid("73f04239-ca28-47e1-9243-a8c888c06f4c"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("7e1d5e54-fb92-4b3f-a356-1ebfe67fc1e7"), 30.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2023, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed a quick meal at Panda Express.", 1, new Guid("6ecf6c47-660e-4871-a6fc-9c17167a3b5d"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("7f8d6c5f-c0f8-4e8b-ac8c-75a5b67b5745"), 250.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("75987053-7057-499b-bdf6-14ac41509853"), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a flight ticket to New York with Delta.", 1, new Guid("7431dc59-2e5a-4863-a58b-05e17566e310"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("81a012a5-f90a-4c3b-88da-1b743a59cc4e"), 40.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Had a family lunch at Chick-fil-A.", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("84b7d10a-1f4d-4a19-afe9-f56b64f58d6e"), 90.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased travel essentials at Burlington.", 1, new Guid("7bb90da3-fee4-4a5b-8679-5d820e90ac3d"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("8a49e1ed-5243-4b8b-bf3b-ca622953c63d"), 200.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a new tablet for reading and entertainment.", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("8afa5455-fdf1-4e93-8e77-be663b175a6e"), 80.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased winter clothing at H&M.", 1, new Guid("fec14558-524a-46a1-ab0e-f8acfaf9ab49"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("8f2d62f3-4a2c-42c2-be4d-bced4d26ed96"), 70.0, null, new Guid("8815ac70-1c20-4167-a045-964d819e195f"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Picked up groceries at Kroger.", 1, new Guid("a53c4426-df1c-4325-8e52-4ea027a6e83e"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("8f30a758-bb12-4e52-b73f-4f69c9b88c60"), 25.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed lunch at Burger King with friends.", 1, new Guid("04848095-f6d3-489e-bfd2-92b09e039019"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("8fc6d79e-c2a6-4f56-a05d-0fca4b0bfbc5"), 300.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought a travel adapter at BestBuy.", 1, new Guid("1b5a7d64-2e25-48cd-8cd6-9587f6078ca5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("9a1e55bb-080d-414d-bd88-f70c8c87fa6b"), 220.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2023, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Booked a round-trip ticket with Southwest.", 1, new Guid("4575e719-3887-413b-9b30-326cf11d27f7"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("9d05b8e3-5b65-4703-85f1-bd9c32c60091"), 850.0, null, new Guid("a6d314d6-ab99-47f9-9b17-2c94f9f03086"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Round-trip flight to London on United Airlines.", 1, new Guid("03a5b5de-e9ec-4e70-94f2-00da5d667787"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("9d9f14d1-250e-40ed-8d42-02bb85fa72f6"), 35.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("566863e0-cac1-427b-9f22-74519d3970d9"), new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoyed a family dinner at Chick-fil-A.", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("c73c7f0e-cbf3-4c82-bdb9-8e3a8f9dffce"), 200.0, null, new Guid("43a9d80e-581d-4639-9bcd-e062e7ac7176"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bought household items at Target.", 1, new Guid("18884556-def7-499c-89df-09a8d95b62d0"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("c92a1d1d-22fc-408c-bda6-98bc62330e08"), 250.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a new TV from Walmart.", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("d4c4089c-bf54-4825-96a5-7b444b849f2d"), 200.0, null, new Guid("a5c6d659-aaf2-495f-affc-015627059750"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Purchased a camera at Walmart for travel photos.", 1, new Guid("1059d5f9-e4aa-4c06-8f35-e664b6215378"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") },
                    { new Guid("e43b3f93-1b7a-48a9-829e-be9a7b99b75d"), 30.0, null, new Guid("9ad4ccf1-f72b-4fe2-9c76-e91cd01445d8"), new Guid("cd4e4392-2fe6-4000-8f7b-d4c3885a0f7d"), new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Had a quick meal at Chick-fil-A before the flight.", 1, new Guid("41f99a2a-3fed-4bec-bee2-465f46f933a5"), null, 1, new Guid("8daa3821-3685-4299-a172-4bbf18929a73") }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

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
