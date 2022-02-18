using SMS.ViewModels;
using System.Collections.Generic;

namespace SMS.Contracts
{
    public interface IProductService
    {
        (bool created, string error) Create(CreateViewModel model);

        IEnumerable<ProductListViewModel> GetProducts();

        ProductListViewModel GetProducts(string id);

        (bool added, string message) AddProductToCart(string productId, string cartId);
    }
}
