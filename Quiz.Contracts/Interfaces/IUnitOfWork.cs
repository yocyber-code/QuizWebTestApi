using Microsoft.EntityFrameworkCore.Storage;
using Quiz.Contracts.Interfaces.repo;


namespace Quiz.Contracts.Interfaces
{
    public interface IUnitOfWork
    {
        IQ_UserRepository Q_UserRepository { get; }
        IQ_UserGroupRepository Q_UserGroupRepository { get; }
        Task CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IExecutionStrategy CreateExecutionStrategy();
    }
}
