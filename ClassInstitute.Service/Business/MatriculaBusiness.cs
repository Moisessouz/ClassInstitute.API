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
    public class MatriculaBusiness : IMatriculaBusiness
    {
        private readonly IMatriculaRepository _matriculaDao;

        public MatriculaBusiness(IMatriculaRepository matriculaDao)
        {
            _matriculaDao = matriculaDao;
        }

        public bool DeleteMatricula(int id)
        {
            return _matriculaDao.DeleteMatricula(id);
        }

        public List<MatriculaEntity> GetAllMatriculas()
        {
            return _matriculaDao.GetAllMatriculas();
        }

        public MatriculaDto GetMatriculaByCurso(int id)
        {
            return _matriculaDao.GetMatriculaByCurso(id);
        }

        public MatriculaDto NewMatricula(MatriculaDto dto)
        {
            return _matriculaDao.NewMatricula(dto);
        }
    }
}
