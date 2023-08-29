using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDemo.Core.Data.DTO;
using MyDemo.Core.Repositories;
using MyDemo.Core.Services;

namespace MyDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserRepository userRepository,
                            JwtService jwtService,
                            IConfiguration configuration,
                            ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userRepository.GetItemWithConditionAsync(
                    u => u.Name == loginRequest.Username && u.Password == loginRequest.Password);

                if (user == null)
                {
                    return Unauthorized(new LoginToken()
                    {
                        access_token = null,
                        user_id = null,
                        expires_in = 0,
                        token_type = "Bearer"
                    });
                }

                var secToken = _jwtService.GetToken(user);
                var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);

                var expires_sec = Convert.ToInt32(_configuration["JwtSettings:ExpirationTimeInMinutes"]) * 60;

                return Ok(new LoginToken()
                {
                    access_token = jwt,
                    user_id = user.Id.ToString(),
                    expires_in = expires_sec,
                    token_type = "Bearer"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login Error");
                return StatusCode(500);
            }
        }
    }
}