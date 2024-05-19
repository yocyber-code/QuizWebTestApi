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
    public class Q_ScoreBoardRepository : Repository<EntitiesContext, Q_SCORE_BOARD>, IQ_ScoreBoardRepository
    {
        public Q_ScoreBoardRepository(EntitiesContext context) : base(context)
        {
        }
    }
}
