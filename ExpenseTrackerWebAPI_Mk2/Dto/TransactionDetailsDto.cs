using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Dto
{
    public class TransactionDetailsDto
    {
        public string CategoryName { get; set; }
        public string RecipientName { get; set; }
        public double Amount { get; set; }
        public PaymentModeEnum PaymentMode { get; set; }
        public TransactionModeEnum TransactionMode { get; set; }
        public double RewardPoints { get; set; }
        public DateTime Date { get; set; }
        public string? GeneralComments { get; set; }
    }
}
