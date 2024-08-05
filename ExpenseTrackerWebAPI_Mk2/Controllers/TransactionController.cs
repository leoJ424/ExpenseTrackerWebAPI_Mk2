using AutoMapper;
using ExpenseTrackerWebAPI_Mk2.Dto;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerWebAPI_Mk2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactiontRepository;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactiontRepository = transactionRepository;
            _mapper = mapper;
        }

        [HttpGet("Credit_TransactionDetails_Datewise")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDate_AmountPairs>))]
        [ProducesResponseType(400)]
        public IActionResult GetCardCreditTransactionAmountsGroupByDate(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            var result = _transactiontRepository.GetCardCreditTransactionAmountsGroupByDate(date1, date2 , CreditCardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("Credit_TransactionDetails_Monthwise")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionMonth_AmountPairs>))]
        [ProducesResponseType(400)]
        public IActionResult GetCardCreditTransactionAmountsGroupByMonth(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            var result = _transactiontRepository.GetCardCreditTransactionAmountsGroupByMonth(date1, date2, CreditCardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("Debit_TransactionDetails_Datewise")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDate_AmountPairs>))]
        [ProducesResponseType(400)]
        public IActionResult GetCardDebitTransactionAmountsGroupByDate(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            var result = _transactiontRepository.GetCardDebitTransactionAmountsGroupByDate(date1, date2, CreditCardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("Debit_TransactionDetails_Monthwise")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionMonth_AmountPairs>))]
        [ProducesResponseType(400)]
        public IActionResult GetCardDebitTransactionAmountsGroupByMonth(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            var result = _transactiontRepository.GetCardDebitTransactionAmountsGroupByMonth(date1, date2, CreditCardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("TransactionDetailsByCategory")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionCategory_AmountPairs>))]
        [ProducesResponseType(400)]
        public IActionResult GetCardTransactionAmountsGroupByCategory(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            var result = _transactiontRepository.GetCardTransactionAmountsGroupByCategory(date1, date2, CreditCardId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("TransactionDetailsEarliestDate")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public IActionResult GetEarliestTransactionDateOnCard(Guid CreditCardId)
        {
            var result = _transactiontRepository.GetEarliestTransactionDateOnCard(CreditCardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("TransactionDetailsLatestDate")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public IActionResult GetLatestTransactionDateOnCard(Guid CreditCardId)
        {
            var result = _transactiontRepository.GetLatestTransactionDateOnCard(CreditCardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("TransactionDetailsDetailed")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDetailsDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTransactionDetailsForView(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            var result = _transactiontRepository.GetTransactionDetailsForView(date1, date2, CreditCardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }


    }
}
