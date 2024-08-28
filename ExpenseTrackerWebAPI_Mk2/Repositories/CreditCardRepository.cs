using ExpenseTrackerWebAPI_Mk2.Data;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly DataContext _context;

        public CreditCardRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Guid> GetCardIdsOfUser(Guid userId)
        {
            return _context.CreditCards.Where(cc => cc.UserID == userId && cc.Status == true)
                                       .OrderBy(cc => cc.CardName)
                                       .Select(cc => cc.CreditCardID).ToList();
        }

        public CreditCard GetCardDetails(Guid creditCardId)
        {
            return _context.CreditCards.Where(cc => cc.CreditCardID == creditCardId && cc.Status == true).FirstOrDefault();
        }

        public string GetCardNumber(Guid creditCardId)
        {
            var cardNumber = "";

            var f = _context.CreditCards.Where(cc => cc.CreditCardID == creditCardId && cc.Status == true).Select(cc => cc.First4Digits).FirstOrDefault();
            var s = _context.CreditCards.Where(cc => cc.CreditCardID == creditCardId && cc.Status == true).Select(cc => cc.Second4Digits).FirstOrDefault();
            var t = _context.CreditCards.Where(cc => cc.CreditCardID == creditCardId && cc.Status == true).Select(cc => cc.Third4Digits).FirstOrDefault();
            var l = _context.CreditCards.Where(cc => cc.CreditCardID == creditCardId && cc.Status == true).Select(cc => cc.Last4Digits).FirstOrDefault();

            cardNumber += f + " " + s + " " + t + " " + l;

            return cardNumber;
        }

        public CreditCard GetMaskedCardDetails(Guid creditCardId)
        {
            var card = _context.CreditCards.Where(cc => cc.CreditCardID == creditCardId && cc.Status == true).FirstOrDefault();

            if (card != null)
            {
                card.First4Digits = ". . . .";
                card.Second4Digits = ". . . .";
                card.Third4Digits = ". . . .";
                card.CVC = 0;
            }
            return card;
        }

        public Guid GetCardOwner(Guid creditCardId)
        {
            return _context.CreditCards.Where(cc => cc.CreditCardID == creditCardId && cc.Status == true).Select(cc => cc.UserID).FirstOrDefault();
        }

        public string GetCardName(Guid creditCardId)
        {
            return _context.CreditCards.Where(cc => cc.CreditCardID == creditCardId && cc.Status == true).Select(cc => cc.CardName).FirstOrDefault();
        }

        public bool CreditCardExists(Guid creditCardId)
        {
            return _context.CreditCards.Any(cc => cc.CreditCardID == creditCardId && cc.Status == true);
        }

        public bool CreateCreditCard(CreditCard card)
        {
            _context.Add(card);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
