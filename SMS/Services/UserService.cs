namespace SMS.Services
{
    using SMS.Contracts;
    using SMS.Data.Common;
    using SMS.Data.DBModels;
    using SMS.ViewModels;
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class UserService : IUserService
    {
        private readonly IRepository repo;

        private readonly IValidationService validationService;

        public UserService(
            IRepository _repo,
            IValidationService _validationService
            )
        {
            repo = _repo;
            validationService = _validationService;
        }

        public UserViewModel GetUsername(string userId)
        {
            return repo.All<User>()
                .Where(u => u.Id == userId)
                .Select(u => new UserViewModel
                {
                   UserId = u.Id,
                   Username = u.Username,
                   CartId = u.CartId
                })
                .FirstOrDefault();
        }

        public string Login(LoginViewModel model)
        {
            var user = repo.All<User>()
                .Where(u => u.Username == model.Username)
                .Where(u => u.Password == CalculateHash(model.Password))
                .SingleOrDefault();

            return user?.Id;
        }

        public (bool registered, string error) Register(RegisterViewModel model)
        {
            bool registered = false;
            string error = null;

            var isUserExists = repo.All<User>()
                                   .Where(u => u.Username == model.Username || 
                                   u.Email == model.Email)
                                   .FirstOrDefault();


            if (isUserExists != null)
            {
            registered = false;
            error = "User with this username or email already exist.";

                return (registered, error);
            }

            var (isValid, validationError) = validationService.ValidateModel(model);
            (isValid, validationError) = validationService.NullOrWhiteSpacesCheck(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }

            Cart cart = new Cart();

            User user = new User()
            {
                Email = model.Email,
                Password = CalculateHash(model.Password),
                Username = model.Username,
                Cart = cart,
                CartId = cart.Id
            };

            try
            {
                repo.Add(user);
                repo.SaveChanges();
                registered = true;
            }
            catch (Exception)
            {
                error = "Could not save user in DB";
            }

            return (registered, error);
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
