using System.ComponentModel.DataAnnotations.Schema;

namespace ClassInstitute.Domain.Models
{
    public class MatriculaEntity
    {
        public int Id { get; set; }
        public DateTime DataMatricula { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public AlunoEntity Aluno { get; set; } = null!;
        public CursoEntity Curso { get; set; } = null!;
    }
}
