namespace SMS.Controllers
{
    using BasicWebServer.Server.Attributes;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using SMS.Contracts;
    using SMS.ViewModels;

    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(
            Request request,
            IProductService _productService)
            : base(request)
        {
            productService = _productService;
        }

        [Authorize]
        public Response Create()
        {
            return View(new { IsAuthenticated = true });
        }

        [HttpPost]
        [Authorize]
        public Response Create(CreateViewModel model)
        {
            var (created, error) = productService.Create(model);

            if (!created)
            {
                return View(new { ErrorMessage = error }, "/Error");
            }

            return Redirect("/");
        }

        [Authorize]
        [HttpGet]
        public Response Add(string productId, string cartId)
        {
            var product = productService.GetProducts(productId);

            var model = new 
            { 
                IsAuthenticated = true, 
                ProductName = product.ProductName, 
                ProductPrice = product.ProductPrice,
                CartId = cartId,
                ProductId = productId
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public Response AddProduct(string productId, string cartId)
        {
            
            (bool isAdded, string message) addedProduct = productService.AddProductToCart(productId, cartId);         


            return Redirect("/Carts/Details");
        }
    }
}
