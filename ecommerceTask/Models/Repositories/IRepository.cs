using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace ecommerceTask.Models.Repositories
{
    public interface IRepository<T>
    {
        public void Create(T entity);
        public void Update(int Id,T entity);
        public bool Delete(int id);
        public T GetById(int id);
        public List<T> GetAll();

    }
}
