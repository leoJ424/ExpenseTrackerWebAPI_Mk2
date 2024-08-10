namespace ExpenseTrackerWebAPI_Mk2.Models
{
    public class Recipient
    {
        public Guid RecipientID { get; set; }
        public string RecipientName { get; set; }
        public bool Status { get; set; }

        //One to many relations
        public ICollection<Transaction> Transactions { get; set; }
    }
}
