using EventsRoyalOneSirR.Applications.DTOs.EventoDTO;
using EventsRoyalOneSirR.Applications.DTOs.UsuarioDTO;
using EventsRoyalOneSirR.Applications.Services;
using EventsRoyalOneSirR.Exceptions;
using EventsRoyalOneSirR.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsRoyalOneSirR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly EventoService _service;

        public EventoController(EventoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerEventoDTO>> Listar()
        {
            List<LerEventoDTO> eventoDto = _service.Listar();
            if (eventoDto == null)
                return NotFound(eventoDto);


            return Ok(eventoDto);
        }

        [HttpGet("{id}")]
        public ActionResult<LerEventoDTO> ObterPorId(int id)
        {
            try
            {
                LerEventoDTO eventoDto = _service.ObterPorId(id);
                return Ok(eventoDto);
            }

            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("nome/{nome}")]
        public ActionResult<LerEventoDTO> ObterPorNome(string nome)
        {
            try
            {
                LerEventoDTO eventoDto = _service.ObterPorNome(nome);
                return Ok(eventoDto);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<LerEventoDTO> Adicionar(CriarEventoDTO criarDto)
            {
                try
                {
                    LerEventoDTO lerUsuarioDto = _service.Adicionar(criarDto);
                Console.WriteLine("Foi criado");
                    return StatusCode(201, criarDto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        [HttpPut]
        public ActionResult<LerEventoDTO> Atualizar(int id, CriarEventoDTO criarDTO)
        {
            try
            {
                LerEventoDTO lerDTO = _service.Atualizar(id, criarDTO);
                return Ok(lerDTO);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{id}")]
        public ActionResult<LerEventoDTO> Remover(int id)
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

