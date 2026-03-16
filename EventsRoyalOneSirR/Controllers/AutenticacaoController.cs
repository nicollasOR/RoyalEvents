using EventsRoyalOneSirR.Applications.DTOs;
using EventsRoyalOneSirR.Applications.DTOs.AutenticacaoDTO;
using EventsRoyalOneSirR.Applications.Services;
using EventsRoyalOneSirR.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsRoyalOneSirR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {

        private readonly AutenticacaoService _service;
        
        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }


        [HttpPost("login")]
        //colocar um método HTTP que ele confere se o usuario é Administrador
        public ActionResult<TokenDTO> Login(LoginDTO _loginDto)
        {
            try
            {
                var token = _service.Login(_loginDto);
                return StatusCode(200, token);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);

            }
        }


    }
}
