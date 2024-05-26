
using Microsoft.EntityFrameworkCore.Storage;
using productivity_hub_api.Models;

namespace productivity_hub_api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    _transaction.Dispose();
                }
            }
            catch (Exception)
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    _transaction.Dispose();
                }
            }
            catch (Exception)
            {
                await RollbackAsync();
                throw;
            }        
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
