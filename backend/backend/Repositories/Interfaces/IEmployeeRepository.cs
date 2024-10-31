using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {

        public IEnumerable<Employee> GetAll();

        public Employee GetById(int id);

        public void Add(Employee employee);

        public void Update(Employee employee);

        public void Delete(int id);
    }
}
