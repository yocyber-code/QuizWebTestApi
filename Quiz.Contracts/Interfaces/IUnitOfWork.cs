using Microsoft.EntityFrameworkCore.Storage;
using Quiz.Contracts.Interfaces.repo;


namespace Quiz.Contracts.Interfaces
{
    public interface IUnitOfWork
    {
        IQ_UserRepository Q_UserRepository { get; }
        IQ_UserGroupRepository Q_UserGroupRepository { get; }
        IQ_QuestionRepository Q_QuestionRepository { get; }
        IQ_ChoiceRepository Q_ChoiceRepository { get; }
        IQ_SaveRepository Q_SaveRepository { get; }

        Task CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IExecutionStrategy CreateExecutionStrategy();
    }
}
