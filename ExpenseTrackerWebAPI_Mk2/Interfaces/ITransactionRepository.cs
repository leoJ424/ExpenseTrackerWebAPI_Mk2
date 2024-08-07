using ExpenseTrackerWebAPI_Mk2.Dto;
using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<TransactionDate_AmountPairs> GetCardDebitTransactionAmountsGroupByDate(DateTime date1, DateTime date2, Guid CreditCardId);
        ICollection<TransactionDate_AmountPairs> GetCardCreditTransactionAmountsGroupByDate(DateTime date1, DateTime date2, Guid CreditCardId);
        ICollection<TransactionMonth_AmountPairs> GetCardDebitTransactionAmountsGroupByMonth(DateTime date1, DateTime date2, Guid CreditCardId);
        ICollection<TransactionMonth_AmountPairs> GetCardCreditTransactionAmountsGroupByMonth(DateTime date1, DateTime date2, Guid CreditCardId);
        DateTime GetEarliestTransactionDateOnCard(Guid CreditCardId);
        DateTime GetLatestTransactionDateOnCard(Guid CreditCardId);
        ICollection<TransactionCategory_AmountPairs> GetCardTransactionAmountsGroupByCategory(DateTime date1, DateTime date2, Guid CreditCardId);
        ICollection<TransactionDetailsDto> GetTransactionDetailsForView(DateTime date1, DateTime date2, Guid CardId);
        bool CreateTransaction(Transaction transaction);
        bool Save();
    }
}
