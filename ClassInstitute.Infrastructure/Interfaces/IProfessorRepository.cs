using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInstitute.Infrastructure.Interfaces
{
    public interface IProfessorRepository
    {
        bool DeleteProfessor(int id);
        List<ProfessorEntity> GetAllProfessores();
        ProfessorEntity GetProfessorById(int id);
        ProfessorDto NewProfessor(ProfessorDto dto);
        bool UpdateProfessor(ProfessorDto dto);
    }
}
