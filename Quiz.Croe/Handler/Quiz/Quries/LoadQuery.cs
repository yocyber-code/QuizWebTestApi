using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Contracts.DTOs.Response.Quiz;
using Quiz.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Quiz.Core.Handler.Quiz.Quries
{
    public class LoadQuery : IRequest<List<LoadDTO>?>
    {
        public string id { get; set; } = "0";
        public string username { get; set; } = null!;
        public string group_id { get; set; } = "0";
    }

    public class LoadQueryHandler : IRequestHandler<LoadQuery, List<LoadDTO>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoadQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LoadDTO>?> Handle(LoadQuery request, CancellationToken cancellationToken)
        {
            List<LoadDTO> result = new List<LoadDTO>();
            var user = _unitOfWork.Q_UserRepository.GetQueryable().Where(x => x.ID == int.Parse(request.id) && x.USERNAME == request.username && x.USERGROUP == int.Parse(request.group_id)).SingleOrDefault();
            if (user == null)
            {
                return null;
            }

            var saves = await _unitOfWork.Q_SaveRepository.GetQueryable().Where(x => x.USER_ID == user.ID).ToListAsync();

            return result;
        }
    }
}
