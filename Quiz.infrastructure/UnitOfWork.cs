using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Quiz.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Infrastructure.Data.Repositories;
using Quiz.Contracts.Interfaces.repo;
using Quiz.infrastructure.Repositories;

namespace Quiz.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntitiesContext _entityContext;
        public UnitOfWork(EntitiesContext entityContext)
        {
            _entityContext = entityContext;
        }

        public async Task CommitAsync()
        {
            await _entityContext.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _entityContext.Database.BeginTransactionAsync();
        }
        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _entityContext.Database.CreateExecutionStrategy();
        }

        public IQ_UserRepository Q_UserRepository => new Q_UserRepository(_entityContext);
        public IQ_UserGroupRepository Q_UserGroupRepository => new Q_UserGroupRepository(_entityContext);
        public IQ_QuestionRepository Q_QuestionRepository => new Q_QuestionRepository(_entityContext);
        public IQ_ChoiceRepository Q_ChoiceRepository => new Q_ChoiceRepository(_entityContext);
        public IQ_SaveRepository Q_SaveRepository => new Q_SaveRepository(_entityContext);
    }
}