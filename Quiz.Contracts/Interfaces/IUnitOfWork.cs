using Microsoft.EntityFrameworkCore.Storage;


namespace Quiz.Contracts.Interfaces
{
    public interface IUnitOfWork
    {
        

        Task CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IExecutionStrategy CreateExecutionStrategy();
    }
}
