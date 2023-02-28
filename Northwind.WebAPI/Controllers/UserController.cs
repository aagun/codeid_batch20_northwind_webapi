using Microsoft.AspNetCore.Mvc;
using Northwind.Contracts.Dto.AuthenticationWebAPI;
using Northwind.Contracts.Models;
using Northwind.Domain.Base;
using Northwind.Domain.Entities;
using Northwind.Services.Abstraction;
using Northwind.WebAPI.Authentication;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IAuthenticationManager _authManager;

        public UserController(IRepositoryManager repositoryManager, ILoggerManager logger, IAuthenticationManager authManager)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _authManager = authManager;
        }


        // GET: api/<UserController>
        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            if (!await _authManager.ValidateUser(userForAuthenticationDto))
            {
                _logger.LogWarning($"{nameof(Authentication)} : Authentication Failed. Wrong username of password");
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken() });
        }

    }
}
