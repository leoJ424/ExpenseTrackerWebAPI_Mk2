using AutoMapper;
using ExpenseTrackerWebAPI_Mk2.Data;
using ExpenseTrackerWebAPI_Mk2.Dto;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ExpenseTrackerWebAPI_Mk2.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreditCardController(ICreditCardRepository creditCardRepository, IUserRepository userRepository, IMapper mapper)
        {
            _creditCardRepository = creditCardRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Guid>))]
        [ProducesResponseType(400)]
        public IActionResult GetCardIdsOfCurrentUser()
        {
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

            var result = _creditCardRepository.GetCardIdsOfUser(new Guid(claimUserId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("specificUser")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Guid>))]
        [ProducesResponseType(400)]
        public IActionResult GetCardIdsOfUser(Guid id)
        {

            var result = _creditCardRepository.GetCardIdsOfUser(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        
        [HttpGet("cardDetails/{type}")]
        [ProducesResponseType(200, Type = typeof(CreditCard))]
        [ProducesResponseType(400)]
        public IActionResult GetCardDetails(Guid cardId, int type)
        {
            #region Check if card belongs to user

            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            if(authHeader == null || !authHeader.StartsWith("Bearer "))
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

            if(claimUserId != _creditCardRepository.GetCardOwner(cardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

            CreditCardDto result = new CreditCardDto();
            if(!_creditCardRepository.CreditCardExists(cardId))
            {
                return NotFound();
            }

            if(type == 101)
            {
                result = _mapper.Map<CreditCardDto>(_creditCardRepository.GetCardDetails(cardId));
            }
            else if(type == 0)
            {
                result = _mapper.Map<CreditCardDto>(_creditCardRepository.GetMaskedCardDetails(cardId));
            }
            else
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpGet("cardName")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetCardName(Guid cardId)
        {
            #region Check if card belongs to user

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

            if (claimUserId != _creditCardRepository.GetCardOwner(cardId).ToString())
            {
                return Unauthorized("Card does not belong to user");
            }

            #endregion

            if (!_creditCardRepository.CreditCardExists(cardId))
            {
                return NotFound();
            }

            var result = _creditCardRepository.GetCardName(cardId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCreditCard([FromQuery] string userName,  [FromBody] CreditCardDto creditCardCreate)
        {
            if(creditCardCreate == null)
            {
                return BadRequest(ModelState);
            }
            
            Guid userId = _userRepository.GetUserId(userName);
            if(userId == Guid.Empty)
            {
                ModelState.AddModelError("", "User does not exist");
                return StatusCode(422, ModelState);
            }

            var existingCards = _creditCardRepository.GetCardIdsOfUser(userId);
            var newCardNumber = creditCardCreate.First4Digits + " " + creditCardCreate.Second4Digits + " " + creditCardCreate.Third4Digits + " " + creditCardCreate.Last4Digits;
            
            foreach (var cardId in existingCards)
            {
                var cardNumber = _creditCardRepository.GetCardNumber(cardId);
                if(cardNumber == newCardNumber)
                {
                    ModelState.AddModelError("", "Card already exists");
                    return StatusCode(422, ModelState);
                }

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creditCardMap = _mapper.Map<CreditCard>(creditCardCreate);
            creditCardMap.UserID = userId;
            creditCardMap.Status = true;

            if(!_creditCardRepository.CreateCreditCard(creditCardMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);

            }

            return Ok("Successfully Created");
        }
    }
}
