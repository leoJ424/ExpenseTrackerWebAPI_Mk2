namespace ExpenseTrackerWebAPI_Mk2.Models
{
    public class Bank
    {
        public Guid BankID { get; set; }
        public string BankName { get; set; }
        public string AccNumber { get; set; }
        public double Balance { get; set; }
        public bool Status { get; set; }

        //One-to-many relations
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
