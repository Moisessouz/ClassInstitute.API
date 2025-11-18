using ClassInstitute.Application.DTO;
using ClassInstitute.Application.Interfaces;
using ClassInstitute.Domain.Models;
using ClassInstitute.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInstitute.Service.Business
{
    public class CursoBusiness : ICursoBusiness
    {
        private readonly ICursoRepository _cursoDao;

        public CursoBusiness(ICursoRepository cursoDao)
        {
            _cursoDao = cursoDao;
        }

        public CursoDto NewCurso(CursoDto dto)
        {
            return _cursoDao.NewCurso(dto);
        }

        public List<CursoEntity> GetAllCursos()
        {
            return _cursoDao.GetAllCursos();
        }

        public CursoEntity GetCursoById(int id)
        {
            return _cursoDao.GetCursoById(id);
        }

        public bool UpdateCurso(CursoDto dto)
        {
            return _cursoDao.UpdateCurso(dto);
        }

        public bool DeleteCurso(int id)
        {
            return _cursoDao.DeleteCurso(id);
        }
    }
}
