using Microsoft.EntityFrameworkCore;
using SMS.Contracts;
using SMS.Data.Common;
using SMS.Data.DBModels;
using SMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository repo;

        public CartService(IRepository repository)
        {
            repo = repository;
        }

        public void BuyProducts(string userId)
        {
            var currentUser = repo.All<User>()
                                    .Where(u => u.Id == userId)
                                    .Include(u => u.Cart)
                                    .ThenInclude(c => c.Products)
                                    .FirstOrDefault();

            currentUser.Cart.Products.Clear();

            repo.SaveChanges();
        }

        public IEnumerable<CartViewModel> GetProducts(string userId)
        {
            var user = repo.All<User>()
                            .Where(u => u.Id == userId)
                            .Include(u => u.Cart)
                            .ThenInclude(c => c.Products)
                            .FirstOrDefault();

            return user.Cart.Products
                            .Select(p => new CartViewModel()
                            {
                                ProductName = p.Name,
                                ProductPrice = p.Price.ToString("F2")
                            });
        }

    }
}
