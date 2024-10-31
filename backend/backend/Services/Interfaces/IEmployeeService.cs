using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAll();

        public EmployeeDto GetById(int id);

        public void Add(Employee employee);

        public void Update(Employee employee);

        public void Delete(int id);
    }
}
