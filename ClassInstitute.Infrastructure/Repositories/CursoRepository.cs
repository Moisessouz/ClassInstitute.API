using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using ClassInstitute.Infrastructure.Data;
using ClassInstitute.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClassInstitute.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly AppDbContext _context;

        public CursoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<CursoEntity> GetAllCursos()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"SELECT C.Id, C.Nome, C.Descricao, C.DuracaoHoras, C.Ativo, P.Nome AS Professor
                                        FROM Cursos (NOLOCK) AS C
                                        INNER JOIN Professores (NOLOCK) AS P
                                            ON P.Id = C.ProfessorId";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            var cursos = new List<CursoEntity>();

                            while (reader.Read())
                            {
                                CursoEntity curso = new CursoEntity
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"]?.ToString() ?? string.Empty,
                                    Descricao = reader["Descricao"]?.ToString() ?? string.Empty,
                                    DuracaoHoras = Convert.ToInt32(reader["DuracaoHoras"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };

                                curso.Professor = new ProfessorEntity
                                {
                                    Nome = reader["Professor"]?.ToString() ?? string.Empty
                                };

                                cursos.Add(curso);
                            }
                            return cursos;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter cursos: " + ex.Message);
            }
        }

        public CursoEntity GetCursoById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();
                    string query = @"SELECT C.Id, C.Nome, C.Descricao, C.DuracaoHoras, C.Ativo, P.Nome AS Professor
                                        FROM Cursos (NOLOCK) AS C
                                        INNER JOIN Professores (NOLOCK) AS P
                                            ON P.Id = C.ProfessorId
                                       WHERE C.Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            CursoEntity? curso = null;

                            if (reader.Read())
                            {
                                curso = new CursoEntity
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"]?.ToString() ?? string.Empty,
                                    Descricao = reader["Descricao"]?.ToString() ?? string.Empty,
                                    DuracaoHoras = Convert.ToInt32(reader["DuracaoHoras"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };

                                curso.Professor = new ProfessorEntity
                                {
                                    Nome = reader["Professor"]?.ToString() ?? string.Empty
                                };
                            }
                            return curso;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter curso por ID: " + ex.Message);
            }
        }

        public CursoDto NewCurso(CursoDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"INSERT INTO Cursos (Nome, Descricao, DuracaoHoras, Ativo, ProfessorId)
                                     VALUES (@Nome, @Descricao, @DuracaoHoras, 1, 1);
                                     SELECT SCOPE_IDENTITY();";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@Nome", SqlDbType.VarChar).Value = dto.Nome;
                        com.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = dto.Descricao;
                        com.Parameters.Add("@DuracaoHoras", SqlDbType.Int).Value = dto.DuracaoHoras;

                        var result = com.ExecuteScalar();

                        if (result != null)
                        {
                            return new CursoDto
                            {
                                Id = Convert.ToInt32(result),
                                Nome = dto.Nome,
                                Descricao = dto.Descricao,
                                DuracaoHoras = Convert.ToInt32(dto.DuracaoHoras),
                                Ativo = true
                            };

                        }
                        else
                        {
                            throw new Exception("Falha ao inserir novo curso.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar novo curso: " + ex.Message);
            }
        }

        public bool UpdateCurso(CursoDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"UPDATE Cursos
                                     SET Nome = @Nome,
                                         Descricao = @Descricao,
                                         DuracaoHoras = @DuracaoHoras,
                                         Ativo = @Ativo
                                     WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@Id", SqlDbType.Int).Value = dto.Id;
                        com.Parameters.Add("@Nome", SqlDbType.VarChar).Value = dto.Nome;
                        com.Parameters.Add("@Descricao", SqlDbType.VarChar).Value = dto.Descricao;
                        com.Parameters.Add("@DuracaoHoras", SqlDbType.Int).Value = dto.DuracaoHoras;
                        com.Parameters.Add("@Ativo", SqlDbType.Bit).Value = dto.Ativo;

                        var result = com.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar curso: " + ex.Message);
            }
        }

        public bool DeleteCurso(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"DELETE FROM Cursos WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Id", id);

                        var result = com.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar curso: " + ex.Message);
            }
        }
    }
}
