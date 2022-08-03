using Microsoft.AspNetCore.Mvc;
using EmployeeListing.Services;
using EmployeeListing.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeListing.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private TokenService _tokenService;
        public TokenController(TokenService tokenService, ILogger<TokenController> logger)
        {
            _logger = logger;
            _tokenService = tokenService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>


        [HttpPost]
        public IActionResult Login(User user)
        {
            if (user.MailId == null && user.MailId == null)
                return BadRequest("Mail Id and Password cannot be null");
            try
            {
                var Result = _tokenService.AuthToken(user.MailId, user.Password!);
                return Ok(Result);
            }
            catch (ValidationException validationException)
            {
                _logger.LogError($"Token Service : AuthToken() : {validationException.Message}");
                return BadRequest(validationException.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Token Service : AuthToken() : {exception.Message}");
                return Problem("Sorry some internal error occured");
            }
        }
    }
}