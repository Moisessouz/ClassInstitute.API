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
    public class ProfessorBusiness : IProfessorBusiness
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorBusiness(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public bool DeleteProfessor(int id)
        {
            return _professorRepository.DeleteProfessor(id);
        }

        public List<ProfessorEntity> GetAllProfessores()
        {
            return _professorRepository.GetAllProfessores();
        }

        public ProfessorEntity GetProfessorById(int id)
        {
            return _professorRepository.GetProfessorById(id);
        }

        public ProfessorDto NewProfessor(ProfessorDto dto)
        {
            return _professorRepository.NewProfessor(dto);
        }

        public bool UpdateProfessor(ProfessorDto dto)
        {
            return _professorRepository.UpdateProfessor(dto);
        }
    }
}
