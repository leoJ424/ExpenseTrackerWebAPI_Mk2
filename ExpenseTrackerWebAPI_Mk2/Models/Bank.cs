namespace ExpenseTrackerWebAPI_Mk2.Models
{
    public class Bank
    {
        public Guid BankID { get; set; }
        public string BankName { get; set; }
        public string AccNumber { get; set; }
        public double Balance { get; set; }
        public int Status { get; set; }
    }
}
