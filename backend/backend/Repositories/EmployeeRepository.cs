using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using System.Data.SqlClient;

namespace backend.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _context;

        public EmployeeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Employees", conn))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            DateOfBirth = reader.GetDateTime(4),
                            Salary = reader.GetDecimal(5),
                            DepartmentId = reader.GetInt32(6)
                        });
                    }
                }
            }
            return employees;
        }

        public Employee GetById(int id)
        {
            Employee employee = null;
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Employees WHERE EmployeeId = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            EmployeeId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            DateOfBirth = reader.GetDateTime(4),
                            Salary = reader.GetDecimal(5),
                            DepartmentId = reader.GetInt32(6)
                        };
                    }
                }
            }
            return employee;
        }

        public void Add(Employee employee)
        {
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Email, DateOfBirth, Salary, DepartmentId) VALUES (@FName, @LName, @Email, @DOB, @Salary, @DeptId)", conn))
            {
                cmd.Parameters.AddWithValue("@FName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LName", employee.LastName);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@DOB", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@DeptId", employee.DepartmentId);
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Employee employee)
        {
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, DateOfBirth = @DOB, Salary = @Salary, DepartmentId = @DeptId WHERE EmployeeId = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@DOB", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@DeptId", employee.DepartmentId);
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeId = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }
    }
}
