using MediatR;
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts.DTOs.Request.Auth;
using Quiz.Contracts.DTOs.Response.Auth;
using Quiz.Contracts.Entities;
using Quiz.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Handler.Auth.Commands
{
    public class LoginCommand : IRequest<LoginDTO?>
    {
        public string Username { get; set; } = null!;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginDTO?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Q_UserRepository.GetQueryable().Where(x => x.USERNAME == request.Username).SingleOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            return new LoginDTO()
            {
                id = user.ID,
                username = user.USERNAME,
                group = user.USERGROUP,
            };
        }
    }
}
