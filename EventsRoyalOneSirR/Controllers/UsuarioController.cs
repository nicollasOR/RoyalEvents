using EventsRoyalOneSirR.Applications.DTOs.UsuarioDTO;
using EventsRoyalOneSirR.Applications.Services;
using EventsRoyalOneSirR.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsRoyalOneSirR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioService _service;
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerUsuarioDTO>> Listar()
        {
            List<LerUsuarioDTO> usuarios = _service.Listar();
            if (usuarios == null)
                return NotFound(usuarios);

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<LerUsuarioDTO> ObterPorId(int id)
        {
            try
            {
                LerUsuarioDTO lerDTO = _service.ObterPorId(id);
                return Ok(lerDTO);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }


        }


        [HttpGet("email/{email}")]
        public ActionResult<LerUsuarioDTO> ObterPorEmail(string email)
        {
            try
            {
                LerUsuarioDTO lerDTO = _service.ObterPorEmail(email);
                return Ok(lerDTO);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<LerUsuarioDTO> Adicionar(CriarUsuarioDTO criarDTO)
        {
            try
            {
                LerUsuarioDTO novoUsuarioDTO = _service.Adicionar(criarDTO);
                return StatusCode(201, criarDTO);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<LerUsuarioDTO> Atualizar(int id, CriarUsuarioDTO criarDTO)
        {
            try
            {
                LerUsuarioDTO lerDTO = _service.Atualizar(id, criarDTO);
                return Ok(lerDTO);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public ActionResult<LerUsuarioDTO> Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
