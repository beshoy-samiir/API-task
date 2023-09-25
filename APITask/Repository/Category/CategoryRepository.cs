using APITask.Models;
using APITask.UnitOfWork;

namespace APITask.Repository
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork<ApplicationDbContext> unitofwork) : base(unitofwork)
        {

        }
    }
}
