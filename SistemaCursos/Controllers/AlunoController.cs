using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCursos.Applications.Services;
using SistemaCursos.DTOs.AlunoDTO;
using SistemaCursos.Exceptions;

namespace SistemaCursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _service;

        public AlunoController(AlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerAlunoDto>> Listar()
        {
            List<LerAlunoDto> alunos = _service.Listar();
            return Ok(alunos);
        }

        [HttpGet("(id)")]
        public ActionResult<LerAlunoDto> ObterPorId(int id)
        {
            try
            {
                LerAlunoDto alunos = _service.ObterPorId(id);
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public ActionResult<LerAlunoDto> ObterPorEmail(string email)
        {
            try
            {
                LerAlunoDto instrutor = _service.ObterPorEmail(email);
                return Ok(instrutor);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<LerAlunoDto> Adicionar(CriarAlunoDto alunoDto)
        {
            try
            {
                LerAlunoDto alunoCriado = _service.Adicionar(alunoDto);
                return StatusCode(201, alunoCriado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("(id)")]
        public ActionResult<LerAlunoDto> Atualizar(int id, CriarAlunoDto alunoDto)
        {
            try
            {
                LerAlunoDto alunoAtualizado = _service.Atualizar(id, alunoDto);

                return StatusCode(200, alunoAtualizado);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("(id)")]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
