namespace ExpenseTrackerWebAPI_Mk2.Models
{
    public enum PaymentModeEnum
    {
        Bank,
        Credit_Card
    }

    public enum TransactionModeEnum
    {
        Credit,
        Debit
    }
    public class Transaction
    {
        public Guid TransactionID { get; set; }
        public PaymentModeEnum PaymentMode { get; set; }
        public TransactionModeEnum TransactionMode { get; set; }
        public double Amount { get; set; }
        public double? RewardPoints { get; set; }
        public DateTime Date { get; set; }
        public string? GeneralComments { get; set; }
        public bool Status { get; set; }

        //Foreign Keys

        public Guid UserID { get; set; }
        public Guid CategoryID { get; set; }
        public Guid RecipientID { get; set; }
        public Guid? BankID { get; set; }
        public Guid? CreditCardID { get; set; }

        //One to many relations
        public User User { get; set; }
        public Category Category { get; set; }
        public Recipient Recipient { get; set; }
        public Bank? Bank { get; set; }
        public CreditCard? CreditCard { get; set; }
    }
}
