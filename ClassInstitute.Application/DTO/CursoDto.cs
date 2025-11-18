using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInstitute.Application.DTO
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int DuracaoHoras { get; set; }
        public int ProfessorId { get; set; }
        public bool Ativo { get; set; }
    }
}
