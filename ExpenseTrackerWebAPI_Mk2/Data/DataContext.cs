using ExpenseTrackerWebAPI_Mk2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerWebAPI_Mk2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Bank Table

            modelBuilder.Entity<Bank>()
                .HasIndex(b => b.BankName)
                .IsUnique();

            modelBuilder.Entity<Bank>()
                .HasIndex(b => b.AccNumber)
                .IsUnique();

            modelBuilder.Entity<Bank>()
                .Property(b => b.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<Bank>()
                .Property(b => b.BankID)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Bank>()
                .HasOne(b => b.User)
                .WithMany(u => u.Banks)
                .HasForeignKey(b => b.UserID);

            #endregion

            #region Category Table

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .Property(c => c.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryID)
                .HasDefaultValueSql("NEWID()");
            #endregion

            #region Credit Card Table

            modelBuilder.Entity<CreditCard>()
                .ToTable(cc => cc.HasCheckConstraint("CHK_StatementGenDay", "StatementGenDay <= 31 AND StatementGenDay >=1"))
                .ToTable(cc => cc.HasCheckConstraint("CHK_PaymentDueIn", "PaymentDueIn <= 31 AND PaymentDueIn >=1"));

            //.ToTable(cc => cc.HasCheckConstraint("CHK_NetworkName", "NetworkName='VISA' OR NetworkName='MasterCard' OR NetworkName='American Express' OR NetworkName='Discover'"))

            modelBuilder.Entity<CreditCard>()
                .Property(cc => cc.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<CreditCard>()
                .Property(cc => cc.CreditCardID)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<CreditCard>()
                .HasOne(cc => cc.User)
                .WithMany(u => u.CreditCards)
                .HasForeignKey(cc => cc.UserID);

            #endregion

            #region Recipient Table

            modelBuilder.Entity<Recipient>()
                .HasIndex(r => r.RecipientName)
                .IsUnique();

            modelBuilder.Entity<Recipient>()
                .Property(r => r.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<Recipient>()
                .Property(r => r.RecipientID)
                .HasDefaultValueSql("NEWID()");

            #endregion

            #region Transaction Table

            //modelBuilder.Entity<Transaction>()
            //    .ToTable(t => t.HasCheckConstraint("CHK_PaymentMethod", "PaymentMethod='Bank' OR PaymentMethod='Credit Card'"))
            //    .ToTable(t => t.HasCheckConstraint("CHK_TransactionType", "TransactionType='Credit' OR TransactionType='Debit'"));

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionID)
                .HasDefaultValueSql("NEWID()");

            #region Foreign Keys

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Recipient)
                .WithMany(r => r.Transactions)
                .HasForeignKey(t => t.RecipientID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Bank)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BankID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.CreditCard)
                .WithMany(cc => cc.Transactions)
                .HasForeignKey(t => t.CreditCardID);

            #endregion

            #endregion

            #region User Table

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(u => u.UserID)
                .HasDefaultValueSql("NEWID()");

            #endregion


            #region Default Data for DB

            #region Bank Table

            modelBuilder.Entity<Bank>()
                .HasData(
                    new Bank { BankID = new Guid("795FCC24-1975-454C-86AC-0CC0DB1E78C6"), BankName = "JP Morgan & Chase", AccNumber = "122104569335", Balance = 22381.20, UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73") },
                    new Bank { BankID = new Guid("4D46C466-805B-4B9D-9DC3-A636AC913EE3"), BankName = "Bank of America", AccNumber = "117856353077", Balance = 95555.50, UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73") }
                );
            #endregion

            #region Credit Card Table

            modelBuilder.Entity<CreditCard>()
                .HasData(
                    new CreditCard { CreditCardID = new Guid("75987053-7057-499B-BDF6-14AC41509853"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), First4Digits = "3 5 1 2", Second4Digits = "0 0 3 2", Third4Digits = "3 7 6 9", Last4Digits = "1 7 8 0", CardHolderName = "Admin Nim", Network = NetworkEnum.Discover, BankName = "CitiBank", ExpDate = DateTime.Parse("2032-04-27"), CVC = 127, CreditLimit = 15000.00, StatementGenDay = 16, PaymentDueIn = 20, CardName = "CitiBank 1 7 8 0" },
                    new CreditCard { CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), First4Digits = "5 4 3 2", Second4Digits = "1 2 9 9", Third4Digits = "0 5 9 1", Last4Digits = "6 6 4 2", CardHolderName = "Admin Nim", Network = NetworkEnum.MasterCard, BankName = "Bank of America", ExpDate = DateTime.Parse("2030-05-31"), CVC = 435, CreditLimit = 75000.00, StatementGenDay = 21, PaymentDueIn = 20, CardName = "Card 1" },
                    new CreditCard { CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), First4Digits = "9 8 7 6", Second4Digits = "4 3 2 7", Third4Digits = "9 7 5 2", Last4Digits = "5 3 1 2", CardHolderName = "Admin Nim", Network = NetworkEnum.VISA, BankName = "JP Morgan & Chase", ExpDate = DateTime.Parse("2028-03-31"), CVC = 380, CreditLimit = 15000.00, StatementGenDay = 15, PaymentDueIn = 25, CardName = "Card 2" }
                );

            #endregion

            #region Category Table

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { CategoryID = new Guid("F5186C55-332A-4EAE-B917-7F5FAE0B2A7B"), CategoryName = "Entertainment" },
                    new Category { CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), CategoryName = "Travel" },
                    new Category { CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), CategoryName = "Clothing" },
                    new Category { CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), CategoryName = "Dining" },
                    new Category { CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), CategoryName = "Groceries" },
                    new Category { CategoryID = new Guid("B72E8DC1-1BC0-4B1E-B086-E412ED4A9DC0"), CategoryName = "Healthcare" },
                    new Category { CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), CategoryName = "Electronics" },
                    new Category { CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), CategoryName = "Shopping" }
                );

            #endregion

            #region Recipient Table

            modelBuilder.Entity<Recipient>()
                .HasData(
                    new Recipient { RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), RecipientName = "H&M" },
                    new Recipient { RecipientID = new Guid("3CFFF9F1-D8B3-4396-A44F-622899681719"), RecipientName = "Ralph Lauren" },
                    new Recipient { RecipientID = new Guid("2B32E737-6637-47B2-B150-CA3AF794D620"), RecipientName = "Michael Kors" },
                    new Recipient { RecipientID = new Guid("DC769F30-BA3C-4DF8-8E9E-44D0F905DEA6"), RecipientName = "OLD NAVY" },
                    new Recipient { RecipientID = new Guid("B7CAEA13-1D1A-4780-831A-ED973765C67A"), RecipientName = "Gap Inc" },

                    new Recipient { RecipientID = new Guid("04848095-F6D3-489E-BFD2-92B09E039019"), RecipientName = "Burger King" },
                    new Recipient { RecipientID = new Guid("5D47A329-4A2D-4676-B349-3D26958FF25D"), RecipientName = "McDonald's" },
                    new Recipient { RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), RecipientName = "Chick-fil-A" },
                    new Recipient { RecipientID = new Guid("6ECF6C47-660E-4871-A6FC-9C17167A3B5D"), RecipientName = "Panda Express" },
                    new Recipient { RecipientID = new Guid("1A45BEDA-0D80-4DDC-9BA7-C12DA07E7AB7"), RecipientName = "Insomnia Cookies" },
                    new Recipient { RecipientID = new Guid("DEA8E720-E79C-4FC3-81FD-201BEFA9F904"), RecipientName = "Dunkin' Donuts" },
                    new Recipient { RecipientID = new Guid("CD39920E-7B14-4043-BE69-2EBAB1A7CD3E"), RecipientName = "Dutch Bros. Coffee" },

                    new Recipient { RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), RecipientName = "Burlington" },
                    new Recipient { RecipientID = new Guid("C7EE15E0-5480-4544-B2D6-094BA5134AC7"), RecipientName = "Costco" },
                    new Recipient { RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), RecipientName = "Walmart" },
                    new Recipient { RecipientID = new Guid("18884556-DEF7-499C-89DF-09A8D95B62D0"), RecipientName = "Target" },
                    new Recipient { RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), RecipientName = "BestBuy" },
                    new Recipient { RecipientID = new Guid("A53C4426-DF1C-4325-8E52-4EA027A6E83E"), RecipientName = "Kroger" },
                    new Recipient { RecipientID = new Guid("DFB07EB1-81E9-4139-8419-BA545F0DB07A"), RecipientName = "H-E-B" },

                    new Recipient { RecipientID = new Guid("3EBAA280-3EC4-4B68-AA21-F9B2FE2CBD63"), RecipientName = "CVS" },

                    new Recipient { RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), RecipientName = "American Airlines" },
                    new Recipient { RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), RecipientName = "Delta" },
                    new Recipient { RecipientID = new Guid("03A5B5DE-E9EC-4E70-94F2-00DA5D667787"), RecipientName = "United" },
                    new Recipient { RecipientID = new Guid("DF7269A9-6C65-434F-BCD5-D9853B0B75B3"), RecipientName = "Air France" },
                    new Recipient { RecipientID = new Guid("4575E719-3887-413B-9B30-326CF11D27F7"), RecipientName = "Southwest" }
                );

            #endregion

            #region User Table

            modelBuilder.Entity<User>()
                .HasData(
                    new User { UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), UserName = "admin", Password = "admin", FirstName = "Admin", LastName = "Nimda", Email = "admin@admin.com" },
                    new User { UserID = new Guid("BD500CE6-DEE1-4445-A214-410829DB561B"), UserName = "John", Password = "john", FirstName = "John", LastName = "Reese", Email = "john.reese@machine.com" }
                );

            #endregion

            modelBuilder.Entity<Transaction>()
                .HasData(
                    new Transaction { TransactionID = new Guid("1A9B89A2-DA14-4ECA-BF84-8BFA6DAD823A"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("C7EE15E0-5480-4544-B2D6-094BA5134AC7"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 100, Date = DateTime.Parse("2023-12-01"), GeneralComments = "Purchased groceries for the week at Costco." },
                    new Transaction { TransactionID = new Guid("3F3D54D9-2A94-48D4-A97C-FA8C31C57F02"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 100, Date = DateTime.Parse("2023-12-01"), GeneralComments = "Purchased winter accessories from Burlington." },
                    new Transaction { TransactionID = new Guid("9A1E55BB-080D-414D-BD88-F70C8C87FA6B"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("4575E719-3887-413B-9B30-326CF11D27F7"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 220, Date = DateTime.Parse("2023-12-02"), GeneralComments = "Booked a round-trip ticket with Southwest." },
                    new Transaction { TransactionID = new Guid("2B4C0DB2-156B-4D56-85BC-2344B481D263"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("A53C4426-DF1C-4325-8E52-4EA027A6E83E"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 75, Date = DateTime.Parse("2023-12-03"), GeneralComments = "Stocked up on essentials from Kroger." },
                    new Transaction { TransactionID = new Guid("3D56BF6A-7D6F-4EAE-8B92-788AD83ECA3E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("04848095-F6D3-489E-BFD2-92B09E039019"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 25, Date = DateTime.Parse("2023-12-04"), GeneralComments = "Enjoyed a quick lunch at Burger King." },
                    new Transaction { TransactionID = new Guid("8A49E1ED-5243-4B8B-BF3B-CA622953C63D"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 200, Date = DateTime.Parse("2023-12-04"), GeneralComments = "Bought a new tablet for reading and entertainment." },
                    new Transaction { TransactionID = new Guid("5E8E09A6-F6D0-44A3-9621-8F59F4C30534"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 35, Date = DateTime.Parse("2023-12-05"), GeneralComments = "Enjoyed a family dinner at Chick-fil-A." },
                    new Transaction { TransactionID = new Guid("7E1D5E54-FB92-4B3F-A356-1EBFE67FC1E7"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("6ECF6C47-660E-4871-A6FC-9C17167A3B5D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 30, Date = DateTime.Parse("2023-12-07"), GeneralComments = "Enjoyed a quick meal at Panda Express." },
                    new Transaction { TransactionID = new Guid("8AFA5455-FDF1-4E93-8E77-BE663B175A6E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 80, Date = DateTime.Parse("2023-12-09"), GeneralComments = "Purchased winter clothing at H&M." },
                    new Transaction { TransactionID = new Guid("5E1A58C8-40BC-47E6-BAA4-3B5B6A20509E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 300, Date = DateTime.Parse("2023-12-10"), GeneralComments = "Purchased a new smartphone from BestBuy." },
                    new Transaction { TransactionID = new Guid("1D07B74A-732E-4721-ADCE-F4C7C4A5A125"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("03A5B5DE-E9EC-4E70-94F2-00DA5D667787"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 350, Date = DateTime.Parse("2023-12-10"), GeneralComments = "Booked a round-trip flight with United Airlines." },
                    new Transaction { TransactionID = new Guid("2C7D40DB-86A5-4698-8CC3-FFC79A64E572"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 400, Date = DateTime.Parse("2023-12-12"), GeneralComments = "Bought a gaming console from BestBuy." },
                    new Transaction { TransactionID = new Guid("2D62B49A-92A3-43BC-9628-EBF167D31C8F"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("B7CAEA13-1D1A-4780-831A-ED973765C67A"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 55, Date = DateTime.Parse("2023-12-13"), GeneralComments = "Purchased a new sweater from Gap." },
                    new Transaction { TransactionID = new Guid("4F3A197B-BF8B-4745-94CC-6C1B0D1C8BAF"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 50, Date = DateTime.Parse("2023-12-15"), GeneralComments = "Enjoyed snacks during a Delta flight." },
                    new Transaction { TransactionID = new Guid("62B2C92B-9C07-4878-BB03-8E6010D653E8"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("C7EE15E0-5480-4544-B2D6-094BA5134AC7"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 120, Date = DateTime.Parse("2023-12-16"), GeneralComments = "Bought bulk groceries from Costco." },
                    new Transaction { TransactionID = new Guid("6F4F7B9C-5D96-466D-83A5-B9D77B35C4E1"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("A53C4426-DF1C-4325-8E52-4EA027A6E83E"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 50, Date = DateTime.Parse("2023-12-18"), GeneralComments = "Stocked up on snacks and drinks at Kroger." },
                    new Transaction { TransactionID = new Guid("1C02BC7E-F204-4E8F-BAE2-F8D24BC8D401"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 500, Date = DateTime.Parse("2023-12-18"), GeneralComments = "Purchased a flight ticket with American Airlines." },
                    new Transaction { TransactionID = new Guid("4F493F8C-5A80-493A-8F24-EC1D0897C3A3"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("F5186C55-332A-4EAE-B917-7F5FAE0B2A7B"), RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 70, Date = DateTime.Parse("2023-12-19"), GeneralComments = "Enjoyed in-flight entertainment on American Airlines." },
                    new Transaction { TransactionID = new Guid("2A15A4ED-134A-4268-9133-B28FF8C46A04"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("18884556-DEF7-499C-89DF-09A8D95B62D0"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 75, Date = DateTime.Parse("2023-12-20"), GeneralComments = "Bought holiday gifts at Target." },
                    new Transaction { TransactionID = new Guid("7B7F48F6-9A35-411C-B3EB-EE79D96C4F66"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("DEA8E720-E79C-4FC3-81FD-201BEFA9F904"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 20, Date = DateTime.Parse("2023-12-21"), GeneralComments = "Grabbed breakfast from Dunkin' Donuts." },
                    new Transaction { TransactionID = new Guid("5C8C8FF9-D91C-4D99-A126-36A098BEC1B4"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 30, Date = DateTime.Parse("2023-12-23"), GeneralComments = "Had dinner at Chick-fil-A with family." },
                    new Transaction { TransactionID = new Guid("3A4B28D4-2B71-42B9-A89E-13DA1D10D9D6"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("04848095-F6D3-489E-BFD2-92B09E039019"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 25, Date = DateTime.Parse("2023-12-23"), GeneralComments = "Had a late-night snack at Burger King." },
                    new Transaction { TransactionID = new Guid("1E7DBA55-D7A2-485E-B87B-64B98C7FA2C5"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 200, Date = DateTime.Parse("2023-12-25"), GeneralComments = "Purchased a tablet as a holiday gift." },
                    new Transaction { TransactionID = new Guid("6E7A7335-7E63-4E06-BC4B-36F8586BB4C4"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 500, Date = DateTime.Parse("2023-12-26"), GeneralComments = "Purchased a last-minute ticket for Delta Airlines." },
                    new Transaction { TransactionID = new Guid("4D7E41F9-505C-431E-AF61-32C84E07DF6B"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("F5186C55-332A-4EAE-B917-7F5FAE0B2A7B"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 150, Date = DateTime.Parse("2023-12-28"), GeneralComments = "Purchased tickets for a trip with Delta." },
                    new Transaction { TransactionID = new Guid("6F100D66-FC30-4FA2-BE71-CB62F16120B4"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("F5186C55-332A-4EAE-B917-7F5FAE0B2A7B"), RecipientID = new Guid("3CFFF9F1-D8B3-4396-A44F-622899681719"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 150, Date = DateTime.Parse("2023-12-28"), GeneralComments = "Attended a fashion show featuring Ralph Lauren." },
                    new Transaction { TransactionID = new Guid("4A0E6C4D-6EDB-43A6-877C-B9C96D1CDA92"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 300, Date = DateTime.Parse("2023-12-29"), GeneralComments = "Bought a new laptop from BestBuy for work." },
                    new Transaction { TransactionID = new Guid("3F35A59A-F20D-465C-8F6F-32BB0FFFB56D"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 350, Date = DateTime.Parse("2023-12-30"), GeneralComments = "Booked a flight with American Airlines for vacation." },
                    new Transaction { TransactionID = new Guid("2E8F06A1-6A58-429E-9B75-0F0191A57D91"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("03A5B5DE-E9EC-4E70-94F2-00DA5D667787"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 600, Date = DateTime.Parse("2024-01-02"), GeneralComments = "Booked a round-trip flight for a business trip." },
                    new Transaction { TransactionID = new Guid("7A67D568-C099-4E61-B067-8D16C891FA68"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 65, Date = DateTime.Parse("2024-01-03"), GeneralComments = "Purchased winter clothing from Burlington." },
                    new Transaction { TransactionID = new Guid("4C3C5D6C-2736-44D4-9D77-180A7BB7E10C"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("6ECF6C47-660E-4871-A6FC-9C17167A3B5D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 28, Date = DateTime.Parse("2024-01-06"), GeneralComments = "Had a meal at Panda Express after shopping." },
                    new Transaction { TransactionID = new Guid("3D6784C2-612A-4420-8D38-354F1D4B199C"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("DEA8E720-E79C-4FC3-81FD-201BEFA9F904"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 25, Date = DateTime.Parse("2024-01-08"), GeneralComments = "Enjoyed a coffee and pastry at Dunkin' Donuts." },
                    new Transaction { TransactionID = new Guid("4F97F8C3-F33E-4D35-912D-CA4C73F39F56"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("F5186C55-332A-4EAE-B917-7F5FAE0B2A7B"), RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 12, Date = DateTime.Parse("2024-01-09"), GeneralComments = "Watched a movie on the flight with American Airlines." },
                    new Transaction { TransactionID = new Guid("5A1D5B77-EB9A-447D-A2B9-4F7D4E82F063"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("F5186C55-332A-4EAE-B917-7F5FAE0B2A7B"), RecipientID = new Guid("3CFFF9F1-D8B3-4396-A44F-622899681719"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 150, Date = DateTime.Parse("2024-01-11"), GeneralComments = "Attended a fashion event featuring Ralph Lauren." },
                    new Transaction { TransactionID = new Guid("63BA5B48-FB89-4DBD-B40D-B55B14EB5DD7"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 499, Date = DateTime.Parse("2024-01-12"), GeneralComments = "Bought a new smartphone at BestBuy." },
                    new Transaction { TransactionID = new Guid("1F12B98E-9C29-4C41-8586-AD340A3565F9"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("B7CAEA13-1D1A-4780-831A-ED973765C67A"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 70, Date = DateTime.Parse("2024-01-15"), GeneralComments = "Purchased some new jeans at Gap." },
                    new Transaction { TransactionID = new Guid("6E2F5B73-4D75-43A4-8A80-07C20544D154"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 250, Date = DateTime.Parse("2024-01-15"), GeneralComments = "Booked a flight with Delta Airlines for vacation." },
                    new Transaction { TransactionID = new Guid("6D23E27C-477D-4C77-B69F-8D8ED593D5C9"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 250, Date = DateTime.Parse("2024-01-18"), GeneralComments = "Bought a smart home device from BestBuy." },
                    new Transaction { TransactionID = new Guid("5A8B6B9B-D8E0-49C1-AC58-BDE963635DF4"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("C7EE15E0-5480-4544-B2D6-094BA5134AC7"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 110, Date = DateTime.Parse("2024-01-18"), GeneralComments = "Did monthly shopping at Costco." },
                    new Transaction { TransactionID = new Guid("4EAA2B7C-4B58-41C3-9E4B-3A9DBE73F19A"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 45, Date = DateTime.Parse("2024-01-21"), GeneralComments = "Bought a new jacket from H&M." },
                    new Transaction { TransactionID = new Guid("3D7BAE39-2344-4A77-BD3F-C99C320D9E3C"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 700, Date = DateTime.Parse("2024-01-22"), GeneralComments = "Purchased a flight ticket for a vacation." },
                    new Transaction { TransactionID = new Guid("9D9F14D1-250E-40ED-8D42-02BB85FA72F6"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 35, Date = DateTime.Parse("2024-01-24"), GeneralComments = "Enjoyed a family dinner at Chick-fil-A." },
                    new Transaction { TransactionID = new Guid("81A012A5-F90A-4C3B-88DA-1B743A59CC4E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 40, Date = DateTime.Parse("2024-01-25"), GeneralComments = "Had a family lunch at Chick-fil-A." },
                    new Transaction { TransactionID = new Guid("C92A1D1D-22FC-408C-BDA6-98BC62330E08"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 250, Date = DateTime.Parse("2024-01-27"), GeneralComments = "Purchased a new TV from Walmart." },
                    new Transaction { TransactionID = new Guid("4A8C0043-0FAE-42AC-B4A3-FA6B3C5D60E5"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("C7EE15E0-5480-4544-B2D6-094BA5134AC7"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 110, Date = DateTime.Parse("2024-01-29"), GeneralComments = "Stocked up on groceries at Costco." },
                    new Transaction { TransactionID = new Guid("38E4E39A-4B82-44ED-B90B-563F5C2E6F45"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 700, Date = DateTime.Parse("2024-01-30"), GeneralComments = "Bought a last-minute flight ticket for travel." },
                    new Transaction { TransactionID = new Guid("2EED2E7E-8858-491F-AFF4-67C1A3130D02"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 80, Date = DateTime.Parse("2024-01-31"), GeneralComments = "Purchased kitchen appliances from Walmart." },
                    new Transaction { TransactionID = new Guid("6B4D03F4-3E5A-4324-8F9E-1A5B92980F53"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("DF7269A9-6C65-434F-BCD5-D9853B0B75B3"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 900, Date = DateTime.Parse("2024-02-01"), GeneralComments = "Booked a flight to Paris with Air France." },
                    new Transaction { TransactionID = new Guid("9D05B8E3-5B65-4703-85F1-BD9C32C60091"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("03A5B5DE-E9EC-4E70-94F2-00DA5D667787"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 850, Date = DateTime.Parse("2024-02-03"), GeneralComments = "Round-trip flight to London on United Airlines." },
                    new Transaction { TransactionID = new Guid("1CBEF37D-1B75-4BFD-97A9-9C8E468F0C78"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("B7CAEA13-1D1A-4780-831A-ED973765C67A"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 80, Date = DateTime.Parse("2024-02-08"), GeneralComments = "Purchased souvenirs at Gap before the trip." },
                    new Transaction { TransactionID = new Guid("4F8A19F6-B4D1-4A0F-BDA3-8F073B6538BC"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 700, Date = DateTime.Parse("2024-02-10"), GeneralComments = "Booked a flight to Barcelona with Delta Airlines." },
                    new Transaction { TransactionID = new Guid("8FC6D79E-C2A6-4F56-A05D-0FCA4B0BFBC5"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 300, Date = DateTime.Parse("2024-02-12"), GeneralComments = "Bought a travel adapter at BestBuy." },
                    new Transaction { TransactionID = new Guid("7D8B8C6A-3F40-4908-B25B-DB4A97E8F17E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 950, Date = DateTime.Parse("2024-02-17"), GeneralComments = "Booked a last-minute flight to Tokyo." },
                    new Transaction { TransactionID = new Guid("E43B3F93-1B7A-48A9-829E-BE9A7B99B75D"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 30, Date = DateTime.Parse("2024-02-20"), GeneralComments = "Had a quick meal at Chick-fil-A before the flight." },
                    new Transaction { TransactionID = new Guid("84B7D10A-1F4D-4A19-AFE9-F56B64F58D6E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 90, Date = DateTime.Parse("2024-02-23"), GeneralComments = "Purchased travel essentials at Burlington." },
                    new Transaction { TransactionID = new Guid("5F6182DA-B9A5-4181-BF83-0DCE3C009D9A"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("C7EE15E0-5480-4544-B2D6-094BA5134AC7"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 150, Date = DateTime.Parse("2024-02-28"), GeneralComments = "Stocked up on groceries at Costco for the trip." },
                    new Transaction { TransactionID = new Guid("D4C4089C-BF54-4825-96A5-7B444B849F2D"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 200, Date = DateTime.Parse("2024-02-29"), GeneralComments = "Purchased a camera at Walmart for travel photos." },
                    new Transaction { TransactionID = new Guid("3D5C2A86-348A-43B4-BF3D-F1E5B4B2A75F"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("C7EE15E0-5480-4544-B2D6-094BA5134AC7"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 150, Date = DateTime.Parse("2024-03-01"), GeneralComments = "Stocked up on groceries at Costco." },
                    new Transaction { TransactionID = new Guid("5A9E1D88-FF26-4F55-A4E3-5C86F5291B79"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 35, Date = DateTime.Parse("2024-03-03"), GeneralComments = "Enjoyed a family meal at Chick-fil-A." },
                    new Transaction { TransactionID = new Guid("C73C7F0E-CBF3-4C82-BDB9-8E3A8F9DFFCE"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("18884556-DEF7-499C-89DF-09A8D95B62D0"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 200, Date = DateTime.Parse("2024-03-05"), GeneralComments = "Bought household items at Target." },
                    new Transaction { TransactionID = new Guid("29D6B144-C31C-4B0D-82DB-BA042A60B8D0"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 300, Date = DateTime.Parse("2024-03-08"), GeneralComments = "Purchased a new TV at BestBuy." },
                    new Transaction { TransactionID = new Guid("6E9B83E6-3FB4-494C-BF61-B08D80F6DB93"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("DEA8E720-E79C-4FC3-81FD-201BEFA9F904"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 12, Date = DateTime.Parse("2024-03-12"), GeneralComments = "Grabbed coffee and donuts from Dunkin'." },
                    new Transaction { TransactionID = new Guid("0D5C78C6-F5E7-494F-83FB-6DA06D1A50D2"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 150, Date = DateTime.Parse("2024-03-15"), GeneralComments = "Bought clothes at Burlington." },
                    new Transaction { TransactionID = new Guid("7F8D6C5F-C0F8-4E8B-AC8C-75A5B67B5745"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("75987053-7057-499B-BDF6-14AC41509853"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 250, Date = DateTime.Parse("2024-03-15"), GeneralComments = "Purchased a flight ticket to New York with Delta." },
                    new Transaction { TransactionID = new Guid("8F2D62F3-4A2C-42C2-BE4D-BCED4D26ED96"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("A53C4426-DF1C-4325-8E52-4EA027A6E83E"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 70, Date = DateTime.Parse("2024-03-18"), GeneralComments = "Picked up groceries at Kroger." },
                    new Transaction { TransactionID = new Guid("6E86A8F7-87C5-4EB8-BC89-2ACB5C03A03A"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 180, Date = DateTime.Parse("2024-03-20"), GeneralComments = "Bought a new tablet at Walmart." },
                    new Transaction { TransactionID = new Guid("1DB79274-33E6-4D7B-849C-44FCD745EBC7"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("6ECF6C47-660E-4871-A6FC-9C17167A3B5D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 30, Date = DateTime.Parse("2024-03-25"), GeneralComments = "Had dinner at Panda Express." },
                    new Transaction { TransactionID = new Guid("3CFF4D4C-5D44-43B4-98C4-8F0A378B8D64"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 60, Date = DateTime.Parse("2024-03-27"), GeneralComments = "Bought some spring clothes at H&M." },
                    new Transaction { TransactionID = new Guid("8F30A758-BB12-4E52-B73F-4F69C9B88C60"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("04848095-F6D3-489E-BFD2-92B09E039019"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 25, Date = DateTime.Parse("2024-03-31"), GeneralComments = "Enjoyed lunch at Burger King with friends." }                    
                );

            #endregion

        }
    }
}
