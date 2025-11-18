using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInstitute.Domain.Models
{
    public class ProfessorEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Especialidade { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataAdmissao { get; set; }
        public DateTime? DataDesligamento { get; set; }
        public bool Ativo { get; set; }
        public ICollection<CursoEntity> Cursos { get; set; } = new List<CursoEntity>();
    }
}
