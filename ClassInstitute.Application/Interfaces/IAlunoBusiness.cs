using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInstitute.Application.Interfaces
{
    public interface IAlunoBusiness
    {
        AlunoDto CreateAluno(AlunoDto dto);
        bool DeleteAluno(int id);
        List<AlunoEntity> GetAllAlunos();
        AlunoEntity GetAlunoById(int id);
        bool UpdateAluno(AlunoDto dto);
    }
}
