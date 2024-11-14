
using ecommerceTask.Data;
using Microsoft.EntityFrameworkCore;

namespace ecommerceTask.Models.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext dbContext;
        public UserRepository( AppDbContext _dbContext) => dbContext = _dbContext;


        public void Create(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "received a null rser entity.");
            }


            entity.Avatar = entity.Avatar ?? "default_image_url";
            entity.Locale = Locale.En;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.LastLogin = DateTime.Now;
            entity.Company = entity.Company ?? "Default Company";

            dbContext.Users.Add(entity);
            dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(x => x.UserId == id);

                if (user == null)
                {
                    throw new Exception($"there is not any user with this Id({id})");
                }

                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                return dbContext.Users.AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


        public User GetById(int id)
        {
            try
            {
                User user = dbContext.Users.FirstOrDefault(x => x.UserId == id);
                if(user == null)
                {
                    throw new Exception($"there is no user with this Id({id})");
                }
                return user;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void Update(int id, User updatedUser)
        {
            var existingUser = GetById(id);

            if (existingUser != null)
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Email = updatedUser.Email;
                existingUser.Phone = updatedUser.Phone;
                existingUser.Role = updatedUser.Role;
                existingUser.Bio = updatedUser.Bio;
                existingUser.Avatar = updatedUser.Avatar ?? existingUser.Avatar;
                existingUser.Locale = updatedUser.Locale;
                existingUser.UpdatedAt = DateTime.Now;
                existingUser.LastLogin = DateTime.Now;
                existingUser.Company = updatedUser.Company ?? existingUser.Company;

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"there is not user found with Id({id}) to update.");
            }
        }
    }
}
