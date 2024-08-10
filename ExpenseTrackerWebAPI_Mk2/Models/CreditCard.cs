using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerWebAPI_Mk2.Models
{
    public enum NetworkEnum
    {
        VISA,
        MasterCard,
        American_Express,
        Discover
    }

    public class CreditCard
    {
        public Guid CreditCardID { get; set; }
        public string First4Digits { get; set; }
        public string Second4Digits { get; set; }
        public string Third4Digits { get; set; }
        public string Last4Digits { get; set; }
        public string CardName { get; set; }
        public string CardHolderName { get; set; }
        public NetworkEnum Network { get; set; }
        public string BankName { get; set; }
        public DateTime ExpDate { get; set; }
        public int CVC { get; set; }
        public double CreditLimit { get; set; }
        public int StatementGenDay { get; set; }
        public int PaymentDueIn { get; set; }
        public bool Status { get; set; }

        //Foreign Key
        public Guid UserID { get; set; }

        //One to many relations
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
