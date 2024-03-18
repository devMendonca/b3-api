using b3_domain.Model;
using System.Data.SqlClient;

namespace b3.Worker.Services
{
    public class DbService
    {
        private readonly string _connectionString;

        public DbService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertTarefa(Tarefa tarefa)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Tarefa (descricao, responsavel, celular, cpf, status, data) " +
                                   "VALUES (@descricao, @responsavel, @celular, @cpf, @status, @data)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@descricao", tarefa.descricao);
                        command.Parameters.AddWithValue("@responsavel", tarefa.responsavel);
                        command.Parameters.AddWithValue("@celular", tarefa.celular);
                        command.Parameters.AddWithValue("@cpf", tarefa.cpf);
                        command.Parameters.AddWithValue("@status", tarefa.status);
                        command.Parameters.AddWithValue("@data", tarefa.data);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
