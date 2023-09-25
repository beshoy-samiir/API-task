using Microsoft.EntityFrameworkCore;

namespace APITask.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>where TContext : DbContext, new()
    {
        private TContext _context;

        public TContext Context
        {
            get
            {
                return _context;
            }
        }
        public UnitOfWork(TContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
