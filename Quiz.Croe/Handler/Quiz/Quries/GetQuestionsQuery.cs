using MediatR;
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts.DTOs.Response.Quiz;
using Quiz.Contracts.Interfaces;

namespace Quiz.Core.Handler.Quiz.Quries
{
    public class GetQuestionsQuery : IRequest<List<QuestionListDTO>>
    {
        public string id { get; set; } = "0";
        public string username { get; set; } = "";
        public string group_id { get; set; } = "0";
    }

    public class GetQuestionsCommandHandler : IRequestHandler<GetQuestionsQuery, List<QuestionListDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetQuestionsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<QuestionListDTO>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            List<QuestionListDTO> result = new List<QuestionListDTO>();
            var user = _unitOfWork.Q_UserRepository.GetQueryable().Where(x => x.ID == int.Parse(request.id) && x.USERNAME == request.username && x.USERGROUP == int.Parse(request.group_id)).SingleOrDefault();
            if(user == null)
            {
                return result;
            }
            var questions = await _unitOfWork.Q_QuestionRepository.GetQueryable().Where(x => x.USERGROUP == user.USERGROUP).ToListAsync();
            
            foreach (var question in questions)
            {
                var choices = await _unitOfWork.Q_ChoiceRepository.GetQueryable().Where(x => x.QUESTION_ID == question.ID).ToListAsync();
                var questionDTO = new QuestionListDTO()
                {
                    id = question.ID,
                    question = question.QUESTION,
                    choices = choices.Select(x => new ChoiceDTO()
                    {
                        id = x.ID,
                        choice = x.CHOICE
                    }).ToList()
                };
                result.Add(questionDTO);
            }
            return result;
        }
    }
}
