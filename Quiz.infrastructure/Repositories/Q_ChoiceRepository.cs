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
    public class Q_ChoiceRepository : Repository<EntitiesContext, Q_CHOICE>, IQ_ChoiceRepository
    {
        public Q_ChoiceRepository(EntitiesContext context) : base(context) { }
    }
}
