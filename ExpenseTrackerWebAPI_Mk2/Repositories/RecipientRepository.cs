using ExpenseTrackerWebAPI_Mk2.Data;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Repositories
{
    public class RecipientRepository : IRecipientRepository
    {
        private readonly DataContext _context;

        public RecipientRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Recipient> GetAllRecipients()
        {
            return _context.Recipients.OrderBy(r => r.RecipientName).ToList();
        }

        public Guid GetRecipientId(string recipientName)
        {
            return _context.Recipients.Where(r => r.RecipientName == recipientName && r.Status == true).Select(r => r.RecipientID).FirstOrDefault();
        }

        public string GetRecipientName(Guid recipientId)
        {
            return _context.Recipients.Where(r => r.RecipientID == recipientId && r.Status == true).Select(r => r.RecipientName).FirstOrDefault();
        }

        public bool RecipientExists(Guid recipientId)
        {
            return _context.Recipients.Any(r => r.RecipientID == recipientId && r.Status == true);
        }

        public bool RecipientExists(string recipientName)
        {
            return _context.Recipients.Any(r => r.RecipientName == recipientName && r.Status == true);
        }

        public bool CreateRecipient(Recipient recipient)
        {
            _context.Add(recipient);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
