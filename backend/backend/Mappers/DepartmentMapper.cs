using backend.DTOs;
using backend.Models;

namespace backend.Mappers
{
    public static class DepartmentMapper
    {
        public static DepartmentDto ToDto(this Department department)
        {
            return new DepartmentDto
            {
                DepartmentID = department.DepartmentID,
                DepartmentCode = department.DepartmentCode,
                DepartmentName = department.DepartmentName
            };
        }
        public static Department ToEntity(this DepartmentDto departmentDto)
        {
            return new Department
            {
                DepartmentID = departmentDto.DepartmentID,
                DepartmentCode = departmentDto.DepartmentCode,
                DepartmentName = departmentDto.DepartmentName
            };
        }
    }
}
