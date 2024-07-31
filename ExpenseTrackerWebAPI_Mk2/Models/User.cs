﻿namespace ExpenseTrackerWebAPI_Mk2.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }

        //One to many relations
        public ICollection<Bank> Banks { get; set; }
        public ICollection<CreditCard> CreditCards { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
