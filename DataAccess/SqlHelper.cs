using System.Data;
using Npgsql;
using NpgsqlTypes;
namespace TestTaskUP.DataAccess
{

    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query, NpgsqlParameter[]? parameters = null)
        {
            var table = new DataTable();
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand(query, conn);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(table);
            return table;
        }

        public int ExecuteNonQuery(string query, NpgsqlParameter[] parameters)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddRange(parameters);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }


}
