namespace SMS.Services.Contracts
{
    public interface IUserService
    {
        (bool registered, string error) Register(string username, string email, string password);

    }
}
