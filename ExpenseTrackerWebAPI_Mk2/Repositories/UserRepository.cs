using ExpenseTrackerWebAPI_Mk2.Data;
using ExpenseTrackerWebAPI_Mk2.Interfaces;

namespace ExpenseTrackerWebAPI_Mk2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public Guid GetUserId(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName && u.Status == true).Select(u => u.UserID).FirstOrDefault();
        }

    }
}
