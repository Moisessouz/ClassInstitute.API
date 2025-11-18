using System.ComponentModel.DataAnnotations.Schema;

namespace ClassInstitute.Domain.Models
{
    public class CursoEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public int DuracaoHoras { get; set; }
        public bool Ativo { get; set; }
        public int? ProfessorId { get; set; }
        public ProfessorEntity? Professor { get; set; }
        public ICollection<MatriculaEntity> Matriculas { get; set; } = new List<MatriculaEntity>();
    }
}
