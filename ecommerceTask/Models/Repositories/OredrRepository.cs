using ecommerceTask.Data;
using ecommerceTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceTask.Models.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly AppDbContext dbContext;

        public OrderRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Create(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Received a null Order entity.");
            }

            entity.CreatedAt = DateTime.Now; 

            dbContext.Orders.Add(entity);
            dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var order = dbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                Console.WriteLine($"No order found with Id({id}).");
                return false;
            }

            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
            return true;
        }

        public List<Order> GetAll()
        {
            try
            {
                return dbContext.Orders
                    .Include(o => o.User)
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while fetching orders: {e.Message}");
                throw;
            }
        }

        public Order GetById(int id)
        {
            var order = dbContext.Orders
                .Include(o => o.User)
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
            {
                Console.WriteLine($"No order found with Id({id}).");
                throw new InvalidOperationException($"No order with Id({id}) found.");
            }

            return order;
        }

        public void Update(int id, Order updatedOrder)
        {
            if (updatedOrder == null)
            {
                throw new ArgumentNullException(nameof(updatedOrder), "Received a null Order entity to update.");
            }

            var existingOrder = GetById(id);
            if (existingOrder != null)
            {
                existingOrder.UserId = updatedOrder.UserId;
                existingOrder.CreatedAt = updatedOrder.CreatedAt;

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No order found with Id({id}) to update.");
            }
        }
    }
}
