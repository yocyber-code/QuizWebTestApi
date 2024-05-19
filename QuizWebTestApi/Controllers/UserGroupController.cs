using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Contracts.DTOs;
using Quiz.Core.Handler.UserGroup.Quries;

namespace QuizWebTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var query = new GetUserGroupQuery();
            var response = await _mediator.Send(query);
            return Ok(new ApiGenericResult<dynamic>() { Code = 200, Count = response.Count, Message = "Successful", MessageAlt = "สำเร็จ", Results = response });
        }
    }
}
