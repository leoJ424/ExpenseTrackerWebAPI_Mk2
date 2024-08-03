using ExpenseTrackerWebAPI_Mk2.Data;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Category> GetAllCategories()
        {
            return _context.Categories.Where(c => c.Status == true).OrderBy(c => c.CategoryName).ToList();
        }

        public bool CategoryExists(Guid categoryId)
        {
            return _context.Categories.Any(c => c.CategoryID == categoryId && c.Status == true);
        }

        public string GetCategoryName(Guid categoryId)
        {
            return _context.Categories.Where(c => c.CategoryID == categoryId && c.Status == true).Select(c => c.CategoryName).FirstOrDefault();
        }
    }
}
