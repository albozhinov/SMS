using System.Collections.Generic;
using SMS.ViewModels;


namespace SMS.Contracts
{
    public interface ICartService
    {
        void BuyProducts(string userId);

        IEnumerable<CartViewModel> GetProducts(string userId);
    }
}
