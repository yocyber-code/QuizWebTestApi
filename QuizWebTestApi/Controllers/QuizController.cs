using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Contracts.DTOs;
using Quiz.Core.Handler.Quiz.Quries;

namespace QuizWebTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuizController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Questions")]
        public async Task<IActionResult> GetQuestions([FromQuery] GetQuestionsQuery query)
        {
            var result = await _mediator.Send(query);
            if(result.Count == 0)
            {
                return BadRequest(new ApiGenericResult<dynamic>() { Code = 400, Message = "User not found", MessageAlt = "ไม่พบผู้ใช้" });
            }
            return Ok(new ApiGenericResult<dynamic>() { Code = 200, Count = result.Count, Message = "Successful", MessageAlt = "สำเร็จ", Results = result });
        }
    }
}
