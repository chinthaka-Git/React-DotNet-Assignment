using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using System.Data.SqlClient;

namespace backend.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DatabaseContext _context;

        public DepartmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            var departments = new List<Department>();
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Departments", conn))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            DepartmentID = reader.GetInt32(0),
                            DepartmentCode = reader.GetString(1),
                            DepartmentName = reader.GetString(2)
                        });
                    }
                }
            }
            return departments;
        }

        public Department GetById(int id)
        {
            Department department = null;
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Departments WHERE DepartmentId = @Id", conn))
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
                        department = new Department
                        {
                            DepartmentID = reader.GetInt32(0),
                            DepartmentCode = reader.GetString(1),
                            DepartmentName = reader.GetString(2)
                        };
                    }
                }
            }
            return department;
        }

        public void Add(Department department)
        {
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("INSERT INTO Departments (DepartmentCode, DepartmentName) VALUES (@Code, @Name)", conn))
            {
                cmd.Parameters.AddWithValue("@Code", department.DepartmentCode);
                cmd.Parameters.AddWithValue("@Name", department.DepartmentName);
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Department department)
        {
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("UPDATE Departments SET DepartmentCode = @Code, DepartmentName = @Name WHERE DepartmentId = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", department.DepartmentID);
                cmd.Parameters.AddWithValue("@Code", department.DepartmentCode);
                cmd.Parameters.AddWithValue("@Name", department.DepartmentName);
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            if (HasEmployees(id))
            {
                throw new InvalidOperationException("Cannot delete department. Please transfer employees to another department before deleting.");
            }

            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("DELETE FROM Departments WHERE DepartmentId = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        private bool HasEmployees(int departmentId)
        {
            using (var conn = _context.GetConnection())
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Employees WHERE DepartmentId = @DepartmentId", conn))
            {
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
