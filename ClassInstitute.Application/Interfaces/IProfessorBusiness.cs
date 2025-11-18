using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInstitute.Application.Interfaces
{
    public interface IProfessorBusiness
    {
        bool DeleteProfessor(int id);
        List<ProfessorEntity> GetAllProfessores();
        ProfessorEntity GetProfessorById(int id);
        ProfessorDto NewProfessor(ProfessorDto dto);
        bool UpdateProfessor(ProfessorDto dto);
    }
}
