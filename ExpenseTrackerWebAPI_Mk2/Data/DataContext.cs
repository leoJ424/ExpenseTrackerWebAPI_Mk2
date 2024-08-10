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

                    new Recipient { RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), RecipientName = "Amazon" },

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
                    new Transaction { TransactionID = new Guid("DD04C84A-5807-43B2-88BB-17E19E96BFAF"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 153.83, RewardPoints = 0, Date = DateTime.Parse("2023-11-03"), GeneralComments = "Bought winter wear - jackets, socks, shoes" },
                    new Transaction { TransactionID = new Guid("F2DA2972-B060-43C8-8039-6F36404EE06C"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 800, RewardPoints = 0, Date = DateTime.Parse("2024-03-05"), GeneralComments = "Purchased a new smartphone for upgraded features" },
                    new Transaction { TransactionID = new Guid("4254185D-498B-45F4-B26C-FCDC16E648F6"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 799.99, RewardPoints = 0, Date = DateTime.Parse("2024-03-05"), GeneralComments = "Purchased a new smartphone for upgraded features" },
                    new Transaction { TransactionID = new Guid("A821CE9F-E206-4C3E-BE7E-5D502C777972"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("DC769F30-BA3C-4DF8-8E9E-44D0F905DEA6"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 60, RewardPoints = 0, Date = DateTime.Parse("2024-03-10"), GeneralComments = "Purchased a new jacket for the upcoming spring season" },
                    new Transaction { TransactionID = new Guid("5C75C12B-3B90-4222-887C-001B9AE75163"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 25.75, RewardPoints = 0, Date = DateTime.Parse("2024-03-10"), GeneralComments = "Dined at Chick-fil-A for lunch" },
                    new Transaction { TransactionID = new Guid("49D48D35-39B6-409A-B2E1-2AE30C4DB609"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 100, RewardPoints = 0, Date = DateTime.Parse("2024-03-14"), GeneralComments = "Bought a new pair of sneakers for daily wear" },
                    new Transaction { TransactionID = new Guid("B55146F1-C05B-4A13-9E6F-1EB5D79B30EA"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("DEA8E720-E79C-4FC3-81FD-201BEFA9F904"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 8.45, RewardPoints = 0, Date = DateTime.Parse("2024-03-15"), GeneralComments = "Bought breakfast at Dunkin' Donuts" },
                    new Transaction { TransactionID = new Guid("E4701E3D-CE36-4662-B196-B93D7DE08973"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("18884556-DEF7-499C-89DF-09A8D95B62D0"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 80, RewardPoints = 0, Date = DateTime.Parse("2024-03-15"), GeneralComments = "Purchased a portable speaker for outdoor gatherings" },
                    new Transaction { TransactionID = new Guid("285249E4-DB59-4E36-BC15-E3496FEED924"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("73F04239-CA28-47E1-9243-A8C888C06F4C"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 600, RewardPoints = 0, Date = DateTime.Parse("2024-03-20"), GeneralComments = "Booked flights to Miami, USA for spring break" },
                    new Transaction { TransactionID = new Guid("3FFD8E08-D9A8-4676-ABDE-24639E728185"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 50, RewardPoints = 0, Date = DateTime.Parse("2024-03-25"), GeneralComments = "Purchased a new dress for a spring outing" },
                    new Transaction { TransactionID = new Guid("9D9F14D1-250E-40ED-8D42-02BB85FA72F6"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Credit, Amount = 50, RewardPoints = 0, Date = DateTime.Parse("2024-03-26"), GeneralComments = "Returned the new dress for a spring outing" },
                    new Transaction { TransactionID = new Guid("C093E452-CDCC-4B7B-A498-1095B07DF761"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("5D47A329-4A2D-4676-B349-3D26958FF25D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 14.25, RewardPoints = 0, Date = DateTime.Parse("2024-04-02"), GeneralComments = "Grabbed lunch at McDonald's" },
                    new Transaction { TransactionID = new Guid("E900CA8A-B60B-4C2E-8570-3BB42C6116DC"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("6ECF6C47-660E-4871-A6FC-9C17167A3B5D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 17.5, RewardPoints = 0, Date = DateTime.Parse("2024-04-05"), GeneralComments = "Enjoyed a meal at Panda Express for dinner" },
                    new Transaction { TransactionID = new Guid("60299A7B-25F1-4895-BBCF-0EBD697D0780"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("B7CAEA13-1D1A-4780-831A-ED973765C67A"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 60, RewardPoints = 0, Date = DateTime.Parse("2024-04-05"), GeneralComments = "Purchased a new dress for the upcoming summer season" },
                    new Transaction { TransactionID = new Guid("1A0C6C5C-FCFD-499A-8346-C0B8CD1489C0"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 300, RewardPoints = 0, Date = DateTime.Parse("2024-04-10"), GeneralComments = "Bought a new smartwatch for fitness tracking and notifications" },
                    new Transaction { TransactionID = new Guid("ADF83F65-1F1B-4915-B034-CCA4ED5E0EF6"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("3CFFF9F1-D8B3-4396-A44F-622899681719"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 400, RewardPoints = 0, Date = DateTime.Parse("2024-04-12"), GeneralComments = "Bought a new suit for upcoming business meetings" },
                    new Transaction { TransactionID = new Guid("D41A1D2D-76E2-4577-AEF0-A115183212A7"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 1200, RewardPoints = 0, Date = DateTime.Parse("2024-04-15"), GeneralComments = "Reserved flights to London, UK for a business conference" },
                    new Transaction { TransactionID = new Guid("A2850307-D063-4227-B50F-B8996118AD21"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("B7CAEA13-1D1A-4780-831A-ED973765C67A"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 70, RewardPoints = 0, Date = DateTime.Parse("2024-04-18"), GeneralComments = "Bought new jeans for casual wear" },
                    new Transaction { TransactionID = new Guid("5F89F1E1-C265-42DC-B61B-F3947C179837"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 21.5, RewardPoints = 0, Date = DateTime.Parse("2024-04-18"), GeneralComments = "Dined at Chick-fil-A for dinner" },
                    new Transaction { TransactionID = new Guid("2088043A-475B-4A61-AB6D-4E00D7E566B3"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 1200, RewardPoints = 0, Date = DateTime.Parse("2024-04-20"), GeneralComments = "Bought a new laptop for work and entertainment" },
                    new Transaction { TransactionID = new Guid("29F8A6D4-2ED1-4490-9E89-523BC3F67EC7"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 1199.5, RewardPoints = 0, Date = DateTime.Parse("2024-04-20"), GeneralComments = "Bought a new laptop for work and entertainment" },
                    new Transaction { TransactionID = new Guid("D75FF396-45E7-4462-B0F1-D076444AAD47"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 103.83, RewardPoints = 0, Date = DateTime.Parse("2024-04-21"), GeneralComments = "Pair of jeans and a couple of t-shirts" },
                    new Transaction { TransactionID = new Guid("02F1BDFF-80A5-41D1-BE93-E14D97402742"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 700, RewardPoints = 0, Date = DateTime.Parse("2024-05-05"), GeneralComments = "Bought a new digital camera for photography hobby" },
                    new Transaction { TransactionID = new Guid("F66A54E3-24D9-4842-9883-458AFABC9F2A"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("6ECF6C47-660E-4871-A6FC-9C17167A3B5D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 19.75, RewardPoints = 0, Date = DateTime.Parse("2024-05-05"), GeneralComments = "Enjoyed lunch at Panda Express" },
                    new Transaction { TransactionID = new Guid("2F5DEA5A-D1F8-4A05-8663-7C9376C0E216"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("6ECF6C47-660E-4871-A6FC-9C17167A3B5D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 19.75, RewardPoints = 0, Date = DateTime.Parse("2024-05-05"), GeneralComments = "Enjoyed lunch at Panda Express" },
                    new Transaction { TransactionID = new Guid("208DA7F5-9BBF-4566-A3E2-253E6596CF63"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("03A5B5DE-E9EC-4E70-94F2-00DA5D667787"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 2500, RewardPoints = 0, Date = DateTime.Parse("2024-05-10"), GeneralComments = "Booked tickets to Tokyo, Japan for vacation" },
                    new Transaction { TransactionID = new Guid("481F0EAE-1FC0-432B-B52E-CC93D69A62A6"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("DC769F30-BA3C-4DF8-8E9E-44D0F905DEA6"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 40, RewardPoints = 0, Date = DateTime.Parse("2024-05-12"), GeneralComments = "Bought a new hoodie for chilly evenings" },
                    new Transaction { TransactionID = new Guid("C8C934C9-1BF2-4285-BDDC-6E8652DC8E72"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 500, RewardPoints = 0, Date = DateTime.Parse("2024-05-15"), GeneralComments = "Purchased a new gaming console for leisure time" },
                    new Transaction { TransactionID = new Guid("1D2BDC9D-F1CB-4E36-A285-1B32ED3AD6FD"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("B7CAEA13-1D1A-4780-831A-ED973765C67A"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 80, RewardPoints = 0, Date = DateTime.Parse("2024-05-20"), GeneralComments = "Bought new shoes for everyday wear" },
                    new Transaction { TransactionID = new Guid("DF89EEF9-4DC9-4E5B-A6AE-CB811A2D68FE"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("04848095-F6D3-489E-BFD2-92B09E039019"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 12.35, RewardPoints = 0, Date = DateTime.Parse("2024-05-20"), GeneralComments = "Grabbed lunch at Burger King" },
                    new Transaction { TransactionID = new Guid("892DF5A2-B46A-4A63-BE9C-E3DB033EB86F"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("04848095-F6D3-489E-BFD2-92B09E039019"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 9.95, RewardPoints = 0, Date = DateTime.Parse("2024-05-23"), GeneralComments = "Bought lunch at Burger King" },
                    new Transaction { TransactionID = new Guid("E8CEEF1E-6412-43A4-BA41-17F5C57BA8FB"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 30, RewardPoints = 0, Date = DateTime.Parse("2024-05-25"), GeneralComments = "Bought new shorts for the upcoming summer season" },
                    new Transaction { TransactionID = new Guid("B3C96FAC-0A88-47FA-BEBC-996A4CD3A39E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 120, RewardPoints = 0, Date = DateTime.Parse("2024-05-28"), GeneralComments = "Purchased a new winter coat for the next winter season" },
                    new Transaction { TransactionID = new Guid("BAB80281-36D2-4B1A-844D-0B4F555CD0BD"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 15.17, RewardPoints = 0, Date = DateTime.Parse("2024-05-30"), GeneralComments = "Bought a Jacket" },
                    new Transaction { TransactionID = new Guid("5ABDC099-4BAB-4351-9A0A-4448E3391339"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 40, RewardPoints = 0, Date = DateTime.Parse("2024-05-30"), GeneralComments = "Purchased a new swimsuit for the upcoming summer season" },
                    new Transaction { TransactionID = new Guid("B7DCAE9D-3F55-4BA9-AB33-855FCD44AD77"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 33.46, RewardPoints = 0, Date = DateTime.Parse("2024-05-30"), GeneralComments = "Bought a pair of shoes" },
                    new Transaction { TransactionID = new Guid("6E2F5B73-4D75-43A4-8A80-07C20544D154"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("4D627443-1AE0-49E0-8BF9-B1EA4B8CD8E1"), RecipientID = new Guid("7BB90DA3-FEE4-4A5B-8679-5D820E90AC3D"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Credit, Amount = 33.46, RewardPoints = 0, Date = DateTime.Parse("2024-05-31"), GeneralComments = "Returned the shoes" },
                    new Transaction { TransactionID = new Guid("9FC18D82-1E91-44ED-B17D-5A90794B56D7"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 522.33, RewardPoints = 0, Date = DateTime.Parse("2024-05-31"), GeneralComments = "Bought a PS5" },
                    new Transaction { TransactionID = new Guid("BB4B5AE6-0992-42CD-99EE-0E4B6D354BCA"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 522.33, RewardPoints = 0, Date = DateTime.Parse("2024-05-31"), GeneralComments = "Bought a new laptop" },
                    new Transaction { TransactionID = new Guid("0321DCF8-9DDC-4B11-A880-9EDBC71E977A"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 522.33, RewardPoints = 0, Date = DateTime.Parse("2024-05-31"), GeneralComments = "Bought a PS5" },
                    new Transaction { TransactionID = new Guid("CF6B8A50-CB07-4224-AB97-A81FC20A29FF"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("DFB07EB1-81E9-4139-8419-BA545F0DB07A"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 150.75, RewardPoints = 0, Date = DateTime.Parse("2024-06-01"), GeneralComments = "Weekly grocery shopping at H-E-B" },
                    new Transaction { TransactionID = new Guid("944F7C7F-5706-4673-ACBA-598111F44441"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("18884556-DEF7-499C-89DF-09A8D95B62D0"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 120.5, RewardPoints = 0, Date = DateTime.Parse("2024-06-01"), GeneralComments = "Purchased groceries and household items" },
                    new Transaction { TransactionID = new Guid("44A1CEBE-267F-4699-8C49-91BDC8B8DDF0"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 45.6, RewardPoints = 0, Date = DateTime.Parse("2024-06-02"), GeneralComments = "Dinner at Chick-fil-A" },
                    new Transaction { TransactionID = new Guid("A7186DBD-FC28-44DC-91BF-DD30EDB24535"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 85.75, RewardPoints = 0, Date = DateTime.Parse("2024-06-02"), GeneralComments = "Bought household items" },
                    new Transaction { TransactionID = new Guid("F8CA31EE-BEA6-4F45-A0A8-4C1F80329336"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("41F99A2A-3FED-4BEC-BEE2-465F46F933A5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 22.5, RewardPoints = 0, Date = DateTime.Parse("2024-06-03"), GeneralComments = "Lunch with friends" },
                    new Transaction { TransactionID = new Guid("D483FB6B-134D-400C-B03E-1C5F304B0AA0"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("B72E8DC1-1BC0-4B1E-B086-E412ED4A9DC0"), RecipientID = new Guid("3EBAA280-3EC4-4B68-AA21-F9B2FE2CBD63"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 200, RewardPoints = 0, Date = DateTime.Parse("2024-06-03"), GeneralComments = "Prescription refill at CVS" },
                    new Transaction { TransactionID = new Guid("8A564D1A-8A57-447C-A2AC-10F5316FDABF"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("DF7269A9-6C65-434F-BCD5-D9853B0B75B3"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 1200, RewardPoints = 0, Date = DateTime.Parse("2024-06-04"), GeneralComments = "Flight tickets with Air France" },
                    new Transaction { TransactionID = new Guid("630F921A-49AE-4E03-B5FA-211649B1FE84"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("03A5B5DE-E9EC-4E70-94F2-00DA5D667787"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 350, RewardPoints = 0, Date = DateTime.Parse("2024-06-04"), GeneralComments = "Flight ticket" },
                    new Transaction { TransactionID = new Guid("67B7CE82-5AEA-493C-81E8-00A7F14F228B"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("03A5B5DE-E9EC-4E70-94F2-00DA5D667787"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Credit, Amount = 350, RewardPoints = 0, Date = DateTime.Parse("2024-06-05"), GeneralComments = "Flight ticket - Refund" },
                    new Transaction { TransactionID = new Guid("866D56F8-F87A-4C5F-AD2A-7ACB32FBA09A"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 320.5, RewardPoints = 0, Date = DateTime.Parse("2024-06-05"), GeneralComments = "Bought electronics from Amazon" },
                    new Transaction { TransactionID = new Guid("D0C989E3-07E0-423B-A547-AF6B82FFED0E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 1200, RewardPoints = 0, Date = DateTime.Parse("2024-06-05"), GeneralComments = "Bought new laptop" },
                    new Transaction { TransactionID = new Guid("892D507A-DC0C-4EFA-9AE2-353244834FE8"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("A53C4426-DF1C-4325-8E52-4EA027A6E83E"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 65.25, RewardPoints = 0, Date = DateTime.Parse("2024-06-06"), GeneralComments = "Weekly groceries" },
                    new Transaction { TransactionID = new Guid("9E74A91F-450E-4280-A876-86AAA1E3C7B1"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 75.2, RewardPoints = 0, Date = DateTime.Parse("2024-06-08"), GeneralComments = "Bought new clothes" },
                    new Transaction { TransactionID = new Guid("6CA5710B-FCE0-4958-93CD-63BEB582576E"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("C7EE15E0-5480-4544-B2D6-094BA5134AC7"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 150.75, RewardPoints = 0, Date = DateTime.Parse("2024-06-10"), GeneralComments = "Bulk shopping for the month" },
                    new Transaction { TransactionID = new Guid("4F657CBB-04F2-4C6C-8288-E05C6BF637E2"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("18884556-DEF7-499C-89DF-09A8D95B62D0"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 95, RewardPoints = 0, Date = DateTime.Parse("2024-06-12"), GeneralComments = "Bought household essentials" },
                    new Transaction { TransactionID = new Guid("5552D052-F5F6-4544-B083-A2F71D594F46"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("B72E8DC1-1BC0-4B1E-B086-E412ED4A9DC0"), RecipientID = new Guid("3EBAA280-3EC4-4B68-AA21-F9B2FE2CBD63"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 35.5, RewardPoints = 0, Date = DateTime.Parse("2024-06-13"), GeneralComments = "Prescription" },
                    new Transaction { TransactionID = new Guid("666B5BEA-CAD5-4796-91ED-DA3CBA658148"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 1200, RewardPoints = 0, Date = DateTime.Parse("2024-06-19"), GeneralComments = "Booked flight tickets to Paris, France for summer vacation" },
                    new Transaction { TransactionID = new Guid("F42DC138-78C5-487D-AFEE-90B6A0B22651"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("03A5B5DE-E9EC-4E70-94F2-00DA5D667787"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 2500, RewardPoints = 0, Date = DateTime.Parse("2024-06-20"), GeneralComments = "Reserved flights to Tokyo, Japan for a cultural exploration trip" },
                    new Transaction { TransactionID = new Guid("77C968F7-4077-4B39-B146-1819953DF416"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("4575E719-3887-413B-9B30-326CF11D27F7"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 400, RewardPoints = 0, Date = DateTime.Parse("2024-06-21"), GeneralComments = "Booked tickets to Los Angeles, USA for a weekend getaway" },
                    new Transaction { TransactionID = new Guid("0EA1410B-E394-4F42-A622-EC8FE7B33C46"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1B5A7D64-2E25-48CD-8CD6-9587F6078CA5"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 1200, RewardPoints = 0, Date = DateTime.Parse("2024-06-22"), GeneralComments = "Purchased a powerful laptop for work and gaming" },
                    new Transaction { TransactionID = new Guid("AC828FE5-632D-4D52-8917-C817338253E6"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 800, RewardPoints = 0, Date = DateTime.Parse("2024-06-23"), GeneralComments = "Upgraded to the latest smartphone model" },
                    new Transaction { TransactionID = new Guid("CC218736-5271-43A3-9384-E21D3CD989D7"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 500, RewardPoints = 24.45, Date = DateTime.Parse("2024-06-24"), GeneralComments = "Bought the latest gaming console with accessories" },
                    new Transaction { TransactionID = new Guid("19E89B80-8561-49E0-B874-F49A305770DA"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("7CA7FCB7-5650-41BA-BCFA-41A1E428ED6E"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 1500, RewardPoints = 0, Date = DateTime.Parse("2024-06-25"), GeneralComments = "Bought a large smart TV for the living room" },
                    new Transaction { TransactionID = new Guid("8F2D62F3-4A2C-42C2-BE4D-BCED4D26ED96"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("8815AC70-1C20-4167-A045-964D819E195F"), RecipientID = new Guid("A53C4426-DF1C-4325-8E52-4EA027A6E83E"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 70, RewardPoints = 0, Date = DateTime.Parse("2024-07-18"), GeneralComments = "Picked up groceries at Kroger." },
                    new Transaction { TransactionID = new Guid("6E86A8F7-87C5-4EB8-BC89-2ACB5C03A03A"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A5C6D659-AAF2-495F-AFFC-015627059750"), RecipientID = new Guid("1059D5F9-E4AA-4C06-8F35-E664B6215378"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 180, RewardPoints = 0, Date = DateTime.Parse("2024-07-20"), GeneralComments = "Bought a new tablet at Walmart." },
                    new Transaction { TransactionID = new Guid("1DB79274-33E6-4D7B-849C-44FCD745EBC7"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("6ECF6C47-660E-4871-A6FC-9C17167A3B5D"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 30, RewardPoints = 0, Date = DateTime.Parse("2024-07-25"), GeneralComments = "Had dinner at Panda Express." },
                    new Transaction { TransactionID = new Guid("3CFF4D4C-5D44-43B4-98C4-8F0A378B8D64"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("43A9D80E-581D-4639-9BCD-E062E7AC7176"), RecipientID = new Guid("FEC14558-524A-46A1-AB0E-F8ACFAF9AB49"), CreditCardID = new Guid("CD4E4392-2FE6-4000-8F7B-D4C3885A0F7D"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 60, RewardPoints = 0, Date = DateTime.Parse("2024-07-27"), GeneralComments = "Bought some spring clothes at H&M." },
                    new Transaction { TransactionID = new Guid("8F30A758-BB12-4E52-B73F-4F69C9B88C60"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("9AD4CCF1-F72B-4FE2-9C76-E91CD01445D8"), RecipientID = new Guid("04848095-F6D3-489E-BFD2-92B09E039019"), CreditCardID = new Guid("566863E0-CAC1-427B-9F22-74519D3970D9"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 25, RewardPoints = 0, Date = DateTime.Parse("2024-07-31"), GeneralComments = "Enjoyed lunch at Burger King with friends." },
                    new Transaction { TransactionID = new Guid("7F8D6C5F-C0F8-4E8B-AC8C-75A5B67B5745"), UserID = new Guid("8DAA3821-3685-4299-A172-4BBF18929A73"), CategoryID = new Guid("A6D314D6-AB99-47F9-9B17-2C94F9F03086"), RecipientID = new Guid("7431DC59-2E5A-4863-A58B-05E17566E310"), CreditCardID = new Guid("75987053-7057-499B-BDF6-14AC41509853"), PaymentMode = PaymentModeEnum.Credit_Card, TransactionMode = TransactionModeEnum.Debit, Amount = 250, RewardPoints = 0, Date = DateTime.Parse("2024-07-31"), GeneralComments = "Purchased a flight ticket to New York with Delta." }
                );

            #endregion

        }
    }
}
