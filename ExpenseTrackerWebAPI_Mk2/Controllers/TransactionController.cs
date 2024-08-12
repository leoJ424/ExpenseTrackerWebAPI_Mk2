using AutoMapper;
using ExpenseTrackerWebAPI_Mk2.Dto;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;
using ExpenseTrackerWebAPI_Mk2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenseTrackerWebAPI_Mk2.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactiontRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IRecipientRepository _recipientRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepository transactionRepository,IUserRepository userRepository, ICreditCardRepository creditCardRepository, ICategoryRepository categoryRepository, IRecipientRepository recipientRepository, IMapper mapper)
        {
            _transactiontRepository = transactionRepository;
            _userRepository = userRepository;
            _creditCardRepository = creditCardRepository;
            _categoryRepository = categoryRepository;
            _recipientRepository = recipientRepository;
            _mapper = mapper;
        }

        [HttpGet("Credit_TransactionDetails_Datewise")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDate_AmountPairs>))]
        [ProducesResponseType(400)]
        public IActionResult GetCardCreditTransactionAmountsGroupByDate(DateTime date1, DateTime date2, Guid CreditCardId)
        {
            #region Check if card belongs to user submitting the request

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization Header missing or irrelevent");
            }

            var token = authHeader.Substring(7).Trim(); //7 beacuse "Bearer " has 7 characters
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userID")?.Value;
            if (claimUserId == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (claimUserId != _creditCardRepository.GetCardOwner(CreditCardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

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
            #region Check if card belongs to user submitting the request

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization Header missing or irrelevent");
            }

            var token = authHeader.Substring(7).Trim(); //7 beacuse "Bearer " has 7 characters
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userID")?.Value;
            if (claimUserId == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (claimUserId != _creditCardRepository.GetCardOwner(CreditCardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

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
            #region Check if card belongs to user submitting the request

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization Header missing or irrelevent");
            }

            var token = authHeader.Substring(7).Trim(); //7 beacuse "Bearer " has 7 characters
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userID")?.Value;
            if (claimUserId == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (claimUserId != _creditCardRepository.GetCardOwner(CreditCardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

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
            #region Check if card belongs to user sublitting the request

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization Header missing or irrelevent");
            }

            var token = authHeader.Substring(7).Trim(); //7 beacuse "Bearer " has 7 characters
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userID")?.Value;
            if (claimUserId == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (claimUserId != _creditCardRepository.GetCardOwner(CreditCardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

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
            #region Check if card belongs to user submitting the request

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization Header missing or irrelevent");
            }

            var token = authHeader.Substring(7).Trim(); //7 beacuse "Bearer " has 7 characters
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userID")?.Value;
            if (claimUserId == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (claimUserId != _creditCardRepository.GetCardOwner(CreditCardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

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
            #region Check if card belongs to user submitting the request

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization Header missing or irrelevent");
            }

            var token = authHeader.Substring(7).Trim(); //7 beacuse "Bearer " has 7 characters
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userID")?.Value;
            if (claimUserId == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (claimUserId != _creditCardRepository.GetCardOwner(CreditCardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

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
            #region Check if card belongs to user submitting the request

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization Header missing or irrelevent");
            }

            var token = authHeader.Substring(7).Trim(); //7 beacuse "Bearer " has 7 characters
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userID")?.Value;
            if (claimUserId == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (claimUserId != _creditCardRepository.GetCardOwner(CreditCardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

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
            #region Check if card belongs to user submitting the request

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized("Authorization Header missing or irrelevent");
            }

            var token = authHeader.Substring(7).Trim(); //7 beacuse "Bearer " has 7 characters
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userID")?.Value;
            if (claimUserId == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (claimUserId != _creditCardRepository.GetCardOwner(CreditCardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

            var result = _transactiontRepository.GetTransactionDetailsForView(date1, date2, CreditCardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTransaction([FromQuery] string userName, [FromQuery] string? cardName, [FromQuery] string? bankName, [FromBody] TransactionDetailsDto transactionDetails)
        {          
            if (transactionDetails == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactionMap = _mapper.Map<Transaction>(transactionDetails);
            transactionMap.Status = true;

            Guid userId = _userRepository.GetUserId(userName);
            if (userId == Guid.Empty)
            {
                ModelState.AddModelError("", "User does not exist");
                return StatusCode(422, ModelState);
            }   
            transactionMap.UserID = userId;

            Guid categoryId = _categoryRepository.GetCategoryId(transactionDetails.CategoryName);
            if (categoryId == Guid.Empty)
            {
                ModelState.AddModelError("", "Category does not exist");
                return StatusCode(422, ModelState);
            }
            transactionMap.CategoryID = categoryId;

            Guid recipientId = _recipientRepository.GetRecipientId(transactionDetails.RecipientName);
            if (recipientId == Guid.Empty)
            {
                ModelState.AddModelError("", "Recipient does not exist");
                return StatusCode(422, ModelState);
            }
            transactionMap.RecipientID = recipientId;

            if (transactionDetails.PaymentMode == PaymentModeEnum.Credit_Card)
            {
                var existingCards = _creditCardRepository.GetCardIdsOfUser(userId);
                bool check = false;
                Guid creditCardId = Guid.Empty;
                foreach (var cardId in existingCards)
                {
                    var existingCardName = _creditCardRepository.GetCardName(cardId);
                    if (existingCardName == cardName)
                    {
                        check = true;
                        creditCardId = cardId;
                        break;
                    }
                }

                if (!check)
                {
                    ModelState.AddModelError("", "Card does not exist");
                    return StatusCode(422, ModelState);
                }

                transactionMap.CreditCardID = creditCardId;

            }
            else if(transactionDetails.PaymentMode == PaymentModeEnum.Bank)
            {
                throw new NotImplementedException();
                //Guid bankId = Guid.Empty;
            }

            else
            {
                ModelState.AddModelError("", "Invalid Payment Mode");
                return StatusCode(422, ModelState);
            }

            if (!_transactiontRepository.CreateTransaction(transactionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");

            
            







        }


    }
}
