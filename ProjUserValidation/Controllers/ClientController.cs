using DataAccess.DataAccessClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjUserValidation.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjUserValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientController : Controller
    {

        private readonly IClientsRepository _IClientsRepository;
        private readonly BearerSecurity _bearerSecurity;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientController(IClientsRepository clientRepository, BearerSecurity bearerSecurity, IHttpContextAccessor httpContextAccessor)
        {
            _IClientsRepository = clientRepository;
            _bearerSecurity = bearerSecurity;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetClients()
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
                    return Ok(_IClientsRepository.GetClients());
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
        public ActionResult GetClientById(int id)
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
                    return Ok(_IClientsRepository.GetClientById(id));
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
        public ActionResult CreateClient([FromBody] ClientME client)
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
                    return Ok(_IClientsRepository.CreateClient(client));
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
        public ActionResult ModifyClient([FromBody] ClientME client)
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
                    return Ok(_IClientsRepository.ModifyClient(client));
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
        public ActionResult DeleteClient(int id)
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
                    return Ok(_IClientsRepository.DeleteClient(id));
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

    }
}
