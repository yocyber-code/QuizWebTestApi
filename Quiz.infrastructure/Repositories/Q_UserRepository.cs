using Quiz.Contracts.Interfaces;
using Quiz.Infrastructure.Data.Repositories.Generic;
using Quiz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Contracts.Entities;
using Quiz.Contracts.Interfaces.repo;

namespace Quiz.infrastructure.Repositories
{
    public class Q_UserRepository : Repository<EntitiesContext, Q_USER> , IQ_UserRepository
    {
        public Q_UserRepository(EntitiesContext context) : base(context)
        {
        }
    }
}
