using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Contracts.DTOs;
using Quiz.Core.Handler.Quiz.Commands;
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

        [HttpGet("Quiz")]
        public async Task<IActionResult> GetQuiz([FromQuery] GetQuestionsQuery query)
        {
            var result = await _mediator.Send(query);
            if(result.Count == 0)
            {
                return BadRequest(new ApiGenericResult<dynamic>() { Code = 400, Message = "User not found", MessageAlt = "ไม่พบผู้ใช้" });
            }
            return Ok(new ApiGenericResult<dynamic>() { Code = 200, Count = result.Count, Message = "Successful", MessageAlt = "สำเร็จ", Results = result });
        }
        
        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] SaveCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result)
            {
                return BadRequest(new ApiGenericResult<dynamic>() { Code = 400, Message = "Save unsuccessful", MessageAlt = "บันทึกไม่สำเร็จ" });
            }
            return Ok(new ApiGenericResult<dynamic>() { Code = 200, Message = "Successful", MessageAlt = "สำเร็จ", Results = result });
        }
        
        [HttpGet("Load")]
        public async Task<IActionResult> Save([FromQuery] LoadQuery query)
        {
            var result = await _mediator.Send(query);
            if(result == null)
            {
                return BadRequest(new ApiGenericResult<dynamic>() { Code = 400, Message = "User not found", MessageAlt = "ไม่พบผู้ใช้" });
            }
            return Ok(new ApiGenericResult<dynamic>() { Code = 200, Message = "Successful", MessageAlt = "สำเร็จ", Results = result });
        }
        
        
    }
}
