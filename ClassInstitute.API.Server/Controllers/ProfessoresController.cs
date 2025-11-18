using ClassInstitute.Application.DTO;
using ClassInstitute.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassInstitute.API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessoresController : ControllerBase
    {
        private readonly IProfessorBusiness _professorBusiness;

        public ProfessoresController(IProfessorBusiness profBusiness)
        {
            _professorBusiness = profBusiness;
        }

        [HttpGet("GetAllProfessores")]
        public IActionResult GetAllProfessores()
        {
            var professores = _professorBusiness.GetAllProfessores();

            return Ok(professores);
        }

        [HttpGet("GetProfessorById/{id}")]
        public IActionResult GetProfessorById(int id)
        {
            var professor = _professorBusiness.GetProfessorById(id);

            if (professor == null)
            {
                return NotFound("Professor não localizado.");
            }

            return Ok(professor);

        }

        [HttpPost("CreateNewProfessor")]
        public IActionResult CreateNewProfessor([FromBody] ProfessorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newProfessor = _professorBusiness.NewProfessor(dto);

            if (newProfessor == null)
            {
                return BadRequest("Erro ao criar o professor.");
            }

            return Ok("Professor criado com sucesso!");
        }

        [HttpPut("UpdateProfessor")]
        public IActionResult UpdateProfessor([FromBody] ProfessorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = _professorBusiness.UpdateProfessor(dto);

            if (!isUpdated)
            {
                return NotFound("Professor informado não localizado.");
            }

            return Ok("Professor atualizado com sucesso!");
        }

        [HttpDelete("DeleteProfessor")]
        public IActionResult DeleteAluno(int id)
        {
            var isDeleted = _professorBusiness.DeleteProfessor(id);

            if (!isDeleted)
            {
                return NotFound("Professor não localizado.");
            }

            return Ok("Professor excluído com sucesso!");
        }
    }
}
