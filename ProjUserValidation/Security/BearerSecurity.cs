using DataAccess.DataAccessUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjUserValidation.Security
{
    public class BearerSecurity
    {
        private readonly IUsersRepository _IUsersRepository;
        public IConfiguration _configuration;

        public BearerSecurity(IUsersRepository userRepository)
        {
            _IUsersRepository = userRepository;
        }

        public TokenME VerificateUser(ClaimsIdentity identity)
        {
            var uId = identity.Claims.FirstOrDefault(x => x.Type == "userId").Value;
            int idUser = (uId != null ? Convert.ToInt32(uId) : 0);
            UserInfoME user = _IUsersRepository.GetUserById(idUser);
            TokenME vToken = JwtME.ValidateToken(identity, user);
            return vToken;
        }


        public dynamic LoginUser(Object optData)
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
