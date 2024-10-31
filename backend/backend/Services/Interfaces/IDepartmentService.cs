using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IDepartmentService
    {
        public IEnumerable<Department> GetAll();

        public Department GetById(int id);

        public void Add(Department department);

        public void Update(Department department);

        public void Delete(int id);
    }
}
