namespace SMS.Controllers
{
    using BasicWebServer.Server.Attributes;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using SMS.Services.Contracts;
    using SMS.ViewModels.User;
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

        [HttpGet]
        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
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

            (bool registered, string errors) = userService.Register(model.Username, model.Email, model.Password);

            if (errors != null)
                return View(new { ErrorMessage = errors }, "/Error");


            return Redirect("/Users/Login");
        }
    }
}
