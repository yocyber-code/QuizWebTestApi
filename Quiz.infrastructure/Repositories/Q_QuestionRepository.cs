using Quiz.Contracts.Entities;
using Quiz.Contracts.Interfaces.repo;
using Quiz.Infrastructure;
using Quiz.Infrastructure.Data.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.infrastructure.Repositories
{
    public class Q_QuestionRepository : Repository<EntitiesContext, Q_QUESTION>, IQ_QuestionRepository
    {
        public Q_QuestionRepository(EntitiesContext context) : base(context)
        {
        }
    }
}
