namespace ExpenseTrackerWebAPI_Mk2.Models
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; }

        //One to many relations
        public ICollection<Transaction> Transactions { get; set; }
    }
}
