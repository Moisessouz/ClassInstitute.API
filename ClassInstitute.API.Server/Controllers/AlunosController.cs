using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassInstitute.Application.Interfaces;
using ClassInstitute.Application.DTO;


namespace ClassInstitute.API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoBusiness _alunoBusiness;

        public AlunoController(IAlunoBusiness alunoBusiness)
        {
            _alunoBusiness = alunoBusiness;
        }

        [HttpGet("GetAllAlunos")]
        public IActionResult GetAllAlunos()
        {
            var alunos = _alunoBusiness.GetAllAlunos();

            return Ok(alunos);
        }

        [HttpGet("GetAlunoById/{id}")]
        public IActionResult GetAlunoById(int id)
        {
            var aluno = _alunoBusiness.GetAlunoById(id);

            if (aluno == null)
            {
                return NotFound("Aluno não localizado.");
            }

            return Ok(aluno);

        }

        [HttpPost("CreateNewAluno")]
        public IActionResult CreateNewAluno([FromBody] AlunoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newAluno = _alunoBusiness.CreateAluno(dto);

            if (newAluno == null)
            {
                return BadRequest("Erro ao criar o aluno.");
            }

            return Ok("Aluno criado com sucesso!");
        }

        [HttpPut("UpdateAluno")]
        public IActionResult UpdateAluno([FromBody] AlunoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated =  _alunoBusiness.UpdateAluno(dto);

            if (!isUpdated)
            {
                return NotFound("Aluno informado não localizado.");
            }

            return Ok("Aluno atualizado com sucesso!");
        }

        [HttpDelete("DeleteAluno")]
        public IActionResult DeleteAluno(int id)
        {
            var isDeleted = _alunoBusiness.DeleteAluno(id);

            if (!isDeleted)
            {
                return NotFound("Aluno não localizado.");
            }

            return Ok("Aluno excluído com sucesso!");
        }
    }
}
