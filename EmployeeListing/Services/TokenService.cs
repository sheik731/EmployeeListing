using EmployeeListing.Models;
using EmployeeListing.DataAccessLayer;
using EmployeeListing.validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Diagnostics;

namespace EmployeeListing.Services
{
    public class TokenService
    {
        private EmployeeDataAccessLayer _employeeDataAccessLayer;
        private ILogger<TokenService> _logger;
        private IConfiguration _configuration;
        
        private readonly Stopwatch _stopwatch = new Stopwatch();
        
        private bool IsTracingEnabled;
        public TokenService(ILogger<TokenService> logger, IConfiguration configuration, EmployeeDataAccessLayer employeeDataAccessLayer)
        {
            _logger = logger;
            _configuration = configuration;
            _employeeDataAccessLayer = employeeDataAccessLayer;// DataFactory.EmployeeDataFactory.GetEmployeeDataAccessLayerObject(logger);
        }

        public object AuthToken(string employeeMail, string password)
        {
            _stopwatch.Start();
            try
            {
                var user = _employeeDataAccessLayer.CheckLoginCrendentials(employeeMail, password);

                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email,user.MailId!),
                        new Claim("UserId", user.EmployeeId.ToString()),
                        new Claim("ClassId",user.EmpClass.ToString()),
                        new Claim("EmployeeName", user.EmployeeName!.ToString()),   
                        new Claim(ClaimTypes.Role,user.RoleId.ToString())
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var encryptingCredentials = new EncryptingCredentials(key, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);
                var token = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    new ClaimsIdentity(claims),
                    null,
                    expires: DateTime.UtcNow.AddHours(6),
                    null,
                    signingCredentials: signIn,
                    encryptingCredentials: encryptingCredentials
                    );


                var Result = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiryInMinutes = 360,
                    UserName = user.EmployeeName,
                    UserId = user.EmployeeId
                };

                return Result;

            }
            catch (ValidationException loginCredentialsNotValid)
            {
                _logger.LogError($"Employee DAL : CheckLoginCredentails throwed an exception : {loginCredentialsNotValid.Message}");
                throw loginCredentialsNotValid;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee DAL : CheckLoginCredentails throwed an exception : {exception.Message}");
                throw ;
            }
        }
    }
}