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

        //Adding this so that when the user wants to create a transaction only data sent is the recipient name and nothing else(i.e status codes and Guids). Also easier to implement in flutter this way...lol.
        [HttpGet("recipientNames")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipientNames()
        {
            var recipientNames = _recipientRepository.GetAvailableRecipientNames();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipientNames);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipient([FromBody] RecipientDto recipientCreate)
        {
            if(recipientCreate == null)
            {
                return BadRequest(ModelState);
            }

            var recipient = _recipientRepository.GetAllRecipients()
                                                .Where(r => r.RecipientName.Trim().ToUpper() == recipientCreate.RecipientName.Trim().ToUpper())
                                                .FirstOrDefault();
            if(recipient != null)
            {
                ModelState.AddModelError("", "Recipient already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipientMap = _mapper.Map<Recipient>(recipientCreate);
            recipientMap.Status = true;//Set true by default. Otherwise false is set as default when object is created.

            if(!_recipientRepository.CreateRecipient(recipientMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

    }
}
