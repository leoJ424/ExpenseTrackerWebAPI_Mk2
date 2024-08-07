using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Interfaces
{
    public interface IRecipientRepository
    {
        ICollection<Recipient> GetAllRecipients();
        string GetRecipientName(Guid recipientId);
        Guid GetRecipientId(string recipientName);
        bool RecipientExists(Guid recipientId);
        bool RecipientExists(string recipientName);

        //Create
        bool CreateRecipient(Recipient recipient);

        bool Save();

    }
}
