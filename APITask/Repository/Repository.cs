using APITask.Models;
using APITask.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace APITask.Repository
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly ApplicationDbContext context;

        public string ServiceId { get; set; }

        public Repository(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            context = unitOfWork.Context;
        }
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Delete(int id)
        {
            context.Remove(Get(id));
        }
        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> GetAll(string includes = null)
        {
            return includes == null ?
                context.Set<T>().ToList() :
                context.Set<T>().Include(includes).ToList();
        }

        public void Insert(T obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.Update(obj);
        }
    }
}
