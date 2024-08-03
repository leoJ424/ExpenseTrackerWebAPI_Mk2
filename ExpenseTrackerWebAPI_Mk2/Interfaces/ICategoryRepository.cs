using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();
        bool CategoryExists (Guid categoryId);
        string GetCategoryName(Guid categoryId);
    }
}
