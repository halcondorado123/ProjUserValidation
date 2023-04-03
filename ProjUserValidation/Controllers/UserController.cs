using DataAccess.DataAccessUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Newtonsoft.Json;
using ProjUserValidation.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjUserValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUsersRepository _IUsersRepository;
        public IConfiguration _configuration;
        private readonly BearerSecurity _bearerSecurity;

        public UserController(IUsersRepository userRepository, IConfiguration configuration, BearerSecurity bearerSecurity)
        {
            _IUsersRepository = userRepository;
            _configuration = configuration;
            _bearerSecurity = bearerSecurity;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetUsers()
        {
            try
            {
                TokenME vToken = _bearerSecurity.VerificateUser(HttpContext.User.Identity as ClaimsIdentity);

                if (!vToken.success)
                {
                    return Ok(vToken);
                }

                if (vToken.success)
                {
                    return Ok(_IUsersRepository.GetUsers());
                }
                else
                {
                    vToken.result = "No está autorizado para ejecutar el método";
                    return Ok(vToken);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        [Authorize]
        public ActionResult GetUserById(int id)
        {
            try
            {
                TokenME vToken = _bearerSecurity.VerificateUser(HttpContext.User.Identity as ClaimsIdentity);

                if (!vToken.success)
                {
                    return Ok(vToken);
                }

                if (vToken.success)
                {
                    return Ok(_IUsersRepository.GetUserById(id));
                }
                else
                {
                    vToken.result = "No está autorizado para ejecutar el método";
                    return Ok(vToken);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult CreateUser([FromBody] UserInfoME user)
        {
            try
            {
                TokenME vToken = _bearerSecurity.VerificateUser(HttpContext.User.Identity as ClaimsIdentity);

                if (!vToken.success)
                {
                    return Ok(vToken);
                }

                if (vToken.success)
                {
                    return Ok(_IUsersRepository.CreateUser(user));
                }
                else
                {
                    vToken.result = "No está autorizado para ejecutar el método";
                    return Ok(vToken);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Authorize]
        public ActionResult ModifyUser([FromBody] UserInfoME user)
        {
            try
            {
                TokenME vToken = _bearerSecurity.VerificateUser(HttpContext.User.Identity as ClaimsIdentity);

                if (!vToken.success)
                {
                    return Ok(vToken);
                }

                if (vToken.success)
                {
                    return Ok(_IUsersRepository.ModifyUser(user));
                }
                else
                {
                    vToken.result = "No está autorizado para ejecutar el método";
                    return Ok(vToken);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                TokenME vToken = _bearerSecurity.VerificateUser(HttpContext.User.Identity as ClaimsIdentity);

                if (!vToken.success)
                {
                    return Ok(vToken);
                }

                if (vToken.success)
                {
                    return Ok(_IUsersRepository.DeleteUser(id));
                }
                else
                {
                    vToken.result = "No está autorizado para ejecutar el método";
                    return Ok(vToken);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("login")]
        public dynamic LoginUser([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<UserInfoME>(optData.ToString());

            List<UserInfoME> users = _IUsersRepository.GetUsers();

            UserInfoME user = users.Where(x => x.UserName == data.UserName && x.UserPassword == data.UserPassword).FirstOrDefault();
            
            if (user == null)
            {
                return new
                {
                    success = true,
                    message = "Credenciales inválidas", 
                    result = ""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<JwtME>();

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("userId", user.UsuId.ToString()),
                new Claim("userName", user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: singin
                );

            return new
            {
                success = true,
                message = "Ingreso exitoso",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

    }
}
