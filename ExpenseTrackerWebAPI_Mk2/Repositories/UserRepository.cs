using ExpenseTrackerWebAPI_Mk2.Data;
using ExpenseTrackerWebAPI_Mk2.Interfaces;
using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users.Where(u => u.Status == true).OrderBy(u => u.LastName).ToList();
        }

        //public Guid GetUserId(string userName)
        //{
        //    return _context.Users.Where(u => u.UserName == userName && u.Status == true).Select(u => u.UserID).FirstOrDefault();
        //}

        public User GetUserById(Guid userId)
        {
            return _context.Users.Where(u => u.UserID == userId && u.Status == true).FirstOrDefault();
        }

        public bool UserIdExists(Guid userId)
        {
            return _context.Users.Any(u => u.UserID == userId && u.Status == true);
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
