using MediatR;
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts.DTOs.Request.Quiz;
using Quiz.Contracts.Entities;
using Quiz.Contracts.Interfaces;

namespace Quiz.Core.Handler.Quiz.Commands
{
    public class SaveCommand : IRequest<bool>
    {
        public int id { get; set; }
        public string username { get; set; } = null!;
        public int group_id { get; set; }
        public List<LoadDTO> save { get; set; } = new List<LoadDTO>();
    }

    public class SaveCommandHandler : IRequestHandler<SaveCommand, bool>
    {

        private readonly IUnitOfWork _unitOfWork;
        public SaveCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SaveCommand request, CancellationToken cancellationToken)
        {
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

            foreach (var item in request.save)
            {
                var save = new Q_SAVE()
                {
                    USER_ID = user.ID,
                    QUESTION_ID = item.question_id,
                    CHOICE_ID = item.choice_id
                };
                _unitOfWork.Q_SaveRepository.InsertOrUpdate(save);
            }
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
