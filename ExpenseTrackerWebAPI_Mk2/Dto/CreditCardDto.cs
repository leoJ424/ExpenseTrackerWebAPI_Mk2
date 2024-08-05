using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Dto
{
    public class CreditCardDto
    {
        public Guid CreditCardID { get; set; }
        public string First4Digits { get; set; }
        public string Second4Digits { get; set; }
        public string Third4Digits { get; set; }
        public string Last4Digits { get; set; }
        public string CardName { get; set; }
        public string CardholderName { get; set; }
        public NetworkEnum Network { get; set; }
        public string BankName { get; set; }
        public DateTime ExpDate { get; set; }
        public int CVC { get; set; }
        public double CreditLimit { get; set; }
        public int StatementGenDate { get; set; }
        public int PaymentDueIn { get; set; }
        
    }
}
