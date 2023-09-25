using Microsoft.EntityFrameworkCore;

namespace APITask.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        TContext Context { get; }
        void Save();
    }
}
