using ClassInstitute.Application.DTO;
using ClassInstitute.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassInstitute.API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculaBusiness _matriculaBusiness;

        public MatriculasController(IMatriculaBusiness matriculaBusiness)
        {
            _matriculaBusiness = matriculaBusiness;
        }

        [HttpGet("GetAllMatriculas")]
        public IActionResult GetAllMatriculas()
        {
            var matriculas = _matriculaBusiness.GetAllMatriculas();

            return Ok(matriculas);
        }

        [HttpGet("GetMatriculaByCurso/{id}")]
        public IActionResult GetMatriculaByCurso(int id)
        {
            var matricula = _matriculaBusiness.GetMatriculaByCurso(id);

            if (matricula == null)
            {
                return NotFound("Matrícula não localizado");
            }

            return Ok(matricula);
        }

        [HttpPost("CreateNewMatricula")]
        public IActionResult CreateNewMatricula([FromBody] MatriculaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMatricula = _matriculaBusiness.NewMatricula(dto);

            if (newMatricula == null)
            {
                return BadRequest("Erro ao matricular aluno");
            }

            return Ok("Matrícula criada com sucesso!");
        }

        [HttpDelete("DeleteCurso")]
        public IActionResult DeleteCurso(int id)
        {
            var isDeleted = _matriculaBusiness.DeleteMatricula(id);

            if (!isDeleted)
            {
                return NotFound("Matrícula não localizada.");
            }

            return Ok("Matrícula excluído com sucesso!");
        }
    }
}
