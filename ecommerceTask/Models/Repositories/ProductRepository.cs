using ecommerceTask.Data;
using Microsoft.EntityFrameworkCore;


namespace ecommerceTask.Models.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Received a null Product entity.");
            }

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            dbContext.Products.Add(entity);
            dbContext.SaveChanges();
        }

        public void Update(int id, Product entity)
        {
            var existingProduct = GetById(id);

            if (existingProduct != null)
            {
                existingProduct.Title = entity.Title;
                existingProduct.Picture = entity.Picture;
                existingProduct.Summary = entity.Summary;
                existingProduct.Description = entity.Description;
                existingProduct.Price = entity.Price;
                existingProduct.DiscountType = entity.DiscountType;
                existingProduct.DiscountValue = entity.DiscountValue;
                existingProduct.Tags = entity.Tags;
                existingProduct.UpdatedAt = DateTime.Now;

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"no product found with Id({id}) to update.");
            }
        }

        public bool Delete(int id)
        {
            var product = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine($"no product found with Id({id}) to delete.");
                return false;
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return true;
        }

        public Product GetById(int id)
        {
            return dbContext.Products
                            .Include(p => p.Category)
                            .Include(p => p.CartItems)
                            .Include(p => p.orderLines)
                            .Include(p => p.Reviews)
                            .FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetAll()
        {
            return dbContext.Products
                            .Include(p => p.Category)
                            .Include(p => p.CartItems)
                            .Include(p => p.orderLines)
                            .Include(p => p.Reviews)
                            .AsNoTracking()
                            .ToList();
        }
    }
}
