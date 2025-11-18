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
    public class AlunoBusiness : IAlunoBusiness
    {
        private readonly IAlunoRepository _alunoDao;

        public AlunoBusiness(IAlunoRepository alunoDao)
        {
            _alunoDao = alunoDao;
        }

        public AlunoDto CreateAluno(AlunoDto dto)
        {
            return _alunoDao.CreateAluno(dto);
        }

        public bool DeleteAluno(int id)
        {
            return _alunoDao.DeleteAluno(id);
        }

        public List<AlunoEntity> GetAllAlunos()
        {
            return _alunoDao.GetAllAlunos();
        }

        public AlunoEntity GetAlunoById(int id)
        {
            return _alunoDao.GetAlunoById(id);
        }

        public bool UpdateAluno(AlunoDto dto)
        {
            return _alunoDao.UpdateAluno(dto);
        }
    }
}
