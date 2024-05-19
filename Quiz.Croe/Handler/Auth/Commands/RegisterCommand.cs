using Quiz.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts.DTOs.Request.Auth;
using Quiz.Contracts.Interfaces;


namespace Quiz.Core.Handler.Auth.Commands
{
    public class RegisterCommand : RegisterDTO, IRequest<Q_USER?>
    {
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Q_USER?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _datetime;

        public RegisterCommandHandler(IUnitOfWork unitOfWork,IDateTime datetime)
        {
            _unitOfWork = unitOfWork;
            _datetime = datetime;
        }

        public async Task<Q_USER?> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var groupIsExsit = await _unitOfWork.Q_UserGroupRepository.GetQueryable().Where(x => x.ID == request.Group_Id).AnyAsync();
            if (!groupIsExsit)
            {
                return new Q_USER()
                {
                    USERNAME = string.Empty,
                    USERGROUP = 0,
                };
            }

            var userIsExsit = await _unitOfWork.Q_UserRepository.GetQueryable().Where(x => x.USERNAME == request.Username).AnyAsync();
            if (userIsExsit)
            {
                return null;
            }

            var user = new Q_USER()
            {
                USERNAME = request.Username,
                USERGROUP = request.Group_Id,
                UPDATE_DATE = _datetime.Now,
                CREATE_DATE = _datetime.Now
            };
            _unitOfWork.Q_UserRepository.InsertOrUpdate(user);
            await _unitOfWork.CommitAsync();
            return user;
        }
    }
}
