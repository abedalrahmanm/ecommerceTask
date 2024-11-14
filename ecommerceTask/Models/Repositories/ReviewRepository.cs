using ecommerceTask.Data;
using Microsoft.EntityFrameworkCore;


namespace ecommerceTask.Models.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly AppDbContext dbContext;

        public ReviewRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Create(Review entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Received a null Review entity.");
            }

            entity.CreatedAt = DateTime.Now;

            dbContext.Reviews.Add(entity);
            dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var review = dbContext.Reviews
                .FirstOrDefault(x => x.Id == id);

            if (review == null)
            {
                Console.WriteLine($"No review found with Id({id}).");
                return false;
            }

            dbContext.Reviews.Remove(review);
            dbContext.SaveChanges();
            return true;
        }

        public List<Review> GetAll()
        {
            try
            {
                return dbContext.Reviews
                    .Include(r => r.User)  
                    .Include(r => r.Product) 
                    .AsNoTracking() 
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching reviews: {e.Message}");
                throw;
            }
        }

        public Review GetById(int id)
        {
            var review = dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefault(x => x.Id == id);

            if (review == null)
            {
                Console.WriteLine($"No review found with Id({id}).");
                throw new InvalidOperationException($"No review with Id({id}) found.");
            }

            return review;
        }

        public void Update(int id, Review updatedReview)
        {
            if (updatedReview == null)
            {
                throw new ArgumentNullException(nameof(updatedReview), "Received a null Review entity to update.");
            }

            var existingReview = GetById(id);

            if (existingReview != null)
            {
                existingReview.Rating = updatedReview.Rating;
                existingReview.Comment = updatedReview.Comment;
                existingReview.CreatedAt = updatedReview.CreatedAt;

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"No review found with Id({id}) to update.");
            }
        }

        public List<Review> GetReviewsByProductId(int productId)
        {
            var reviews = dbContext.Reviews
                .Include(r => r.User)
                .Where(r => r.ProductId == productId)
                .ToList();

            if (!reviews.Any())
            {
                Console.WriteLine($"No reviews found for ProductId({productId}).");
                return null;
            }

            return reviews;
        }

       
    }
}
