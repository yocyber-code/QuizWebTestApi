using Quiz.Contracts.Entities;
using Quiz.Contracts.Interfaces.repo;
using Quiz.Infrastructure.Data.Repositories.Generic;
using Quiz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.infrastructure.Repositories
{
    public class Q_SaveRepository : Repository<EntitiesContext, Q_SAVE>, IQ_SaveRepository
    {
        public Q_SaveRepository(EntitiesContext context) : base(context)
        {
        }
    }
}
