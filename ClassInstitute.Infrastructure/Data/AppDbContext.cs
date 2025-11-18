using Microsoft.EntityFrameworkCore;
using ClassInstitute.Domain.Models;

namespace ClassInstitute.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AlunoEntity> Alunos { get; set; }
        public DbSet<CursoEntity> Cursos { get; set; }
        public DbSet<MatriculaEntity> Matriculas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

    }
}
