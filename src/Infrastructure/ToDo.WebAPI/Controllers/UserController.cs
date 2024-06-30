using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Application.Users.GetUserById;
using ToDo.Application.Users.LoginUser;
using ToDo.Application.Users.RegisterUser;
using ToDo.Contracts.Users.Request;
using ToDo.Contracts.Users.Response;
using ToDo.WebAPI.Tools;

namespace ToDo.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, IMapper mapper, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Register", Name = "RegisterUser")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registering new user...");

            try
            {
                var command = _mapper.Map<RegisterUserCommand>(request);
                var userId = await _mediator.Send(command, cancellationToken);
                var token = JwtTokenGenerator.GenerateToken(userId.ToString(), TimeSpan.FromHours(1));
                var response = new LoginUserResponse(userId, token);

                _logger.LogInformation("User registered successfully.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Login", Name = "LoginUser")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User login attempt...");

            try
            {
                var query = _mapper.Map<LoginUserQuery>(request);
                var userId = await _mediator.Send(query);
                var userQuery = new GetUserByIdQuery(userId);
                var user = await _mediator.Send(userQuery, cancellationToken);

                var token = JwtTokenGenerator.GenerateToken(userId.ToString(), TimeSpan.FromHours(1));
                var response = new LoginUserResponse(userId, token);

                _logger.LogInformation("User logged in successfully.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in user.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
