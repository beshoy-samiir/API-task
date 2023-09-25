using APITask.Models;
using APITask.UnitOfWork;

namespace APITask.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork<ApplicationDbContext> unitofwork) : base(unitofwork)
        {

        }
    }
}
