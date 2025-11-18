using ClassInstitute.Application.DTO;
using ClassInstitute.Application.Interfaces;
using ClassInstitute.Service.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassInstitute.API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursoBusiness _cursoBusiness;

        public CursosController(ICursoBusiness cursoBusiness)
        {
            _cursoBusiness = cursoBusiness;
        }

        [HttpGet("GetAllCursos")]
        public IActionResult GetAllCursos()
        {
            var cursos = _cursoBusiness.GetAllCursos();

            return Ok(cursos);
        }

        [HttpGet("GetCursoById/{id}")]
        public IActionResult GetCursoById(int id)
        {
            var curso = _cursoBusiness.GetCursoById(id);

            if (curso == null)
            {
                return NotFound("Curso não localizado");
            }

            return Ok(curso);
        }

        [HttpPost("CreateNewCurso")]
        public IActionResult CreateNewCurso([FromBody]CursoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCurso = _cursoBusiness.NewCurso(dto);

            if (newCurso == null)
            {
                return BadRequest("Erro ao criar o curso");
            }

            return Ok("Curso criado com sucesso!");
        }

        [HttpPut("UpdateCurso")]
        public IActionResult UpdateCurso([FromBody] CursoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = _cursoBusiness.UpdateCurso(dto);

            if (!isUpdated)
            {
                return NotFound("Curso informado não localizado.");
            }

            return Ok("Curso atualizado com sucesso!");
        }

        [HttpDelete("DeleteCurso")]
        public IActionResult DeleteCurso(int id)
        {
            var isDeleted = _cursoBusiness.DeleteCurso(id);

            if (!isDeleted)
            {
                return NotFound("Curso não localizado.");
            }

            return Ok("Curso excluído com sucesso!");
        }
    }
}
