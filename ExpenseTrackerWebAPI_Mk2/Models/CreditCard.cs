namespace ExpenseTrackerWebAPI_Mk2.Models
{
    public class CreditCard
    {
        public Guid CardID { get; set; }
        public string First4Digits { get; set; }
        public string Second4Digits { get; set; }
        public string Third4Digits { get; set; }
        public string Last4Digits { get; set; }
        public string CardName { get; set; }
        public string CardHolderName { get; set; }
        public string NetworkName { get; set; }
        public string BankName { get; set; }
        public DateTime ExpDate { get; set; }
        public int CVC { get; set; }
        public double CreditLimit { get; set; }
        public int StatementGenDate { get; set; }
        public int PaymentDueIn { get; set; }
        public bool Status { get; set; }

        //One to many relations

        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
