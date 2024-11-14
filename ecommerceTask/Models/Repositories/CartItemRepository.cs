using ecommerceTask.Data;
using ecommerceTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceTask.Models.Repositories
{
    public class CartItemRepository : IRepository<CartItem>
    {
        private readonly AppDbContext dbContext;

        public CartItemRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Create(CartItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Received a null CartItem entity.");
            }

            entity.CreatedAt = DateTime.Now;

            dbContext.CartItems.Add(entity);
            dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var cartItem = dbContext.CartItems
                .FirstOrDefault(x => x.Id == id);

            if (cartItem == null)
            {
                Console.WriteLine($"No cart item found with Id({id}).");
                return false;
            }

            dbContext.CartItems.Remove(cartItem);
            dbContext.SaveChanges();
            return true;
        }

        public List<CartItem> GetAll()
        {
            try
            {
                return dbContext.CartItems
                    .Include(c => c.Cart)
                    .Include(c => c.Product)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching cart items: {e.Message}");
                throw;
            }
        }

        public CartItem GetById(int id)
        {
            var cartItem = dbContext.CartItems
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefault(x => x.Id == id);

            if (cartItem == null)
            {
                Console.WriteLine($"No cart item found with Id({id}).");
                throw new InvalidOperationException($"No cart item with Id({id}) found.");
            }

            return cartItem;
        }

        public void Update(int id, CartItem updatedCartItem)
        {
            if (updatedCartItem == null)
            {
                throw new ArgumentNullException(nameof(updatedCartItem), "Received a null CartItem entity to update.");
            }

            var existingCartItem = GetById(id);

            if (existingCartItem != null)
            {
                existingCartItem.CartId = updatedCartItem.CartId;
                existingCartItem.ProductId = updatedCartItem.ProductId;
                existingCartItem.Price = updatedCartItem.Price;
                existingCartItem.Quantity = updatedCartItem.Quantity;
                existingCartItem.CreatedAt = updatedCartItem.CreatedAt;

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No cart item found with Id({id}) to update.");
            }
        }
    }
}
