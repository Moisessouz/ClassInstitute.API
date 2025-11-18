using ClassInstitute.Application.DTO;
using ClassInstitute.Domain.Models;
using ClassInstitute.Infrastructure.Data;
using ClassInstitute.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClassInstitute.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;
        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        public AlunoDto CreateAluno(AlunoDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"INSERT INTO Alunos (Nome, Email, DataNascimento, Ativo) VALUES (@Nome, @Email, @DataNascimento, 1); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@Nome", SqlDbType.VarChar).Value = dto.Nome;
                        com.Parameters.Add("@Email", SqlDbType.VarChar).Value = dto.Email;
                        com.Parameters.Add("@DataNascimento", SqlDbType.DateTime).Value = dto.DataNascimento;

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new AlunoDto
                                {
                                    Id = Convert.ToInt32(reader[0]),
                                    Nome = dto.Nome,
                                    Email = dto.Email,
                                    DataNascimento = dto.DataNascimento
                                };
                            }
                            else
                            {
                                throw new Exception("Falha ao inserir o aluno.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar aluno: " + ex.Message);
            }
        }

        public bool DeleteAluno(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"DELETE FROM Alunos WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Id", id);

                        return com.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar aluno: " + ex.Message);
            }
        }

        public List<AlunoEntity> GetAllAlunos()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"SELECT Id, Nome, Email, DataNascimento, Ativo FROM Alunos (NOLOCK)";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = com.ExecuteReader())
                        {

                            var alunos = new List<AlunoEntity>();

                            while (reader.Read())
                            {
                                var aluno = new AlunoEntity
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"]?.ToString() ?? string.Empty,
                                    Email = reader["Email"]?.ToString() ?? string.Empty,
                                    DataNascimento = reader["DataNascimento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["DataNascimento"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };
                                alunos.Add(aluno);
                            }
                            return alunos;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter alunos: " + ex.Message);
            }
        }

        public AlunoEntity GetAlunoById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"SELECT Id, Nome, Email, DataNascimento, Ativo FROM Alunos (NOLOCK) WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            AlunoEntity? aluno = null;

                            if (reader.Read())
                            {
                                aluno = new AlunoEntity
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"]?.ToString() ?? string.Empty,
                                    Email = reader["Email"]?.ToString() ?? string.Empty,
                                    DataNascimento = reader["DataNascimento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["DataNascimento"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };

                            }
                            return aluno;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter aluno: " + ex.Message);
            }
        }

        public bool UpdateAluno(AlunoDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    con.Open();

                    string query = @"UPDATE Alunos 
                                    SET Nome = @Nome, 
                                        Email = @Email, 
                                        DataNascimento = @DataNascimento, 
                                        Ativo = @Ativo 
                                    WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.Add("@Id", SqlDbType.Int).Value = dto.Id;
                        com.Parameters.Add("@Nome", SqlDbType.VarChar).Value = dto.Nome;
                        com.Parameters.Add("@Email", SqlDbType.VarChar).Value = dto.Email;
                        com.Parameters.Add("@DataNascimento", SqlDbType.DateTime).Value = dto.DataNascimento;
                        com.Parameters.Add("@Ativo", SqlDbType.Bit).Value = dto.Ativo;

                        return com.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar aluno: " + ex.Message);
            }
        }
    }
}
