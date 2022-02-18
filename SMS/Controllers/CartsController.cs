namespace SMS.Controllers
{
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using SMS.Contracts;

    public class CartsController : Controller
    {
        private readonly ICartService cartService;


        public CartsController(Request request, ICartService _cartService)
            : base(request)
        {
            cartService = _cartService;
        }

        public Response Details()
        {
            var userProducts = cartService.GetProducts(User.Id);

            return View(userProducts);
        }

        public Response Buy()
        {
            cartService.BuyProducts(User.Id);

            return Redirect("/");
        }
    }
}
