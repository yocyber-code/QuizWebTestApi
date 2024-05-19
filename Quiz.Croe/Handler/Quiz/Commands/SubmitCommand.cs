using MediatR;
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts.DTOs.Request.Quiz;
using Quiz.Contracts.Entities;
using Quiz.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Handler.Quiz.Commands
{
    public class SubmitCommand : IRequest<bool>
    {
        public int id { get; set; }
        public string username { get; set; } = null!;
        public int group_id { get; set; }
        public List<SaveDTO> save { get; set; } = new List<SaveDTO>();
    }

    public class SubmitCommandHandler : IRequestHandler<SubmitCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;

        public SubmitCommandHandler(IUnitOfWork unitOfWork, IDateTime dateTime)
        {
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        public async Task<bool> Handle(SubmitCommand request, CancellationToken cancellationToken)
        {
            if (request.save.Count < 5)
            {
                return false;
            }

            var user = _unitOfWork.Q_UserRepository.GetQueryable().Where(x => x.ID == request.id && x.USERNAME == request.username && x.USERGROUP == request.group_id).SingleOrDefault();
            if (user == null)
            {
                return false;
            }

            var oldSave = await _unitOfWork.Q_SaveRepository.GetQueryable().Where(x => x.USER_ID == user.ID).ToListAsync();
            foreach (var item in oldSave)
            {
                _unitOfWork.Q_SaveRepository.Delete(item);
            }
            await _unitOfWork.CommitAsync();

            int score = 0;

            foreach (var item in request.save)
            {
                var save = new Q_SAVE()
                {
                    USER_ID = user.ID,
                    QUESTION_ID = item.question_id,
                    CHOICE_ID = item.choice_id
                };
                _unitOfWork.Q_SaveRepository.InsertOrUpdate(save);

                int scoreCheck = await _unitOfWork.Q_ChoiceRepository.GetQueryable().Where(x => x.QUESTION_ID == save.QUESTION_ID && x.ID == save.CHOICE_ID).Select(x => x.SCORE).SingleOrDefaultAsync();
                if (scoreCheck != 0)
                {
                    score += scoreCheck;
                }
            }
            await _unitOfWork.CommitAsync();

            var userScore = new Q_SCORE_BOARD()
            {
                USER_ID = user.ID,
                SCORE = score,
                GROUP_ID = user.USERGROUP,
                CREATE_DATE = _dateTime.Now,
                UPDATE_DATE = _dateTime.Now
            };
            _unitOfWork.Q_ScoreBoardRepository.InsertOrUpdate(userScore);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
