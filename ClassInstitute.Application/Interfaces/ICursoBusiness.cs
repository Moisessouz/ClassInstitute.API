using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInstitute.Application.Interfaces
{
    public interface ICursoBusiness
    {
        CursoDto NewCurso(CursoDto dto);
        List<CursoEntity> GetAllCursos();
        CursoEntity GetCursoById(int id);
        bool UpdateCurso(CursoDto dto);
        bool DeleteCurso(int id);
    }
}
