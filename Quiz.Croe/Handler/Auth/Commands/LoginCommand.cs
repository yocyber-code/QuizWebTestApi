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
        private readonly IJwt _jwt;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwt jwt)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt;
        }

        public async Task<LoginDTO?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Q_UserRepository.GetQueryable().Where(x => x.USERNAME == request.Username).SingleOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            string accessToken = await _jwt.CreateJWTExpireAsync(user.ID.ToString(), request.Username.ToString(), 2592000);
            return new LoginDTO()
            {
                Token = accessToken
            };
        }
    }
}
