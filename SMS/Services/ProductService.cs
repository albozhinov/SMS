namespace SMS.Services
{
    using SMS.Contracts;
    using SMS.Data.Common;
    using SMS.Data.DBModels;
    using SMS.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly IRepository repo;
        private readonly IValidationService validationService;

        public ProductService(
            IRepository _repo,
            IValidationService _validationService)
        {
            repo = _repo;
            validationService = _validationService;
        }

        public (bool added, string message) AddProductToCart(string productId, string cartId)
        {
            bool isAdded = true;
            string message = null;

            var product = repo.All<Product>()
                                    .FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                isAdded = false;
                message = "This product is not exist.";

                return (isAdded, message);
            }

            var cart = repo.All<Cart>()
                            .FirstOrDefault(c => c.Id == cartId);

            if (cart == null)
            {
                isAdded = false;
                message = "This cart is not exist.";

                return (isAdded, message);
            }

            cart.Products.Add(product);
            repo.SaveChanges();

            return (isAdded, message);
        }

        public (bool created, string error) Create(CreateViewModel model)
        {
            bool created = false;
            string error = null;

            var (isValid, validationError) = validationService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }

            decimal price = 0;

            if (!decimal.TryParse(model.Price, NumberStyles.Float, CultureInfo.InvariantCulture, out price)
                || price < 0.05M || price > 1000M)
            {
                return (false, "Price must be between 0.05 and 1000");
            }

            var product = new Product()
            {
                Name = model.Name,
                Price = price
            };

            try
            {
                repo.Add(product);
                repo.SaveChanges();
                created = true;
            }
            catch (Exception)
            {
                error = "Could not save product";
            }

            return (created, error);
        }

        public IEnumerable<ProductListViewModel> GetProducts()
        {
            return repo.All<Product>()
                .Select(p => new ProductListViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("F2"),
                    ProductId = p.Id
                })
                .ToList();
        }
        public ProductListViewModel GetProducts(string id)
        {
            return repo.All<Product>()
                .Where(p => p.Id == id)
                .Select(p => new ProductListViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("F2"),
                    ProductId = p.Id
                })
                .FirstOrDefault();
        }
    }
}
