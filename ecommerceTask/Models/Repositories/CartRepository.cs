using ecommerceTask.Data;
using ecommerceTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceTask.Models.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private readonly AppDbContext dbContext;

        public CartRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Create(Cart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Received a null Cart entity.");
            }

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            dbContext.Carts.Add(entity);
            dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var cart = dbContext.Carts
                .Include(c => c.CartItems)  
                .FirstOrDefault(x => x.Id == id);

            if (cart == null)
            {
                Console.WriteLine($"No cart found with Id({id}).");
                return false;
            }

            dbContext.CartItems.RemoveRange(cart.CartItems);
            dbContext.Carts.Remove(cart);
            dbContext.SaveChanges();
            return true;
        }

        public List<Cart> GetAll()
        {
            try
            {
                return dbContext.Carts
                    .Include(c => c.User)
                    .Include(c => c.CartItems) 
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching carts: {e.Message}");
                throw;
            }
        }

        public Cart GetById(int id)
        {
            var cart = dbContext.Carts
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .FirstOrDefault(x => x.Id == id);

            if (cart == null)
            {
                Console.WriteLine($"No cart found with Id({id}).");
                throw new InvalidOperationException($"No cart with Id({id}) found.");
            }

            return cart;
        }

        public void Update(int id, Cart updatedCart)
        {
            if (updatedCart == null)
            {
                throw new ArgumentNullException(nameof(updatedCart), "Received a null Cart entity to update.");
            }

            var existingCart = GetById(id);

            if (existingCart != null)
            {
                existingCart.Status = updatedCart.Status;
                existingCart.CreatedBy = updatedCart.CreatedBy;
                existingCart.UpdatedAt = DateTime.Now;

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No cart found with Id({id}) to update.");
            }
        }

        public Cart GetCardByUserId(int userId)
        {
            var cart = dbContext.Carts
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.Id == userId && c.Status == CartStatus.Active);

            if (cart == null)
            {
                Console.WriteLine($"No active cart found for UserId({userId}).");
                return null;
            }

            return cart;
        }
    }
}
