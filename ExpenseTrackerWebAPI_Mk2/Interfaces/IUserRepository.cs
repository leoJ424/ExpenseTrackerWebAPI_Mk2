using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUsers();
        Guid GetUserId(string userName);
        bool CreateUser(User user);
        bool Save();
    }
}
