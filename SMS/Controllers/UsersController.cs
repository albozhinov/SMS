namespace SMS.Controllers
{
    using BasicWebServer.Server.Attributes;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using SMS.Contracts;
    using SMS.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(Request request, IUserService _userService)
            : base(request)
        {
            userService = _userService;
        }
        
        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Login(LoginViewModel model)
        {
            Request.Session.Clear();
            string id = userService.Login(model);

            if (id == null)
            {
                return View(new { ErrorMessage = "Incorect Login" }, "/Error");
            }

            SignIn(id);

            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName,
                Request.Session.Id);

            return Redirect("/");
        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            var model = new
            {
                IsAuthenticated = User.IsAuthenticated,
            };

            return View(model);
        }

        [HttpPost]
        public Response Register(RegisterViewModel model)
        {
            var context = new ValidationContext(model);
            var errorResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, context, errorResult, true);
            if (!isValid)
            {
                var errorMessage = String.Join(", ", errorResult.Select(e => e.ErrorMessage));

                return View(new { ErrorMessage = errorMessage }, "/Error");
            }

            (bool registered, string errors) = userService.Register(model);

            if (errors != null)
                return View(new { ErrorMessage = errors }, "/Error");

            return Redirect("/Users/Login");
        }

        [Authorize]
        public Response Logout()
        {
            SignOut();

            return Redirect("/");
        }
    }
}
