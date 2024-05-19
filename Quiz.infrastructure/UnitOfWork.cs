using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Quiz.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Infrastructure.Data.Repositories;

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

        //public ICustomerRepository Customer => new CustomerRepository(_entityContext);
     

    }
}