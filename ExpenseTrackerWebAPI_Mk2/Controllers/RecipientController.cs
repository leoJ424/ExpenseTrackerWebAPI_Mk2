using AutoMapper;
using ExpenseTrackerWebAPI_Mk2.Dto;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerWebAPI_Mk2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipientController : Controller
    {
        private readonly IRecipientRepository _recipientRepository;
        private readonly IMapper _mapper;

        public RecipientController(IRecipientRepository recipientRepository, IMapper mapper)
        {
            _mapper = mapper;
            _recipientRepository = recipientRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipient>))]
        public IActionResult GetAllRecipients()
        {
            var recipients = _mapper.Map<List<RecipientDto>>(_recipientRepository.GetAllRecipients());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipients);
        }

        [HttpGet("id/{recipientId}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipientName(Guid recipientId)
        {
            if(!_recipientRepository.RecipientExists(recipientId))
            {
                return NotFound();
            }

            var recipientName = _recipientRepository.GetRecipientName(recipientId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(recipientName);
        }

        [HttpGet("recipientname/{recipientName}")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipientId(string recipientName)
        {
            if (!_recipientRepository.RecipientExists(recipientName))
            {
                return NotFound();
            }

            var recipientId = _recipientRepository.GetRecipientId(recipientName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipientId);
        }

    }
}
