namespace SMS.Controllers
{
    using BasicWebServer.Server.Attributes;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using SMS.Services.Contracts;

    public class UsersController : Controller
    {
        private readonly IUserService userService; 

        public UsersController(Request request, IUserService _userService)
            : base(request)
        {
            userService = _userService;
        }

        [HttpGet]
        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false});
        }

        [HttpGet]
        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Register(RegisterUserViewModel model)
        {
            

            return View();
        }
    }
}
