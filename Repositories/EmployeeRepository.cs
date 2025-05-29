using Npgsql;
using NpgsqlTypes;
using System.Data;
using TestTaskUP.DataAccess;
using TestTaskUP.Models;

namespace TestTaskUP.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetFilteredEmployees(int? departmentId = null, string? nameSearch = null)
        {
            var employees = new List<Employee>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT * FROM get_filtered_employees(@DepartmentId, @NameSearch)";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@DepartmentId", (object?)departmentId ?? DBNull.Value);
            command.Parameters.AddWithValue("@NameSearch", (object?)nameSearch ?? DBNull.Value);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var employee = new Employee
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    MiddleName = reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? null : reader.GetString(reader.GetOrdinal("MiddleName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                    HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                    Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                    DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                    PositionId = reader.GetInt32(reader.GetOrdinal("PositionId")),
                    CompanyId = reader.GetInt32(reader.GetOrdinal("CompanyId")),
                    DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                    PositionName = reader.GetString(reader.GetOrdinal("PositionName")),
                    CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                };

                employees.Add(employee);
            }

            return employees;
        }

        public Employee? GetEmployeeById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT * FROM get_employee_by_id(@Id)";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Employee
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    MiddleName = reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? null : reader.GetString(reader.GetOrdinal("MiddleName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                    HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                    Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                    DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                    PositionId = reader.GetInt32(reader.GetOrdinal("PositionId")),
                    //CompanyId = reader.GetInt32(reader.GetOrdinal("CompanyId")),
                };
            }

            return null;
        }

        public void UpdateEmployee(Employee employee)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var query = "CALL update_employee(@Id, @FirstName, @MiddleName, @LastName, @Address, @Phone, @BirthDate, @HireDate, @Salary, @DepartmentId, @PositionId)";
            using var command = new NpgsqlCommand(query, connection);

            command.Parameters.Add("@Id", NpgsqlDbType.Integer).Value = employee.Id;
            command.Parameters.Add("@FirstName", NpgsqlDbType.Varchar).Value = employee.FirstName;
            command.Parameters.Add("@MiddleName", NpgsqlDbType.Varchar).Value = employee.MiddleName;
            command.Parameters.Add("@LastName", NpgsqlDbType.Varchar).Value = employee.LastName;
            command.Parameters.Add("@Address", NpgsqlDbType.Varchar).Value = employee.Address;
            command.Parameters.Add("@Phone", NpgsqlDbType.Varchar).Value = employee.Phone;
            command.Parameters.Add("@BirthDate", NpgsqlDbType.Date).Value = employee.BirthDate;
            command.Parameters.Add("@HireDate", NpgsqlDbType.Date).Value = employee.HireDate;
            command.Parameters.Add("@Salary", NpgsqlDbType.Numeric).Value = employee.Salary;
            command.Parameters.Add("@DepartmentId", NpgsqlDbType.Integer).Value = employee.DepartmentId;
            command.Parameters.Add("@PositionId", NpgsqlDbType.Integer).Value = employee.PositionId;
            //command.Parameters.Add("@CompanyId", NpgsqlDbType.Integer).Value = employee.CompanyId;

            command.ExecuteNonQuery();
        }
        public List<SalaryReportItem> GetSalaryReport(int? departmentId)
        {
            var results = new List<SalaryReportItem>();

            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            var sql = @"
            SELECT 
                   d.Name as DepartmentName,
                   SUM(e.Salary) as Salary,
                   MIN(e.Salary) as MINSalary,
                   MAX(e.Salary) as MAXSalary,
                   AVG(e.Salary) as AVGSalary,
                   COUNT(e.Id) AS EmployeeCount

            FROM public.Employee e
            JOIN public.Department d ON e.DepartmentId = d.Id
            WHERE (@departmentId IS NULL OR e.DepartmentId = @departmentId)
            group by DepartmentName, e.departmentid
			order by e.departmentid
                    ";

            cmd.CommandText = sql;
            cmd.Parameters.Add(new NpgsqlParameter("@departmentId", NpgsqlDbType.Integer)
            {
                Value = departmentId.HasValue ? departmentId.Value : DBNull.Value
            });

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new SalaryReportItem
                {
                    DepartmentName = reader.GetString(0),
                    Salary = reader.GetDecimal(1),
                    MinSalary = reader.GetDecimal(2),
                    MaxSalary = reader.GetDecimal(3),
                    AvgSalary = reader.GetDecimal(4),
                    EmployeeCount = reader.GetInt32(5),

                });
            }

            return results;
        }
    }

}
