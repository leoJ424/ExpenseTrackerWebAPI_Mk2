﻿using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Interfaces
{
    public interface ICreditCardRepository
    {
        ICollection<Guid> GetCardIdsOfUser(Guid userId);
        CreditCard GetCardDetails(Guid creditCardId);
        CreditCard GetMaskedCardDetails(Guid creditCardId);
        string GetCardName(Guid creditCardId);
        bool CreditCardExists(Guid creditCardId);

    }
}
