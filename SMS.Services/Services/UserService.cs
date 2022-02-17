namespace SMS.Services.Services
{
    using SMS.Data.Common;
    using SMS.Data.Models;
    using SMS.Services.Contracts;
    using SMS.Services.Validator;
    using System.Security.Cryptography;
    using System.Text;

    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository repository)
        {
            repo = repository;
        }

        public (bool registered, string error) Register(string username, string email, string password)
        {
            bool isRegistered = false;
            string error = string.Empty;

            var existsUser = repo.All<User>().FirstOrDefault(u => u.Username == username || u.Email == email);

            if (existsUser != null)
                return (isRegistered, error = "This user or email already exists");
             
            var cart = new Cart();           

            var user = new User()
            {
                Username = username,
                Email = email,
                Password = CalculateHash(password),
                Cart = cart,
                CartId = cart.Id,
            };

            (isRegistered, error) = ValidationService.ValidateModel(user);

            if (!isRegistered)
            {
                return (isRegistered, error);
            }

            cart.User = user;            

            try
            {
                repo.Add(user);
                repo.SaveAsync();
                isRegistered = true;
            }
            catch (Exception)
            {
                error = "Could not save user in DB";
            }

            return (isRegistered, error);
        }


        private string CalculateHash(string password)
        {
            byte[] passworArray = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(passworArray));
            }
        }
    }
}
