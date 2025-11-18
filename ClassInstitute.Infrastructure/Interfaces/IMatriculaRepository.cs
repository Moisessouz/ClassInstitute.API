using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInstitute.Infrastructure.Interfaces
{
    public interface IMatriculaRepository
    {
        bool DeleteMatricula(int id);
        List<MatriculaEntity> GetAllMatriculas();
        MatriculaDto GetMatriculaByCurso(int id);
        MatriculaDto NewMatricula(MatriculaDto dto);
    }
}
