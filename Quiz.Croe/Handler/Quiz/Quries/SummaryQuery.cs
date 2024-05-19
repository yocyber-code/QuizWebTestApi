using MediatR;
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts.DTOs.Response.Quiz;
using Quiz.Contracts.Interfaces;

namespace Quiz.Core.Handler.Quiz.Quries
{
    public class SummaryQuery : IRequest<SummaryDTO?>
    {
        public string id { get; set; } = "0";
        public string username { get; set; } = null!;
        public string group_id { get; set; } = "0";
    }

    public class SummaryQueryHandler : IRequestHandler<SummaryQuery, SummaryDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SummaryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SummaryDTO?> Handle(SummaryQuery request, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Q_UserRepository.GetQueryable().Where(x => x.ID == int.Parse(request.id) && x.USERNAME == request.username && x.USERGROUP == int.Parse(request.group_id)).SingleOrDefault();
            if (user == null)
            {
                return null;
            }
            var allScoreBoard = await _unitOfWork.Q_ScoreBoardRepository.GetQueryable().Where(x => x.GROUP_ID == user.USERGROUP).OrderBy(x => x.SCORE).Select(x => x.USER_ID).ToListAsync();
            var scoreBoard = await _unitOfWork.Q_ScoreBoardRepository.GetQueryable().Where(x => x.USER_ID == user.ID).SingleOrDefaultAsync();
            if(scoreBoard == null)
            {
                return new SummaryDTO() { username = user.USERNAME, score = 0, rank = 0 };
            }
            SummaryDTO result = new SummaryDTO() { username = user.USERNAME, score = scoreBoard.SCORE, rank = allScoreBoard.IndexOf(user.ID) + 1 };
            return result;
        }
    }
}
