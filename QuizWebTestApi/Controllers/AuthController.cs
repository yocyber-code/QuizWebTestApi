using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Contracts.DTOs;
using Quiz.Core.Handler.Auth.Commands;

namespace QuizWebTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == null)
            {
                return BadRequest(new ApiGenericResult<dynamic>() { Code = 409, Message = "This username is aleready used", MessageAlt = "ชื่อบัญชีนี้ถูกใช้งานไปแล้ว", Results = null });
            }
            if(response.USERGROUP == 0)
            {
                return BadRequest(new ApiGenericResult<dynamic>() { Code = 400, Message = "Group not found", MessageAlt = "ไม่พบกลุ่มผู้ใช้", Results = null });
            }
            return Ok(new ApiGenericResult<dynamic>() { Code = 200, Message = "Successful", MessageAlt = "ลงทะเบียนสำเร็จ", Results = response });
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == null)
            {
                return BadRequest(new ApiGenericResult<dynamic>() { Code = 400, Message = "Username not found", MessageAlt = "ไม่พบบัญชีผู้ใช้", Results = null });
            }
            return Ok(new ApiGenericResult<dynamic>() { Code = 200, Message = "Successful", MessageAlt = "เข้าสู่ระบบสำเร็จ", Results = response });
        }
    }
}
