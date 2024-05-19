using IdentityModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts.DTOs.Response;
using Quiz.Contracts.DTOs.Response.Auth;
using Quiz.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Handler.UserGroup.Quries
{
    public class GetUserGroupQuery : IRequest<List<DropDownListDTO>>
    {
    }

    public class GetUserGroupCommandHandler : IRequestHandler<GetUserGroupQuery, List<DropDownListDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DropDownListDTO>> Handle(GetUserGroupQuery request, CancellationToken cancellationToken)
        {
            var userGroup = await _unitOfWork.Q_UserGroupRepository.GetQueryable().ToListAsync();
            var dropdownList = userGroup.Select(x => new DropDownListDTO()
            {
                id = x.ID.ToString(),
                name = x.GROUP_NAME
            }).ToList();

            return dropdownList;
        }
    }
}
