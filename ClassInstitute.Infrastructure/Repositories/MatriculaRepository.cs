using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using ClassInstitute.Infrastructure.Data;
using ClassInstitute.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClassInstitute.Infrastructure.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly AppDbContext _context;
        public MatriculaRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool DeleteMatricula(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"DELETE FROM Matriculas WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                        return com.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar matrícula: " + ex.Message);
            }
        }

        public List<MatriculaEntity> GetAllMatriculas()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();
                    string query = @"SELECT M.Id, M.DataMatricula, M.AlunoId, A.Nome AS Aluno, M.CursoId, C.Nome AS Curso
                                    FROM Matriculas (NOLOCK) AS M
                                    INNER JOIN Alunos (NOLOCK) AS A
                                        ON M.AlunoId = A.Id
                                    INNER JOIN Cursos (NOLOCK) AS C
                                        ON M.CursoId = C.Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            List<MatriculaEntity> matriculas = new List<MatriculaEntity>();

                            while (reader.Read())
                            {
                                MatriculaEntity matricula = new MatriculaEntity
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    DataMatricula = reader["DataMatricula"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["DataMatricula"]),
                                    AlunoId = Convert.ToInt32(reader["AlunoId"]),
                                    CursoId = Convert.ToInt32(reader["CursoId"]),
                                    Aluno = new AlunoEntity
                                    {
                                        Id = Convert.ToInt32(reader["AlunoId"]),
                                        Nome = reader["Aluno"].ToString()?.ToString() ?? string.Empty,
                                    },
                                    Curso = new CursoEntity
                                    {
                                        Id = Convert.ToInt32(reader["CursoId"]),
                                        Nome = reader["Curso"].ToString()?.ToString() ?? string.Empty
                                    }
                                };
                                matriculas.Add(matricula);
                            }
                            return matriculas;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter matrículas: " + ex.Message);
            }
        }

        public MatriculaDto GetMatriculaByCurso(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"SELECT M.Id, M.DataMatricula, M.AlunoId, A.Nome AS Aluno, M.CursoId, C.Nome AS Curso
                                    FROM Matriculas (NOLOCK) AS M
                                    INNER JOIN Alunos (NOLOCK) AS A
                                        ON M.AlunoId = A.Id
                                    INNER JOIN Cursos (NOLOCK) AS C
                                        ON M.CursoId = C.Id
                                    WHERE C.Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@CursoId", id);

                        using (SqlDataReader reader = com.ExecuteReader())
                        {

                            MatriculaDto? matricula = null;

                            if (reader.Read())
                            {
                                matricula = new MatriculaDto
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    DataMatricula = reader["DataMatricula"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["DataMatricula"]),
                                    AlunoId = Convert.ToInt32(reader["AlunoId"]),
                                    CursoId = Convert.ToInt32(reader["CursoId"]),
                                };
                            }
                            return matricula!;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter matrícula do curso: " + ex.Message);
            }
        }

        public MatriculaDto NewMatricula(MatriculaDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();
                    string query = @"INSERT INTO Matriculas (AlunoId, CursoId, DataMatricula)
                                    VALUES (@AlunoId, @CursoId, GETDATE());
                                    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@AlunoId", SqlDbType.Int).Value = dto.AlunoId;
                        com.Parameters.Add("@CursoId", SqlDbType.Int).Value = dto.CursoId;

                        var result = com.ExecuteScalar();

                        if (result != null)
                        {
                            return new MatriculaDto
                            {
                                AlunoId = dto.AlunoId,
                                CursoId = dto.CursoId,
                                DataMatricula = dto.DataMatricula
                            };

                        }
                        else
                        {
                            throw new Exception("Falha ao inserir nova matrícula.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar nova matrícula: " + ex.Message);
            }
        }
    }
}
