using AutoMapper;
using ExpenseTrackerWebAPI_Mk2.Dto;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;
using ExpenseTrackerWebAPI_Mk2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerWebAPI_Mk2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetAllCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

        [HttpGet("{categoryID}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryName(Guid categoryID)
        {
            if(!_categoryRepository.CategoryExists(categoryID))
            {
                return NotFound();
            }

            var categoryName = _categoryRepository.GetCategoryName(categoryID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categoryName);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var category = _categoryRepository.GetAllCategories()
                                               .Where(c => c.CategoryName.Trim().ToUpper() == categoryCreate.CategoryName.Trim().ToUpper())
                                               .FirstOrDefault();
            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);
            categoryMap.Status = true;//Set true by default. Otherwise false is set as default when object is created.

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }
    }
}
