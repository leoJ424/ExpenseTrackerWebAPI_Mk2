namespace ExpenseTrackerWebAPI_Mk2.Interfaces
{
    public interface IUserRepository
    {
        Guid GetUserId(string userName);
    }
}
