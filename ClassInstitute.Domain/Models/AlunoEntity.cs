namespace ClassInstitute.Domain.Models
{
    public class AlunoEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public ICollection<MatriculaEntity> Matriculas { get; set; } = new List<MatriculaEntity>();
    }
}
