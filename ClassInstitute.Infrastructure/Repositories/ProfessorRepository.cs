using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using ClassInstitute.Infrastructure.Data;
using ClassInstitute.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClassInstitute.Infrastructure.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _context;

        public ProfessorRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool DeleteProfessor(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"DELETE FROM ProfessorEntity WHERE Id = @Id";

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

        public List<ProfessorEntity> GetAllProfessores()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"SELECT Id, Nome, Especialidade, Email, DataAdmissao, DataDesligamento, Ativo FROM ProfessorEntity";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            List<ProfessorEntity> professores = new List<ProfessorEntity>();

                            while (reader.Read())
                            {
                                ProfessorEntity professor = new ProfessorEntity
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"].ToString()?.ToString() ?? string.Empty,
                                    Especialidade = reader["Especialidade"].ToString()?.ToString() ?? string.Empty,
                                    Email = reader["Email"].ToString()?.ToString() ?? string.Empty,
                                    DataAdmissao = Convert.ToDateTime(reader["DataAdmissao"]),
                                    DataDesligamento = reader["DataDesligamento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["DataDesligamento"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };
                                professores.Add(professor);
                            }
                            return professores;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter professores: " + ex.Message);
            }
        }

        public ProfessorEntity GetProfessorById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"SELECT Id, Nome, Especialidade, Email, DataAdmissao, DataDesligamento, Ativo FROM ProfessorEntity WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ProfessorEntity professor = new ProfessorEntity
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"].ToString()?.ToString() ?? string.Empty,
                                    Especialidade = reader["Especialidade"].ToString()?.ToString() ?? string.Empty,
                                    Email = reader["Email"].ToString()?.ToString() ?? string.Empty,
                                    DataAdmissao = Convert.ToDateTime(reader["DataAdmissao"]),
                                    DataDesligamento = reader["DataDesligamento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["DataDesligamento"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };
                                return professor;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter professor: " + ex.Message);
            }
        }

        public ProfessorDto NewProfessor(ProfessorDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"INSERT INTO ProfessorEntity (Nome, Especialidade, Email, DataAdmissao, DataDesligamento, Ativo)
                                     VALUES (@Nome, @Especialidade, @Email, @DataAdmissao, @DataDesligamento, @Ativo);
                                     SELECT SCOPE_IDENTITY();";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = dto.Nome;
                        com.Parameters.Add("@Especialidade", SqlDbType.NVarChar).Value = dto.Especialidade;
                        com.Parameters.Add("@Email", SqlDbType.NVarChar).Value = dto.Email;
                        com.Parameters.Add("@DataAdmissao", SqlDbType.DateTime).Value = dto.DataAdmissao;
                        com.Parameters.Add("@DataDesligamento", SqlDbType.DateTime).Value = dto.DataDesligamento == DateTime.MinValue ? (object)DBNull.Value : dto.DataDesligamento;
                        com.Parameters.Add("@Ativo", SqlDbType.Bit).Value = dto.Ativo;

                        var result = com.ExecuteScalar();

                        if (result != null)
                        {
                            return new ProfessorDto
                            {
                                Id = Convert.ToInt32(result),
                                Nome = dto.Nome,
                                Especialidade = dto.Especialidade,
                                Email = dto.Email,
                                DataAdmissao = dto.DataAdmissao,
                                DataDesligamento = dto.DataDesligamento,
                                Ativo = dto.Ativo
                            };
                        }
                        else
                        {
                            throw new Exception("Falha ao inserir novo professor.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar professor: " + ex.Message);
            }
        }

        public bool UpdateProfessor(ProfessorDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();
                    string query = @"UPDATE ProfessorEntity
                                     SET Nome = @Nome,
                                         Especialidade = @Especialidade,
                                         Email = @Email,
                                         DataAdmissao = @DataAdmissao,
                                         DataDesligamento = @DataDesligamento,
                                         Ativo = @Ativo
                                     WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@Id", SqlDbType.Int).Value = dto.Id;
                        com.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = dto.Nome;
                        com.Parameters.Add("@Especialidade", SqlDbType.NVarChar).Value = dto.Especialidade;
                        com.Parameters.Add("@Email", SqlDbType.NVarChar).Value = dto.Email;
                        com.Parameters.Add("@DataAdmissao", SqlDbType.DateTime).Value = dto.DataAdmissao;
                        com.Parameters.Add("@DataDesligamento", SqlDbType.DateTime).Value = dto.DataDesligamento == DateTime.MinValue ? (object)DBNull.Value : dto.DataDesligamento;
                        com.Parameters.Add("@Ativo", SqlDbType.Bit).Value = dto.Ativo;

                        return com.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar professor: " + ex.Message);
            }
        }
    }
}
