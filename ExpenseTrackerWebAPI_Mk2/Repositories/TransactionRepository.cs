using ExpenseTrackerWebAPI_Mk2.Data;
using ExpenseTrackerWebAPI_Mk2.Dto;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace ExpenseTrackerWebAPI_Mk2.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<TransactionDate_AmountPairs> GetCardCreditTransactionAmountsGroupByDate(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            return _context.Transactions.Where(t => t.Date >= date1 && t.Date <= date2 && t.CreditCardID == CreditCardId && t.TransactionMode == Models.TransactionModeEnum.Credit && t.Status == true)
                                        .GroupBy(t => t.Date)
                                        .Select(s => new TransactionDate_AmountPairs
                                        {
                                            Date = s.Key,
                                            Amount = s.Sum(t => t.Amount),
                                        }).OrderBy(tdap => tdap.Date).ToList();
        }

        public ICollection<TransactionMonth_AmountPairs> GetCardCreditTransactionAmountsGroupByMonth(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            return _context.Transactions.Where(t => t.Date >= date1 && t.Date <= date2 && t.CreditCardID == CreditCardId && t.TransactionMode == Models.TransactionModeEnum.Credit && t.Status == true)
                                        .GroupBy(t => t.Date.Month)
                                        .Select(s => new TransactionMonth_AmountPairs
                                        {
                                            Month = s.Key,
                                            Amount = s.Sum(t => t.Amount),
                                        }).OrderBy(tmap => tmap.Month).ToList();
        }

        public ICollection<TransactionDate_AmountPairs> GetCardDebitTransactionAmountsGroupByDate(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            return _context.Transactions.Where(t => t.Date >= date1 && t.Date <= date2 && t.CreditCardID == CreditCardId && t.TransactionMode == Models.TransactionModeEnum.Debit && t.Status == true)
                                        .GroupBy(t => t.Date)
                                        .Select(s => new TransactionDate_AmountPairs 
                                        {
                                            Date = s.Key,
                                            Amount = s.Sum(t => t.Amount),
                                        }).OrderBy(tdap => tdap.Date).ToList();
        }

        public ICollection<TransactionMonth_AmountPairs> GetCardDebitTransactionAmountsGroupByMonth(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            return _context.Transactions.Where(t => t.Date >= date1 && t.Date <= date2 && t.CreditCardID == CreditCardId && t.TransactionMode == Models.TransactionModeEnum.Debit && t.Status == true)
                                        .GroupBy(t => t.Date.Month)
                                        .Select(s => new TransactionMonth_AmountPairs
                                        {
                                            Month = s.Key,
                                            Amount = s.Sum(t => t.Amount),
                                        }).OrderBy(tmap => tmap.Month).ToList();
        }

        public ICollection<TransactionCategory_AmountPairs> GetCardTransactionAmountsGroupByCategory(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            var debitTransactions = _context.Transactions.Where(t => t.Date >= date1 && t.Date <= date2 && t.CreditCardID == CreditCardId && t.TransactionMode == Models.TransactionModeEnum.Debit && t.Status == true)
                                                         .GroupBy(t => t.CategoryID)
                                                         .Select(s => new TransactionCategory_AmountPairs
                                                         {
                                                             CategoryId = s.Key,
                                                             Amount = s.Sum(t => t.Amount)
                                                         }).ToList();


            var creditTransactions = _context.Transactions.Where(t => t.Date >= date1 && t.Date <= date2 && t.CreditCardID == CreditCardId && t.TransactionMode == Models.TransactionModeEnum.Credit && t.Status == true)
                                                          .GroupBy(t => t.CategoryID)
                                                          .Select(s => new TransactionCategory_AmountPairs
                                                          {
                                                              CategoryId = s.Key,
                                                              Amount = s.Sum(t => t.Amount)
                                                          }).ToList();

            return debitTransactions.GroupJoin(creditTransactions, dt => dt.CategoryId, ct => ct.CategoryId, (dt, ct) => new { dt, ct = ct.DefaultIfEmpty() })
                                    .SelectMany(x => x.ct,
                                                (x, ct) => new TransactionCategory_AmountPairs
                                                {
                                                    Amount = x.dt.Amount - (ct?.Amount ?? 0),
                                                    CategoryId = x.dt.CategoryId
                                                }).OrderByDescending(tcap => tcap.Amount).Take(4).ToList();

            
        }

        public DateTime GetEarliestTransactionDateOnCard(Guid CreditCardId)
        {
            return _context.Transactions.Where(t => t.CreditCardID == CreditCardId && t.Status == true)
                                        .OrderBy(t => t.Date)
                                        .Select(t => t.Date).FirstOrDefault();
        }

        public DateTime GetLatestTransactionDateOnCard(Guid CreditCardId)
        {
            return _context.Transactions.Where(t => t.CreditCardID == CreditCardId && t.Status == true)
                                        .OrderByDescending(t => t.Date)
                                        .Select(t => t.Date).FirstOrDefault();
        }

        public ICollection<TransactionDetailsDto> GetTransactionDetailsForView(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            return _context.Transactions.Where(t => t.Date >= date1 && t.Date <= date2 && t.CreditCardID == CreditCardId && t.Status == true)
                                        .Join(_context.Categories, t => t.CategoryID, c => c.CategoryID, (t, c) => new { t, c.CategoryName })
                                        .Join(_context.Recipients, t => t.t.RecipientID, r => r.RecipientID, (t, r) => new TransactionDetailsDto
                                        {
                                            CategoryName = t.CategoryName,
                                            RecipientName = r.RecipientName,
                                            Amount = t.t.Amount,
                                            PaymentMode = t.t.PaymentMode,
                                            TransactionMode = t.t.TransactionMode,
                                            RewardPoints = t.t.RewardPoints ?? 0,
                                            Date = t.t.Date,
                                            GeneralComments = t.t.GeneralComments,
                                        }).OrderBy(tdd => tdd.Date).ToList();
                                            
        }

        public bool CreateTransaction(Transaction transaction)
        {
            _context.Add(transaction);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
