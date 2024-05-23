namespace productivity_hub_api.Repository
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();

        Task CommitAsync();

        Task RollbackAsync();

        Task SaveChangesAsync();
    }
}
